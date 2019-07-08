using System;
using System.IO;
using System.Threading.Tasks;
using Walmart.Sdk.Marketplace.V3.Api.Request;
using Walmart.Sdk.Marketplace.V3.Payload.Order;

namespace Walmart.Sdk.Marketplace.V3.Api
{
    public interface IOrderEndpoint
    {
        Task<Order> AcknowledgeOrder(string purchaseOrderId);
        Task<Order> CancelOrderLines(string purchaseOrderId, Stream stream);
        Task<OrdersListType> GetAllOrders(OrderFilter filter);
        Task<OrdersListType> GetAllOrders(string nextCursor);
        Task<OrdersListType> GetAllReleasedOrders(DateTime createdStartDate, DateTime createdEndDate, int limit = 20);
        Task<OrdersListType> GetAllReleasedOrders(string nextCursor);
        Task<Order> GetOrderById(string purchaseOrderId);
        Task<Order> RefundOrderLines(string purchaseOrderId, Stream stream);
        Task<Order> ShippingUpdates(string purchaseOrderId, string stream);

        Task<Order> SendShippingUpdate(string purchaseOrderId,
            OrderShipmentTrackingInformation shipmentTrackingInformation);
    }
}