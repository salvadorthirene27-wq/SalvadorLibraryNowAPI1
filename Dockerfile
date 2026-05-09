FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "SalvadorLibraryNowAPI1/SalvadorLibraryNowAPI1.csproj"
RUN dotnet publish "SalvadorLibraryNowAPI1/SalvadorLibraryNowAPI1.csproj" -c Release -o /app/out

FROM base AS final
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "SalvadorLibraryNowAPI1.dll"]
