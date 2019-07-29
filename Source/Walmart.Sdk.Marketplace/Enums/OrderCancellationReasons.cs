using System;
using System.Collections.Generic;
using System.Text;

namespace Walmart.Sdk.Marketplace.Enums
{
    public enum OrderCancellationReasons
    {
        CUSTOMER_REQUESTED_SELLER_TO_CANCEL,
        SELLER_CANCEL_OUT_OF_STOCK,
        SELLER_CANCEL_CUSTOMER_DUPLICATE_ORDER,
        SELLER_CANCEL_CUSTOMER_CHANGE_ORDER,
        SELLER_CANCEL_CUSTOMER_INCORRECT_ADDRESS,
        SELLER_CANCEL_FRAUD_STOP_SHIPMENT,
        SELLER_CANCEL_PRICING_ERROR,

    }
}
