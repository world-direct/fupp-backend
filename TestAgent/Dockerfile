FROM microsoft/dotnet-framework:4.7
ARG source
WORKDIR /app
COPY ${source:-bin/Debug} .
ENTRYPOINT ["C:\\app\\TestAgent.exe"]
