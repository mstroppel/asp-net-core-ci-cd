FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY AspNetCoreSample.sln ./
COPY AspNetCoreSample/AspNetCoreSample.csproj AspNetCoreSample/
COPY AspNetCoreSample.Logic/AspNetCoreSample.Logic.csproj AspNetCoreSample.Logic/
COPY AspNetCoreSample.Logic.Tests/AspNetCoreSample.Logic.Tests.csproj AspNetCoreSample.Logic.Tests/
COPY AspNetCoreSample.Outbound/AspNetCoreSample.Outbound.csproj AspNetCoreSample.Outbound/
RUN dotnet restore AspNetCoreSample/AspNetCoreSample.csproj

COPY . .
RUN dotnet publish AspNetCoreSample/AspNetCoreSample.csproj \
    --configuration Release \
    --no-restore \
    --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "AspNetCoreSample.dll"]
