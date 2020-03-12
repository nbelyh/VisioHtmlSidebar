param ($newFull)

$oldFull = "111.111.111.111"

$oldVersion = $oldFull|%{[System.Version]$_}
$newVersion = $newFull|%{[System.Version]$_}

$old = "$($oldVersion.Major).$($oldVersion.Minor).$($oldVersion.Build)"
$new = "$($newVersion.Major).$($newVersion.Minor).$($newVersion.Build)"

$filesFull = 
	".\HtmlFormData\HtmlFormData.csproj", 
	".\HtmlFormData\Properties\AssemblyInfo.cs",
	".\Setup\Product.wxs",
	".\Setup\Setup.wixproj"

$filesFull | ForEach-Object { 
	
	$fileFull = $_
	(Get-Content $fileFull) | ForEach-Object { $_ -replace $oldFull, $newFull } | Set-Content $fileFull
}

$files = 
	".\HtmlFormData\HtmlFormData.csproj", 
	".\HtmlFormData\Properties\AssemblyInfo.cs",
	".\Setup\Product.wxs",
	".\Setup\Setup.wixproj"

$files | ForEach-Object { 
	
	$file = $_
	(Get-Content $file) | ForEach-Object { $_ -replace $old, $new } | Set-Content $file
}
