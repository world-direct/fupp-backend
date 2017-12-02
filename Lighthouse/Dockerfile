FROM microsoft/dotnet-framework:4.7
ARG source
WORKDIR /app
COPY ${source:-bin/Debug/net461} .
EXPOSE 4053
ENTRYPOINT ["C:\\app\\Lighthouse.exe"]
