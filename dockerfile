# 1. Use the .NET SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj file
COPY ["PlayStudioInterview.sln", "."]
COPY ["ClubApi/ClubApi.csproj", "ClubApi/"]
COPY ["Database/Database.csproj", "Database/"]
COPY ["Model/Model.csproj", "Model/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Test/Test.csproj", "Test/"]

RUN dotnet restore "ClubApi/ClubApi.csproj"

# Copy all files
COPY . .

WORKDIR "/src/ClubApi"
RUN dotnet publish "ClubApi.csproj" -c Release -o /app

# 2. Use a smaller runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ClubApi.dll"]
