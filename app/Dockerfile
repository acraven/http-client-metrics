FROM microsoft/dotnet:2.2-sdk-alpine as build
WORKDIR /build

COPY . .

RUN dotnet build -c Release ./DemoApi/DemoApi.csproj
RUN dotnet publish -c Release ./DemoApi/DemoApi.csproj

FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine
WORKDIR /app

EXPOSE 8090

COPY --from=build /build/DemoApi/bin/Release/netcoreapp2.2/publish .

CMD ["dotnet", "DemoApi.dll"]