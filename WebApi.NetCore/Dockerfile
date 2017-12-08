FROM microsoft/dotnet:2.0-runtime-deps
WORKDIR /app
COPY bin/Release/netcoreapp2.0/linux-x64/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "C:\\app\\TestCoordinator.NetCore.dll"]