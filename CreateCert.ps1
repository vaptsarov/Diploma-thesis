Set-Location $PSScriptRoot
$name = "UglyTest"
&"C:\Program Files (x86)\Windows Kits\8.0\bin\x64\makecert.exe" -a sha512 -r -pe -n CN=$name -ss my -sr LocalMachine -eku 1.3.6.1.5.5.7.3.2 -len 2048 -e 05/25/2020 -sky exchange "$name.cer"
# can be in C:\Program Files (x86)\Windows Kits\8.1\bin\x64\makecert.exe - depends on the OS