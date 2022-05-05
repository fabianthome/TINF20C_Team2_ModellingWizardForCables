FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["CableWizardBackend.csproj", "./"]
RUN dotnet restore "./CableWizardBackend.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CableWizardBackend.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "CableWizardBackend.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CableWizardBackend.dll"]
