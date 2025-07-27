# Use official .NET SDK image as base image to build the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CSharpKube/CSharpKube.csproj", "CSharpKube/"]
RUN dotnet restore "CSharpKube/CSharpKube.csproj"
COPY . .
WORKDIR "/src/CSharpKube"
RUN dotnet build "CSharpKube.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CSharpKube.csproj" -c Release -o /app/publish

# Final image to run the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSharpKube.dll"]
