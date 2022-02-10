using System.Text;
using Google.Protobuf.WellKnownTypes;
using Tinkoff.InvestAPI.V1;

namespace Tinkoff.InvestApi.Sample;

public class OperationsServiceSample
{
    private readonly InvestApiClient _investApiClient;

    public OperationsServiceSample(InvestApiClient investApiClient)
    {
        _investApiClient = investApiClient;
    }

    public async Task<string> GetOperationsDescriptionAsync(CancellationToken cancellationToken)
    {
        var accounts = await _investApiClient.Users.GetAccountsAsync();
        var accountId = accounts.Accounts.First().Id;

        var operations = _investApiClient.Operations;
        var portfolio = await operations.GetPortfolioAsync(new PortfolioRequest {AccountId = accountId},
            cancellationToken: cancellationToken);

        var positions = await operations.GetPositionsAsync(new PositionsRequest {AccountId = accountId},
            cancellationToken: cancellationToken);

        var withdrawLimits =
            await operations.GetWithdrawLimitsAsync(new WithdrawLimitsRequest {AccountId = accountId},
                cancellationToken: cancellationToken);

        var operationsResponse = await operations.GetOperationsAsync(new OperationsRequest
        {
            AccountId = accountId,
            From = Timestamp.FromDateTime(DateTime.UtcNow.AddMonths(-1)),
            To = Timestamp.FromDateTime(DateTime.UtcNow)
        }, cancellationToken: cancellationToken);

        return new OperationsFormatter(portfolio, positions, operationsResponse, withdrawLimits).Format();
    }

    public string GetOperationsDescription()
    {
        var accounts = _investApiClient.Users.GetAccounts();
        var accountId = accounts.Accounts.First().Id;

        var operations = _investApiClient.Operations;
        var portfolio = operations.GetPortfolio(new PortfolioRequest {AccountId = accountId});

        var positions = operations.GetPositions(new PositionsRequest {AccountId = accountId});

        var withdrawLimits = operations.GetWithdrawLimits(new WithdrawLimitsRequest {AccountId = accountId});

        var operationsResponse = operations.GetOperations(new OperationsRequest
        {
            AccountId = accountId,
            From = Timestamp.FromDateTime(DateTime.UtcNow.AddMonths(-1)),
            To = Timestamp.FromDateTime(DateTime.UtcNow)
        });

        return new OperationsFormatter(portfolio, positions, operationsResponse, withdrawLimits).Format();
    }

    private class OperationsFormatter
    {
        private readonly OperationsResponse _operations;
        private readonly PortfolioResponse _portfolio;
        private readonly PositionsResponse _positions;
        private readonly WithdrawLimitsResponse _withdrawLimits;

        public OperationsFormatter(PortfolioResponse portfolio, PositionsResponse positions,
            OperationsResponse operations, WithdrawLimitsResponse withdrawLimits)
        {
            _portfolio = portfolio;
            _positions = positions;
            _operations = operations;
            _withdrawLimits = withdrawLimits;
        }

        public string Format()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Portfolio:")
                .AppendFormat("- Shares: {0} {1}", (decimal) _portfolio.TotalAmountShares,
                    _portfolio.TotalAmountShares.Currency)
                .AppendLine()
                .AppendFormat("- Bonds: {0} {1}", (decimal) _portfolio.TotalAmountBonds,
                    _portfolio.TotalAmountBonds.Currency)
                .AppendLine()
                .AppendFormat("- Etf: {0} {1}", (decimal) _portfolio.TotalAmountEtf,
                    _portfolio.TotalAmountEtf.Currency)
                .AppendLine()
                .AppendFormat("- Currencies: {0} {1}", (decimal) _portfolio.TotalAmountCurrencies,
                    _portfolio.TotalAmountCurrencies.Currency)
                .AppendLine()
                .AppendFormat("- Futures: {0} {1}", (decimal) _portfolio.TotalAmountFutures,
                    _portfolio.TotalAmountFutures.Currency)
                .AppendLine()
                .AppendFormat("- Expected yield: {0}", (decimal) _portfolio.ExpectedYield)
                .AppendLine()
                .AppendLine();

            if (_withdrawLimits.Money.Any())
            {
                stringBuilder.AppendLine().AppendLine("Withdraw limits:");
                foreach (var value in _withdrawLimits.Money)
                    stringBuilder.AppendFormat("- {0} {1}", (decimal) value, value.Currency)
                        .AppendLine();
            }

            if (_positions.Securities.Any())
            {
                stringBuilder.AppendLine().AppendLine("Positions:");
                foreach (var security in _positions.Securities)
                    stringBuilder.AppendFormat("- [{0}] {1}", security.Figi, security.Balance)
                        .AppendLine();
            }

            if (_operations.Operations.Any())
            {
                stringBuilder.AppendLine().AppendLine("Operations:");
                foreach (var operation in _operations.Operations)
                    stringBuilder.AppendFormat("- [{0}] {1} {2} {3}", operation.Figi, operation.Date,
                            (decimal) operation.Payment, operation.Currency)
                        .AppendLine();
            }


            return stringBuilder.ToString();
        }
    }
}