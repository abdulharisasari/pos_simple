# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj dan restore
COPY *.csproj ./
RUN dotnet restore

# Copy seluruh source code dan publish
COPY . ./
RUN dotnet publish -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

# Expose port default
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

# Jalankan aplikasi
ENTRYPOINT ["dotnet", "pos_simple.dll"]
