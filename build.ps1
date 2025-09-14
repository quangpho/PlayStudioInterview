# Step 1: Restore the project dependencies
dotnet restore

# Step 2: Build the project
dotnet build
# dotnet ef migrations add InitialCreate --project ./Database --startup-project ./ClubApi

# Step 3: Apply migrations
dotnet ef database update --project ./Database --startup-project ./ClubApi
