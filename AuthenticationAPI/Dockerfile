# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Base image for the runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Base image for building the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["AuthenticationAPI/AuthenticationAPI.csproj", "AuthenticationAPI/"]
RUN dotnet restore "AuthenticationAPI/AuthenticationAPI.csproj"

# Copy the rest of the application code
COPY . .

# Install EF Core CLI tools
RUN dotnet tool install --global dotnet-ef

# Ensure the tools are in the PATH
ENV PATH="${PATH}:/root/.dotnet/tools"

# Build the application
WORKDIR "/src/AuthenticationAPI"
RUN dotnet build "AuthenticationAPI.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "AuthenticationAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage: create the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthenticationAPI.dll"]
