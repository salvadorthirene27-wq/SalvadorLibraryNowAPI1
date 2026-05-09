FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copy all your files into the Docker container
COPY . .

# 2. Print out the exact files Docker sees (this will show up in your Render logs!)
RUN echo "--- FINDING CSPROJ FILE ---" && find . -name "*.csproj"

# 3. Automatically find the .csproj file and build it (bypasses the path issue entirely)
RUN find . -name "*.csproj" -exec dotnet restore {} \;
RUN find . -name "*.csproj" -exec dotnet publish {} -c Release -o /app/out \;

FROM base AS final
WORKDIR /app
COPY --from=build /app/out .

# The only thing you might need to check: 
# Ensure the DLL name below EXACTLY matches your project's output name (case-sensitive)
ENTRYPOINT ["dotnet", "SalvadorLibraryNowAPI1.dll"]
