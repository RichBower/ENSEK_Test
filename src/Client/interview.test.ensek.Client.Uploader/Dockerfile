#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Client/interview.test.ensek.Client.Uploader/interview.test.ensek.Client.Uploader.csproj", "src/Client/interview.test.ensek.Client.Uploader/"]
RUN dotnet restore "src/Client/interview.test.ensek.Client.Uploader/interview.test.ensek.Client.Uploader.csproj"
COPY . .
WORKDIR "/src/src/Client/interview.test.ensek.Client.Uploader"
RUN dotnet build "interview.test.ensek.Client.Uploader.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "interview.test.ensek.Client.Uploader.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "interview.test.ensek.Client.Uploader.dll"]