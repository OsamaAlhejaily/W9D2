FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Show the build environment
RUN dotnet --info

# Copy project files
COPY ["CommentService/CommentService.csproj", "CommentService/"]
COPY ["PostService/PostService.csproj", "PostService/"]

# List copied files
RUN ls -la CommentService/
RUN ls -la PostService/

# Try restore with verbose logging
RUN dotnet restore "CommentService/CommentService.csproj" --verbosity detailed

# Copy everything else
COPY . .

# List project files
RUN ls -la

# Try building with verbose output
WORKDIR "/src/CommentService"
RUN dotnet build "CommentService.csproj" -c Release -o /app/build --verbosity detailed

# Try publishing with verbose output
RUN dotnet publish -c release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CommentService.dll"]