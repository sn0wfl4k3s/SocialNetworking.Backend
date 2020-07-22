dotnet publish -r win-x64 --self-contained true -c Release

cd bin\Release\netcoreapp3.1\win-x64

Start-Process "APIRest.exe"
Start-Process "cmd" -ArgumentList "/k ngrok http 5000"

exit