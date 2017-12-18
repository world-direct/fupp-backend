FROM microsoft/dotnet:2.0-runtime
WORKDIR /app
COPY bin/Release/netcoreapp2.0/linux-x64/publish .
EXPOSE 4053
ENTRYPOINT ["dotnet", "Lighthouse.NetCoreApp.dll"]