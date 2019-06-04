FROM microsoft/dotnet:2.2-sdk-alpine as build
WORKDIR /build

COPY . .

RUN dotnet build -c Release ./StubApi/StubApi.csproj
RUN dotnet publish -c Release ./StubApi/StubApi.csproj

FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine
WORKDIR /app

EXPOSE 8080

COPY --from=build /build/StubApi/bin/Release/netcoreapp2.2/publish .

CMD ["dotnet", "StubApi.dll"]