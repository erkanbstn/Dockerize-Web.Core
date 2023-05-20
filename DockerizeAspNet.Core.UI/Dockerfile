FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build 
WORKDIR /app
COPY *.csproj . 
RUN dotnet restore
COPY . .
RUN dotnet publish DockerizeAspNet.Core.UI.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/runtime
WORKDIR /app
COPY --from=build /app/out .
ENV ASPNETCORE_URLS="https://*:4500"
ENV ASPNETCORE_ENVIRONMENT="Development"
ENTRYPOINT ["dotnet", "DockerizeAspNet.Core.UI.dll"]



