using Grpc.Core;

namespace Tinkoff.InvestApi.V1;

public partial class InstrumentsService
{
    public partial class InstrumentsServiceClient
    {
        private static readonly InstrumentsRequest InstrumentsRequest = new();

        public AsyncUnaryCall<BondsResponse> BondsAsync(CancellationToken cancellationToken = default)
        {
            return BondsAsync(InstrumentsRequest, null, null, cancellationToken);
        }

        public BondsResponse Bonds(CancellationToken cancellationToken = default)
        {
            return Bonds(InstrumentsRequest, null, null, cancellationToken);
        }

        public AsyncUnaryCall<FuturesResponse> FuturesAsync(CancellationToken cancellationToken = default)
        {
            return FuturesAsync(InstrumentsRequest, null, null, cancellationToken);
        }

        public FuturesResponse Futures(CancellationToken cancellationToken = default)
        {
            return Futures(InstrumentsRequest, null, null, cancellationToken);
        }

        public AsyncUnaryCall<EtfsResponse> EtfsAsync(CancellationToken cancellationToken = default)
        {
            return EtfsAsync(InstrumentsRequest, null, null, cancellationToken);
        }

        public EtfsResponse Etfs(CancellationToken cancellationToken = default)
        {
            return Etfs(InstrumentsRequest, null, null, cancellationToken);
        }

        public AsyncUnaryCall<SharesResponse> SharesAsync(CancellationToken cancellationToken = default)
        {
            return SharesAsync(InstrumentsRequest, null, null, cancellationToken);
        }

        public SharesResponse Shares(CancellationToken cancellationToken = default)
        {
            return Shares(InstrumentsRequest, null, null, cancellationToken);
        }
    }
}