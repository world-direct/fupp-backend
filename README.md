fupp-backend


build NetCore project using dotnet-cli:

```
dotnet restore -r liniux-x64 FuppBackend.NetCore.sln
dotnet publish -c Release -r linux-x64 FuppBackend.NetCore.sln
```