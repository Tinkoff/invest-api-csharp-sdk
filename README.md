# InvestApi .NET SDK

[![Build](https://github.com/Tinkoff/invest-api-csharp-sdk/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Tinkoff/invest-api-csharp-sdk/actions/workflows/dotnet.yml)
[![NuGet version (Tinkoff Trading OpenApi)](https://img.shields.io/nuget/v/Tinkoff.InvestApi.svg)](https://www.nuget.org/packages/Tinkoff.InvestApi/)
[![NuGet version (Tinkoff Trading OpenApi)](https://img.shields.io/nuget/dt/Tinkoff.InvestApi.svg)](https://www.nuget.org/packages/Tinkoff.InvestApi/)

Данный проект представляет собой инструментарий на языке C# для работы с grpc-интерфейсом торговой
платформы [Тинькофф Инвестиции](https://www.tinkoff.ru/invest/).

## Начало работы

### Nuget

SDK [доступен](https://www.nuget.org/packages/Tinkoff.InvestApi/) на nuget.org, для подключения добавьте в проект
зависимость Tinkoff.InvestApi.

### Сборка

Для сборки вам потребуется dotnet SDK 6.0. Перейдите в директорию проекта и выполните следующую команду:

```bash
dotnet build -c Release
```
Основной код сервисов генерируется по .proto файлам при сборке, для этого используются source generators. 

## Документация

Подробную документацию по InvestApi можно найти по [ссылке](https://tinkoff.github.io/investAPI/).

### Быстрый старт

Для взаимодействия с InvestApi нужно зарегистрировать InvestApiClient, который является фасадом для grpc сервисов.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddInvestApiClient((_, settings) => settings.AccessToken = "<token>");
}
```

InvestApiClient можно внедрить непосредственно в места использования.

### Примеры

Проект [Tinkoff.InvestApi.Sample](Tinkoff.InvestApi.Sample) является примером использования SDK. При запуске будут
использованы методы InvestApi, результаты будут выведены в консоль. Перед запуском сохраните свой токен
в [user secrets](https://docs.microsoft.com/ru-ru/aspnet/core/security/app-secrets):

```bash
cd Tinkoff.InvestApi.Sample
dotnet user-secrets set "AccessToken" "<token>"
```

### Где взять токен аутентификации?

В разделе инвестиций вашего [личного кабинета tinkoff](https://www.tinkoff.ru/invest/)

* Перейдите в настройки
* Проверьте, что функция “Подтверждение сделок кодом” отключена
* Выпустите токен для торговли на бирже и режима “песочницы” (sandbox)
* Скопируйте токен и сохраните, токен отображается только один раз, просмотреть его позже не получится, тем не менее вы
  можете выпускать неограниченное количество токенов

### У меня есть вопрос

[Основной репозиторий с документацией](https://github.com/Tinkoff/investAPI/) — в нем вы можете задать
вопрос в Issues и получать информацию о релизах в Releases. Если возникают вопросы по данному SDK, нашёлся баг или есть
предложения по улучшению, то можно задать его в Issues.
