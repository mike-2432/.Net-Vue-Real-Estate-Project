FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /App

COPY . ./
# ENV NUGET_XMLDOC_MODE=none
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
ENV ASPNETCORE_URLS=http://+:5000
COPY --from=build /App/out .
ENTRYPOINT ["dotnet", "server.dll"]