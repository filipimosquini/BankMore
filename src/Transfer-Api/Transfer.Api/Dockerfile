# Etapa de execução base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos de projeto e restaura dependências
COPY ["src/Transfer-Api/Transfer.Api/Transfer.Api.csproj", "Transfer.Api/"]
COPY ["src/Transfer-Api/Transfer.Application/Transfer.Application.csproj", "Transfer.Application/"]
COPY ["src/Transfer-Api/Transfer.Core/Transfer.Core.csproj", "Transfer.Core/"]
COPY ["src/Transfer-Api/Transfer.Infrastructure/Transfer.Infrastructure.csproj", "Transfer.Infrastructure/"]
COPY ["src/Transfer-Api/Transfer.Infrastructure.CrossCutting/Transfer.Infrastructure.CrossCutting.csproj", "Transfer.Infrastructure.CrossCutting/"]

# Restaura as dependências
RUN dotnet restore "Transfer.Api/Transfer.Api.csproj"

# Copia todo o conteúdo do diretório src para o contêiner
COPY src/Transfer-Api/ .

# Define o diretório de trabalho como o projeto principal
WORKDIR /src/Transfer.Api

# Compila o projeto em modo Release e cria a saída na pasta /app/build
RUN dotnet build "Transfer.Api.csproj" -c Release -o /app/build

# Etapa de publicação
FROM build AS publish
RUN dotnet publish "Transfer.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Define o entrypoint para executar a API
ENTRYPOINT ["dotnet", "Transfer.Api.dll"]
