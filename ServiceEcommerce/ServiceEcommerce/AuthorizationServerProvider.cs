using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using ServiceEcommerce.Models;

namespace ServiceEcommerce
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {            
            context.Validated();
        }

        public override  async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            ecommerceTesteEntities dbContext = new ecommerceTesteEntities();

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            try
            {
                string user = context.UserName;
                string password = context.Password;

                var retorno = from bd in dbContext.cadastroUsuario
                              where bd.cadastroUsuario_user.Equals(user) &&
                                    bd.cadastroUsuario_senha.Equals(password) &&
                                    bd.cadastroUsuario_ativo == true
                              select bd.cadastroUsuario_id;
                              

                if (retorno.Count() <= 0)
                {
                    context.SetError("invalid_grant", "Usuario ou senha incorretos");
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                identity.AddClaim(new Claim(ClaimTypes.Name, user));

                var roles = new List<string>();

                roles.Add("User");

                foreach (var role in roles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }

                GenericPrincipal principal = new GenericPrincipal(identity, roles.ToArray());
                Thread.CurrentPrincipal = principal;

                context.Validated(identity);

            } catch
            {
                context.SetError("invalid_grant", "Falha ao autenticar");
            }
        }
    }
}