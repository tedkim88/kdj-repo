# 1️⃣ Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY RMSSystem.sln .
COPY RMSApp/*.csproj RMSApp/
COPY RMSSystem/*.csproj RMSSystem/
RUN dotnet restore

COPY . .
RUN dotnet publish RMSApp/RMSApp.csproj -c Release -o /app/publish /p:UseAppHost=false

# 2️⃣ Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Render will provide PORT environment variable
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_USE_POLLING_FILE_WATCHER=1

# 앱 실행
ENTRYPOINT ["dotnet", "RMSApp.dll", "--urls", "http://*:$PORT"]
