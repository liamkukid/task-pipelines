# $env:USERPROFILE works only in windows and means %USERPROFILE%
# https://devblogs.microsoft.com/scripting/powertip-use-powershell-to-find-user-profile-path/

# https://docs.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-3.1
dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\aspnetapp.pfx -p password
dotnet dev-certs https --trust

Start-Process "http://localhost:4200"

docker-compose -f "docker-compose.yml" stop
docker-compose -f "docker-compose.yml" rm --force
docker-compose -f "docker-compose.yml" up --build mongo backend frontend