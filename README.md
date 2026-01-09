# Инструкция по запуску проекта "Сервис для раскладов Таро"

cd "C:\Users\Ardor\Desktop\Сервис\test_project"

docker-compose up -d


## Проверка работы приложения

- **Swagger UI:** http://localhost:8080
- **Health Check:** http://localhost:8080/health

## Регистрация нового пользователя
{
    username = "admin"
    email = "admin@gmail.com"
    password = "admin123"
}

## Примеры использования

## Создание колоды (требуется авторизация)
{
    name = "Колода Таро Райдера-Уэйта"
    description = "Классическая колода для гадания"
    author = "Артур Эдвард Уэйт"
} 

## Создание карты
{
    name = "Дурак"
    description = "Первая карта Старших Арканов"
    suit = "Старшие Арканы"
    number = 0
    uprightMeaning = "Новые начинания, невинность, спонтанность"
    reversedMeaning = "Безрассудство, риск, отсутствие направления"
    deckId = 1
} 

## Технологический стек

- ASP.NET Core 8.0 Web API
- PostgreSQL 16
- Redis 7
- Docker + Docker Compose
- Liquibase (миграции БД)
- Entity Framework Core (CRUD)
- Dapper 
- Swagger
- JWT Bearer Authentication
- API Key Authentication
- Health Checks



