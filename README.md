fupp-backend


build NetCore project using dotnet-cli:

```
dotnet restore -r liniux-x64 FuppBackend.NetCore.sln
```

build docker containers for .NET core solution:
```
dotnet publish -c Release -r linux-x64 FuppBackend.NetCore.sln
docker-compose -f docker-compose.NetCore.yml build
```