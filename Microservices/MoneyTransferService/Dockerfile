#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5019

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Microservices/MoneyTransferService/MoneyTransferService.csproj", "Microservices/MoneyTransferService/"]
RUN dotnet restore "Microservices/MoneyTransferService/MoneyTransferService.csproj"
COPY . .
WORKDIR "/src/Microservices/MoneyTransferService"
RUN dotnet build "MoneyTransferService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MoneyTransferService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MoneyTransferService.dll"]