# Etapa de execução base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copia os arquivos de projeto e restaura dependências
COPY ["src/Authentication-api/Authentication.Api/Authentication.Api.csproj", "Authentication.Api/"]
COPY ["src/Authentication-api/Authentication.Application/Authentication.Application.csproj", "Authentication.Application/"]
COPY ["src/Authentication-api/Authentication.Domain/Authentication.Domain.csproj", "Authentication.Domain/"]
COPY ["src/Authentication-api/Authentication.Infrastructure/Authentication.Infrastructure.csproj", "Authentication.Infrastructure/"]
COPY ["src/Authentication-api/Authentication.Infrastructure.CrossCutting/Authentication.Infrastructure.CrossCutting.csproj", "Authentication.Infrastructure.CrossCutting/"]

# Restaura as dependências
RUN dotnet restore "Authentication.Api/Authentication.Api.csproj"

# Copia todo o conteúdo do diretório src para o contêiner
COPY src/Authentication-api/ .

# Define o diretório de trabalho como o projeto principal
WORKDIR /src/Authentication.Api

# Compila o projeto em modo Release e cria a saída na pasta /app/build
RUN dotnet build "Authentication.Api.csproj" -c Release -o /app/build

# Etapa de publicação
FROM build AS publish
RUN dotnet publish "Authentication.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Define o entrypoint para executar a API
ENTRYPOINT ["dotnet", "Authentication.Api.dll"]
