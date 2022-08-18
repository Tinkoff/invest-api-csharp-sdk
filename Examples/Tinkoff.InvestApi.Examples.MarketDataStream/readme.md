#### Кратко о примере
Пример показывает, как можно работать со стримами.

#### Подробнее
1. Подписка на обновление раз в минуту свечей определённого инструмента
2. Вывод информации о новой свече в формате json, как только она получена

#### Как запустить
1. Добавьте ваш токен в переменную окружения `TOKEN`
а. В Меню выберите Debug, затем Tinkoff.InvestApi.Examples.MarketDataStream Debug Properies
b. В появившемся окне Launch Properties в поле Environments variables добавьте ваш токен в формате Token=value
2. `dotnet run -p ./Examples/Tinkoff.InvestApi.Examples.MarketDataStream`