param($installPath, $toolsPath, $package, $project)

$item = $project.ProjectItems.Item("swagger")
$item2 = $item.ProjectItems.Item("script.js")
$item2.Properties.Item("BuildAction").Value = [int]3