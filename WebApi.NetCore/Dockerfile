FROM microsoft/dotnet:2.0-sdk
WORKDIR /app
COPY bin/Release/netcoreapp2.0/linux-x64/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "WebApi.NetCore.dll"]