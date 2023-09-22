#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Lazar.Presentation.WebApi/Lazar.Presentation.WebApi.csproj", "src/Lazar.Presentation.WebApi/"]
COPY ["src/Lazar.Domain.Core/Lazar.Domain.Core.csproj", "src/Lazar.Domain.Core/"]
COPY ["src/CommonUtils/CommonUtils.csproj", "src/CommonUtils/"]
COPY ["src/Lazar.Infrastructure.Data.Ef/Lazar.Infrastructure.Data.Ef.csproj", "src/Lazar.Infrastructure.Data.Ef/"]
COPY ["src/Lazar.Domain.Interfaces/Lazar.Domain.Interfaces.csproj", "src/Lazar.Domain.Interfaces/"]
COPY ["src/Lazar.Infrastructure.JwtAuth/Lazar.Infrastructure.JwtAuth.csproj", "src/Lazar.Infrastructure.JwtAuth/"]
COPY ["src/Lazar.Services/Lazar.Services.csproj", "src/Lazar.Services/"]
COPY ["src/Lazar.Infrastructure.Mapper/Lazar.Infrastructure.Mapper.csproj", "src/Lazar.Infrastructure.Mapper/"]
COPY ["src/Lazar.Services.Contracts/Lazar.Services.Contracts.csproj", "src/Lazar.Services.Contracts/"]
COPY ["src/Lazar.Srevices.Iterfaces/Lazar.Srevices.Iterfaces.csproj", "src/Lazar.Srevices.Iterfaces/"]
RUN dotnet restore "src/Lazar.Presentation.WebApi/Lazar.Presentation.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Lazar.Presentation.WebApi"
RUN dotnet build "Lazar.Presentation.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lazar.Presentation.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lazar.Presentation.WebApi.dll"]