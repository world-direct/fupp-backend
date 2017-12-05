FROM microsoft/dotnet:2.0-runetime-deps
WORKDIR /app
COPY bin/Release/netcoreapp2.0/publish .
EXPOSE 4053
ENTRYPOINT ["dotnet", "C:\\app\\TestAgent.NetCore.dll"]