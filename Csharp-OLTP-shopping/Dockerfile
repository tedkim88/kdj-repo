# ---------------------------
# 1️⃣ Build 단계
# ---------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# 솔루션 파일 복사
COPY DMIT2018Project.sln ./

# 프로젝트 csproj 복사 (라이브러리 포함)
COPY DMIT2018Project/*.csproj ./DMIT2018Project/
COPY POSystem/*.csproj ./POSystem/
COPY ReceivingSystem/*.csproj ./ReceivingSystem/
COPY RentalSystem/*.csproj ./RentalSystem/
COPY SalesSystem/*.csproj ./SalesSystem/
COPY ServiceSystem/*.csproj ./ServiceSystem/

# NuGet 패키지 복원
RUN dotnet restore

# 전체 소스 복사
COPY . .

# Blazor 앱 publish
RUN dotnet publish DMIT2018Project/DMIT2018Project.csproj -c Release -o out

# ---------------------------
# 2️⃣ Runtime 단계
# ---------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Publish 결과 복사
COPY --from=build /app/out/ ./

# 컨테이너 포트 열기
EXPOSE 10000

# 앱 실행
ENTRYPOINT ["dotnet", "DMIT2018Project.dll"]
