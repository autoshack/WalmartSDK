/**
Copyright (c) 2018-present, Walmart Inc.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System.IO;
using System.Text;
using Walmart.Sdk.Base.Http.Exception;

namespace Walmart.Sdk.Marketplace.V3.Api
{
    using System;
    using System.Threading.Tasks;
    using Walmart.Sdk.Base.Http;
    using Walmart.Sdk.Marketplace.V3.Payload.Order;
    using Walmart.Sdk.Base.Primitive;
    using Walmart.Sdk.Marketplace.V3.Api.Request;


    public class OrderEndpoint : ApiEndpoint, IOrderEndpoint
    {
        private enum OrderAction
        {
            Ack,
            Cancel,
            Refund,
            Shipping
        }

        public OrderEndpoint(IEndpointClient client) : base(client)
        {
            payloadFactory = new V3.Payload.PayloadFactory();
        }

        public async Task<OrdersListType> GetAllReleasedOrders(DateTime createdStartDate, DateTime createdEndDate, int limit = 200)
        {
            // to avoid deadlock if this method is executed synchronously
            await new ContextRemover();

            var request = CreateRequest();

            request.EndpointUri = BuildEndpointUrl("orders/released");

            if (limit < 1) limit = 1;
            if (limit > 200) limit = 200;

            request.QueryParams.Add("limit", limit.ToString());
            request.QueryParams.Add("createdStartDate", createdStartDate.ToString("yyyy-MM-ddTHH:mm:ss"));
            request.QueryParams.Add("createdEndDate", createdEndDate.ToString("yyyy-MM-ddTHH:mm:ss"));

            var result = await ProcessRequestTask<OrdersListType>(client.GetAsync(request));

            return result;
         
           
        }

        public async Task<OrdersListType> GetAllReleasedOrders(string nextCursor)
        {
            // to avoid deadlock if this method is executed synchronously
            await new ContextRemover();

            var request = CreateRequest();
            request.EndpointUri = BuildEndpointUrl($"orders/released{nextCursor}");
            var result = await ProcessRequestTask<OrdersListType>(client.GetAsync(request));
            return result;
        }

        public async Task<Order> GetOrderById(string purchaseOrderId)
        {
            // to avoid deadlock if this method is executed synchronously
            await new ContextRemover();

            var request = CreateRequest();
            request.EndpointUri = BuildEndpointUrl($"orders/{purchaseOrderId}");
            var result = await ProcessRequestTask<Order>(client.GetAsync(request));
            return result;
        }

        public async Task<OrdersListType> GetAllOrders(OrderFilter filter)
        {
            // to avoid deadlock if this method is executed synchronously
            await new ContextRemover();

            var request = CreateRequest();
            filter.FullfillRequest(request);
            request.EndpointUri = BuildEndpointUrl("orders");
            var result = await ProcessRequestTask<OrdersListType>(client.GetAsync(request));
            return result;
        }

        public async Task<OrdersListType> GetAllOrders(string nextCursor)
        {
            // to avoid deadlock if this method is executed synchronously
            await new ContextRemover();

            var request = CreateRequest();
            request.EndpointUri = BuildEndpointUrl($"orders/{nextCursor}");
            var result = await ProcessRequestTask<OrdersListType>(client.GetAsync(request));
            return result;
        }

        private async Task<IResponse> UpdateOrder(string purchaseOrderId, OrderAction desiredAction, string payload=null)
        {
            // to avoid deadlock if this method is executed synchronously
            await new ContextRemover();

            string action = "";
            switch (desiredAction)
            {
                case OrderAction.Ack:
                    action = "acknowledge";
                    break;
                case OrderAction.Cancel:
                    action = "cancel";
                    break;
                case OrderAction.Refund:
                    action = "refund";
                    break;
                case OrderAction.Shipping:
                    action = "shipping";
                    break;
                default:
                    throw new Base.Exception.InvalidValueException("Unknown order action provided >" + action.ToString() + "<");
            }
            var request = CreateRequest();
            if (!string.IsNullOrEmpty(payload))
            {
                request.AddPayload(payload);
            }
            request.EndpointUri = BuildEndpointUrl($"orders/{purchaseOrderId}/{action}");
            var response = await client.PostAsync(request);
            return response;
        }

        public async Task<Order> AcknowledgeOrder(string purchaseOrderId)
        {
            // to avoid deadlock if this method is executed synchronously
            await new ContextRemover();

            var result = await ProcessRequestTask<Order>(UpdateOrder(purchaseOrderId, OrderAction.Ack));
            return result;
        }

        public async Task<Order> CancelOrderLines(string purchaseOrderId, System.IO.Stream stream)
        {
            // to avoid deadlock if this method is executed synchronously
            await new ContextRemover();

            var result = await ProcessRequestTask<Order>(UpdateOrder(purchaseOrderId, OrderAction.Cancel));
            return result;
        }

        public async Task<Order> RefundOrderLines(string purchaseOrderId, System.IO.Stream stream)
        {
            // to avoid deadlock if this method is executed synchronously
            await new ContextRemover();
            var result = await ProcessRequestTask<Order>(UpdateOrder(purchaseOrderId, OrderAction.Refund));
            return result;
        }

        public async Task<Order> ShippingUpdates(string purchaseOrderId, string payload)
        {
            // to avoid deadlock if this method is executed synchronously
            await new ContextRemover();
            var result = await ProcessRequestTask<Order>(UpdateOrder(purchaseOrderId, OrderAction.Shipping,payload));
            return result;
        }

        public async Task<Order> SendShippingUpdate(string purchaseOrderId, OrderShipmentTrackingInformation shipmentTrackingInformation)
        {
            var serializer = this.payloadFactory.GetSerializer(this.config.ApiFormat);
            var serializedPayload = serializer.Serialize(shipmentTrackingInformation);
            return await ShippingUpdates(purchaseOrderId, serializedPayload);
           
        }
    }
}
