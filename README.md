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

### Сборка из исходников

Если вы хотите сами собрать пакет из исходников, а не использовать Nuget, вам понадобится:
- git
- dotnet SDK 6.0.

Основной код сервисов генерируется по .proto файлам при сборке, для этого используются source generators. Proto файлы лежат в submodule. Поэтому необходимо клонировать репозиторий вместе с submodule, это можно сделать командой:

```
git clone --recurse-submodules https://github.com/Tinkoff/invest-api-csharp-sdk.git
```

Затем перейдите в директорию проекта и выполните следующую команду:

```bash
dotnet build -c Release
```

## Документация

Подробную документацию по InvestApi можно найти по [ссылке](https://tinkoff.github.io/investAPI/).

### Быстрый старт

Для взаимодействия к InvestApi используется класс `InvestApiClient`, который является фасадом для grpc сервисов. Есть несколько способов его создания.

#### Создание через фабрику

Этот способ подходит для быстрого старта или для приложений без [Generic Host](https://docs.microsoft.com/ru-ru/aspnet/core/fundamentals/host/generic-host), например WinForms или консольные приложения в старом стиле.

```csharp
var client = InvestApiClientFactory.Create("<token>");
```

#### Создание через DI

Для проектов, использующих [Generic Host](https://docs.microsoft.com/ru-ru/aspnet/core/fundamentals/host/generic-host), например AspNetCore или консольные приложения в новом стиле, можно зарегистрировать InvestApiClient в DI.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddInvestApiClient((_, settings) => settings.AccessToken = "<token>");
}
```

Далее InvestApiClient можно внедрить непосредственно в места использования.

```csharp
class TradingBot 
{
    public TradingBot(InvestApiClient investApiClient)
    {
    }
}
```

### Примеры

#### Примеры по отдельным функциям

Перед запуском, нужно добавить токен в переменную окружения `TOKEN`

- [Получение информации о рынке в реальном времени](Examples/Tinkoff.InvestApi.Examples.MarketDataStream)

#### Сложное приложение

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
