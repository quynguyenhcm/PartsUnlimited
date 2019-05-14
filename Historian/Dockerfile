FROM mcr.microsoft.com/dotnet/core/sdk:2.2
WORKDIR /app
EXPOSE 5005
COPY ./publish .
ENTRYPOINT ["dotnet", "Historian.dll"]
