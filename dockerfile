# 1. Use the .NET SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["PlayStudioInterview.sln", "."]
COPY ["ClubApi/ClubApi.csproj", "ClupApi/"]
COPY ["Database/Database.csproj", "Database/"]
COPY ["Model/Model.csproj", "Model/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["Services/Services.csproj", "Services/"]

RUN dotnet restore "PlayStudioInterview.sln"
RUN dotnet publish -c Release -o /app

# 2. Use a smaller runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ClubApi.dll"]
