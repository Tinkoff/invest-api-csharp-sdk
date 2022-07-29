Cначала нужно добавить токен в переменную окружения.
- sh
```shell
$ export TOKEN=YOUR_TOKEN
```
- cmd
```shell
$ set TOKEN=YOUR_TOKEN
```
- powershell
```pwsh
$ $env:TOKEN = "YOUR_TOKEN"
```
А потом можно запускать примеры

```shell
$ dotnet run --project ./samples/Samples.MarketDataStream
```
