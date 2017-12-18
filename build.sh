#!/bin/bash
dotnet publish -c Release -r linux-x64 FuppBackend.NetCore.sln
docker-compose -f docker-compose.NetCore.yml build