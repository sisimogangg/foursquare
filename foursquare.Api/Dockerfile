FROM microsoft/dotnet:2.1.402-sdk AS build

WORKDIR /code

COPY . .

RUN dotnet restore

RUN dotnet publish --output /output --configuration Release

FROM microsoft/dotnet:2.1.4-aspnetcore-runtime-alpine

COPY --from=build /output /app

WORKDIR /app

ENTRYPOINT [ "dotnet", "foursquareApi.dll" ]