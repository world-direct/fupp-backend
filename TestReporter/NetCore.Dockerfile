FROM microsoft/dotnet:2.0-sdk
WORKDIR /app
COPY bin/Release/netcoreapp2.0/linux-x64/publish .
ENTRYPOINT ["dotnet", "TestReporter.NetCore.dll"]