# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем файлы проекта
COPY test_project.csproj .
RUN dotnet restore

# Копируем весь код и собираем приложение
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Этап запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Копируем собранное приложение
COPY --from=build /app/publish .

# Запускаем приложение
ENTRYPOINT ["dotnet", "test_project.dll"]