using Grpc.Core;
using Tinkoff.InvestApi.V1;

namespace Tinkoff.InvestApi;

public class InvestApiClient
{
    public InvestApiClient(CallInvoker callInvoker)
    {
        Instruments = new InstrumentsService.InstrumentsServiceClient(callInvoker);
        MarketData = new MarketDataService.MarketDataServiceClient(callInvoker);
        MarketDataStream = new MarketDataStreamService.MarketDataStreamServiceClient(callInvoker);
        Operations = new OperationsService.OperationsServiceClient(callInvoker);
        Orders = new OrdersService.OrdersServiceClient(callInvoker);
        OrdersStream = new OrdersStreamService.OrdersStreamServiceClient(callInvoker);
        Sandbox = new SandboxService.SandboxServiceClient(callInvoker);
        StopOrders = new StopOrdersService.StopOrdersServiceClient(callInvoker);
        Users = new UsersService.UsersServiceClient(callInvoker);
    }

    public InstrumentsService.InstrumentsServiceClient Instruments { get; }
    public MarketDataService.MarketDataServiceClient MarketData { get; }
    public MarketDataStreamService.MarketDataStreamServiceClient MarketDataStream { get; }
    public OperationsService.OperationsServiceClient Operations { get; }
    public OrdersService.OrdersServiceClient Orders { get; }
    public OrdersStreamService.OrdersStreamServiceClient OrdersStream { get; set; }
    public SandboxService.SandboxServiceClient Sandbox { get; }
    public StopOrdersService.StopOrdersServiceClient StopOrders { get; }
    public UsersService.UsersServiceClient Users { get; }
}