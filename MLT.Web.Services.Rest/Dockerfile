#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MLT.Web.Services.Rest/MLT.Web.Services.Rest.csproj", "MLT.Web.Services.Rest/"]
COPY ["MLT.Domain.Contracts/MLT.Domain.Contracts.csproj", "MLT.Domain.Contracts/"]
COPY ["MLT.Data.Contracts/MLT.Data.Contracts.csproj", "MLT.Data.Contracts/"]
COPY ["MLT.Web.Contracts/MLT.Web.Contracts.csproj", "MLT.Web.Contracts/"]
COPY ["MLT.Infrastructure/MLT.Infrastructure.csproj", "MLT.Infrastructure/"]
COPY ["MLT.Data.Repositories/MLT.Data.Repositories.csproj", "MLT.Data.Repositories/"]
COPY ["MLT.Desktop.AppUsers.Contracts/MLT.Desktop.AppUsers.Contracts.csproj", "MLT.Desktop.AppUsers.Contracts/"]
COPY ["MLT.Domain.Services/MLT.Domain.Services.csproj", "MLT.Domain.Services/"]
RUN dotnet restore "MLT.Web.Services.Rest/MLT.Web.Services.Rest.csproj"
COPY . .
WORKDIR "/src/MLT.Web.Services.Rest"
RUN dotnet build "MLT.Web.Services.Rest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MLT.Web.Services.Rest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MLT.Web.Services.Rest.dll"]