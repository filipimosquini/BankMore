# Etapa de execução base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copia os arquivos de projeto e restaura dependências
COPY ["src/Account-api/Account.Api/Account.Api.csproj", "Account.Api/"]
COPY ["src/Account-api/Account.Application/Account.Application.csproj", "Account.Application/"]
COPY ["src/Account-api/Account.Domain/Account.Domain.csproj", "Account.Domain/"]
COPY ["src/Account-api/Account.Infrastructure/Account.Infrastructure.csproj", "Account.Infrastructure/"]
COPY ["src/Account-api/Account.Infrastructure.CrossCutting/Account.Infrastructure.CrossCutting.csproj", "Account.Infrastructure.CrossCutting/"]

# Restaura as dependências
RUN dotnet restore "Account.Api/Account.Api.csproj"

# Copia todo o conteúdo do diretório src para o contêiner
COPY src/Account-api/ .

# Define o diretório de trabalho como o projeto principal
WORKDIR /src/Account.Api

# Compila o projeto em modo Release e cria a saída na pasta /app/build
RUN dotnet build "Account.Api.csproj" -c Release -o /app/build

# Etapa de publicação
FROM build AS publish
RUN dotnet publish "Account.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Define o entrypoint para executar a API
ENTRYPOINT ["dotnet", "Account.Api.dll"]
