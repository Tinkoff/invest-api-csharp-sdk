using System.Text;
using Google.Protobuf.WellKnownTypes;
using Tinkoff.InvestApi.V1;

namespace Tinkoff.InvestApi.Sample;

public class InstrumentsServiceSample
{
    private readonly InstrumentsService.InstrumentsServiceClient _service;

    public InstrumentsServiceSample(InstrumentsService.InstrumentsServiceClient service)
    {
        _service = service;
    }

    public async Task<string> GetInstrumentsDescriptionAsync(CancellationToken cancellationToken)
    {
        var shares = await _service.SharesAsync(cancellationToken);
        var etfs = await _service.EtfsAsync(cancellationToken);
        var bonds = await _service.BondsAsync(cancellationToken);
        var futures = await _service.FuturesAsync(cancellationToken);

        var dividends = new List<GetDividendsResponse>(3);
        foreach (var share in shares.Instruments.Take(dividends.Capacity))
        {
            var dividendsResponse = await _service.GetDividendsAsync(new GetDividendsRequest
            {
                Figi = share.Figi, From = share.IpoDate, To = Timestamp.FromDateTime(DateTime.UtcNow)
            }, cancellationToken: cancellationToken);
            dividends.Add(dividendsResponse);
        }

        var accruedInterests = new List<GetAccruedInterestsResponse>(3);
        foreach (var bond in bonds.Instruments.Take(accruedInterests.Capacity))
        {
            var accruedInterestsResponse = await _service.GetAccruedInterestsAsync(new GetAccruedInterestsRequest
                    {Figi = bond.Figi, From = bond.PlacementDate, To = Timestamp.FromDateTime(DateTime.UtcNow)},
                cancellationToken: cancellationToken);
            accruedInterests.Add(accruedInterestsResponse);
        }

        var futuresMargin = new List<GetFuturesMarginResponse>(3);
        foreach (var future in futures.Instruments.Take(accruedInterests.Capacity))
        {
            var futureMargin = await _service.GetFuturesMarginAsync(new GetFuturesMarginRequest {Figi = future.Figi},
                cancellationToken: cancellationToken);
            futuresMargin.Add(futureMargin);
        }

        var tradingSchedulesResponse = await _service.TradingSchedulesAsync(new TradingSchedulesRequest
        {
            Exchange = "SPB",
            From = Timestamp.FromDateTime(DateTime.UtcNow.Date.AddDays(1)),
            To = Timestamp.FromDateTime(DateTime.UtcNow.Date.AddDays(3))
        }, cancellationToken: cancellationToken);


        return new InstrumentsFormatter(shares.Instruments, etfs.Instruments, bonds.Instruments, futures.Instruments,
            dividends, accruedInterests, futuresMargin, tradingSchedulesResponse).Format();
    }

    public string GetInstrumentsDescription()
    {
        var shares = _service.Shares();
        var etfs = _service.Etfs();
        var bonds = _service.Bonds();
        var futures = _service.Futures();

        var dividends = new List<GetDividendsResponse>(3);
        foreach (var share in shares.Instruments.Take(dividends.Capacity))
        {
            var dividendsResponse = _service.GetDividends(new GetDividendsRequest
            {
                Figi = share.Figi, From = share.IpoDate, To = Timestamp.FromDateTime(DateTime.UtcNow)
            });
            dividends.Add(dividendsResponse);
        }

        var accruedInterests = new List<GetAccruedInterestsResponse>(3);
        foreach (var bond in bonds.Instruments.Take(accruedInterests.Capacity))
        {
            var accruedInterestsResponse = _service.GetAccruedInterests(new GetAccruedInterestsRequest
                {Figi = bond.Figi, From = bond.PlacementDate, To = Timestamp.FromDateTime(DateTime.UtcNow)});
            accruedInterests.Add(accruedInterestsResponse);
        }

        var futuresMargin = new List<GetFuturesMarginResponse>(3);
        foreach (var future in futures.Instruments.Take(accruedInterests.Capacity))
        {
            var futureMargin = _service.GetFuturesMargin(new GetFuturesMarginRequest {Figi = future.Figi});
            futuresMargin.Add(futureMargin);
        }

        var tradingSchedulesResponse = _service.TradingSchedules(new TradingSchedulesRequest
        {
            Exchange = "SPB",
            From = Timestamp.FromDateTime(DateTime.UtcNow.Date.AddDays(1)),
            To = Timestamp.FromDateTime(DateTime.UtcNow.Date.AddDays(3))
        });

        return new InstrumentsFormatter(shares.Instruments, etfs.Instruments, bonds.Instruments, futures.Instruments,
            dividends, accruedInterests, futuresMargin, tradingSchedulesResponse).Format();
    }


