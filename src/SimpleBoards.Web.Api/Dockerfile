#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/SimpleBoards.Web.Api/SimpleBoards.Web.Api.csproj", "src/SimpleBoards.Web.Api/"]
COPY ["src/SimpleBoards.Persistence.SqlServer/SimpleBoards.Persistence.SqlServer.csproj", "src/SimpleBoards.Persistence.SqlServer/"]
COPY ["src/SimpleBoards.Core/SimpleBoards.Core.csproj", "src/SimpleBoards.Core/"]
COPY ["src/SimpleBoards.Web.Models/SimpleBoards.Web.Models.csproj", "src/SimpleBoards.Web.Models/"]
RUN dotnet restore "src/SimpleBoards.Web.Api/SimpleBoards.Web.Api.csproj"
COPY . .
WORKDIR "/src/src/SimpleBoards.Web.Api"
RUN dotnet build "SimpleBoards.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleBoards.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleBoards.Web.Api.dll"]
