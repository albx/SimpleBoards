#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/SimpleBoards.Identity/SimpleBoards.Identity.csproj", "src/SimpleBoards.Identity/"]
COPY ["src/SimpleBoards.Core.Identity/SimpleBoards.Core.Identity.csproj", "src/SimpleBoards.Core.Identity/"]
RUN dotnet restore "src/SimpleBoards.Identity/SimpleBoards.Identity.csproj"
COPY . .
WORKDIR "/src/src/SimpleBoards.Identity"
RUN dotnet build "SimpleBoards.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleBoards.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleBoards.Identity.dll"]
