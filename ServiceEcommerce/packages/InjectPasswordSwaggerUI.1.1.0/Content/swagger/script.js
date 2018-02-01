var wrapper = document.getElementById("swagger-ui-container");
var child = wrapper;

var parent = wrapper.parentNode;
wrapper.parentNode.removeChild(wrapper);

var apikey = document.getElementById("input_apiKey");
apikey.setAttribute('type', 'password');

apikey.onchange = function () {
    var wrapper = document.getElementById("swagger-ui-container");
    var key = "1234567890";
    var url = "/Swagger/GetKey";
    var httpRequest = new XMLHttpRequest();
    var params = "";
    httpRequest.open("GET", url, true);

    httpRequest.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    httpRequest.onreadystatechange = function () {
        if (httpRequest.readyState === 4 && httpRequest.status === 200) {
            key = httpRequest.responseText;
            var enteredkey = "\"" + apikey.value + "\"";
            if (enteredkey == key) {
                parent.appendChild(child);
            } else {
                child = wrapper;
                wrapper.parentNode.removeChild(wrapper);
            }
        }
    }
    httpRequest.send(params);
};