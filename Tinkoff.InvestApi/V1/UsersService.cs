using Grpc.Core;

namespace Tinkoff.InvestAPI.V1;

public static partial class UsersService
{
    public partial class UsersServiceClient
    {
        private static readonly GetInfoRequest GetInfoRequest = new();
        private static readonly GetUserTariffRequest GetUserTariffRequest = new();
        private static readonly GetAccountsRequest GetAccountsRequest = new();
        private static readonly GetMarginAttributesRequest GetMarginAttributesRequest = new();

        public AsyncUnaryCall<GetInfoResponse> GetInfoAsync(CancellationToken cancellationToken = default)
        {
            return GetInfoAsync(GetInfoRequest, null, null, cancellationToken);
        }

        public GetInfoResponse GetInfo(CancellationToken cancellationToken = default)
        {
            return GetInfo(GetInfoRequest, null, null, cancellationToken);
        }

        public AsyncUnaryCall<GetUserTariffResponse> GetUserTariffAsync(CancellationToken cancellationToken = default)
        {
            return GetUserTariffAsync(GetUserTariffRequest, null, null, cancellationToken);
        }

        public GetUserTariffResponse GetUserTariff(CancellationToken cancellationToken = default)
        {
            return GetUserTariff(GetUserTariffRequest, null, null, cancellationToken);
        }

        public AsyncUnaryCall<GetAccountsResponse> GetAccountsAsync(CancellationToken cancellationToken = default)
        {
            return GetAccountsAsync(GetAccountsRequest, null, null, cancellationToken);
        }

        public GetAccountsResponse GetAccounts(CancellationToken cancellationToken = default)
        {
            return GetAccounts(GetAccountsRequest, null, null, cancellationToken);
        }

        public AsyncUnaryCall<GetMarginAttributesResponse> GetMarginAttributesAsync(
            CancellationToken cancellationToken = default)
        {
            return GetMarginAttributesAsync(GetMarginAttributesRequest, null, null, cancellationToken);
        }

        public GetMarginAttributesResponse GetMarginAttributes(CancellationToken cancellationToken = default)
        {
            return GetMarginAttributes(GetMarginAttributesRequest, null, null, cancellationToken);
        }
    }
}