    private class InstrumentsFormatter
    {
        private readonly IReadOnlyList<GetAccruedInterestsResponse> _accruedInterests;
        private readonly IReadOnlyList<Bond> _bonds;
        private readonly IReadOnlyList<GetDividendsResponse> _dividends;
        private readonly IReadOnlyList<Etf> _etfs;
        private readonly IReadOnlyList<Future> _futures;
        private readonly IReadOnlyList<GetFuturesMarginResponse> _futuresMargin;
        private readonly IReadOnlyList<Share> _shares;
        private readonly TradingSchedulesResponse _tradingSchedulesResponse;

        public InstrumentsFormatter(IReadOnlyList<Share> shares, IReadOnlyList<Etf> etfs,
            IReadOnlyList<Bond> bonds, IReadOnlyList<Future> futures,
            IReadOnlyList<GetDividendsResponse> dividends,
            IReadOnlyList<GetAccruedInterestsResponse> accruedInterests,
            IReadOnlyList<GetFuturesMarginResponse> futuresMargin, TradingSchedulesResponse tradingSchedulesResponse)
        {
            _shares = shares;
            _etfs = etfs;
            _bonds = bonds;
            _futures = futures;
            _dividends = dividends;
            _accruedInterests = accruedInterests;
            _futuresMargin = futuresMargin;
            _tradingSchedulesResponse = tradingSchedulesResponse;
        }

        public string Format()
        {
            var stringBuilder = new StringBuilder();

            foreach (var tradingSchedule in _tradingSchedulesResponse.Exchanges)
            {
                stringBuilder.AppendFormat("Trading schedule for exchange {0}: ", tradingSchedule.Exchange)
                    .AppendLine();
                foreach (var tradingDay in tradingSchedule.Days)
                    stringBuilder.AppendFormat("- {0} {1:working;0;non-working} {2} {3}", tradingDay.Date,
                            tradingDay.IsTradingDay.GetHashCode(), tradingDay.StartTime, tradingDay.EndTime)
                        .AppendLine();
            }

            stringBuilder.AppendFormat("Loaded {0} shares", _shares.Count)
                .AppendLine();
            for (var i = 0; i < 10; i++)
            {
                var share = _shares[i];
                stringBuilder.AppendFormat("- [{0}] {1}", share.Figi, share.Name)
                    .AppendLine();
                if (i < _dividends.Count)
                {
                    var dividendsCount = Math.Min(10, _dividends[i].Dividends.Count);

                    if (dividendsCount == 0) continue;

                    stringBuilder.AppendFormat("  Dividends:").AppendLine();
                    for (var j = 0; j < dividendsCount; j++)
                    {
                        var dividend = _dividends[i].Dividends[j];
                        stringBuilder.AppendFormat("  - {0} {1} {2}", (decimal) dividend.DividendNet,
                                dividend.DividendType, dividend.DeclaredDate)
                            .AppendLine();
                    }
                }
            }

            stringBuilder.AppendLine("...").AppendLine();

            stringBuilder.AppendFormat("Loaded {0} etfs", _etfs.Count)
                .AppendLine();
            for (var i = 0; i < 10; i++)
            {
                var etf = _etfs[i];
                stringBuilder.AppendFormat("- [{0}] {1}", etf.Figi, etf.Name)
                    .AppendLine();
            }

            stringBuilder.AppendLine("...").AppendLine();

            stringBuilder.AppendFormat("Loaded {0} bonds", _bonds.Count)
                .AppendLine();
            for (var i = 0; i < 10; i++)
            {
                var bond = _bonds[i];
                stringBuilder.AppendFormat("- [{0}] {1}", bond.Figi, bond.Name)
                    .AppendLine();

                if (i < _accruedInterests.Count)
                {
                    stringBuilder.AppendFormat("  Accrued Interest:").AppendLine();
                    var accruedInterestsCount = Math.Min(_accruedInterests[i].AccruedInterests.Count, 10);
                    for (var j = 0; j < accruedInterestsCount; j++)
                    {
                        var accruedInterest = _accruedInterests[i].AccruedInterests[j];
                        stringBuilder.AppendFormat("  - {0} {1}", accruedInterest.Date,
                                (decimal) accruedInterest.Nominal)
                            .AppendLine();
                    }
                }
            }

            stringBuilder.AppendLine("...").AppendLine();

            stringBuilder.AppendFormat("Loaded {0} futures", _futures.Count)
                .AppendLine();
            for (var i = 0; i < 10; i++)
            {
                var future = _futures[i];
                stringBuilder.AppendFormat("- [{0}] {1}", future.Figi, future.Name)
                    .AppendLine();

                if (i < _futuresMargin.Count)
                {
                    stringBuilder.AppendFormat("  Initial Margin On Buy: {0}",
                        (decimal) _futuresMargin[i].InitialMarginOnBuy).AppendLine();
                    stringBuilder.AppendFormat("  Initial Margin On Sell: {0}",
                        (decimal) _futuresMargin[i].InitialMarginOnSell).AppendLine();
                }
            }

            stringBuilder.AppendLine("...");

            return stringBuilder.ToString();
        }
    }
}