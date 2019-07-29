using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Walmart.Sdk.Base.Primitive;
using Walmart.Sdk.Marketplace.Enums;

namespace Walmart.Sdk.Marketplace.V3.Payload.Order
{

    [XmlRoot(ElementName = "statusQuantity")]
    public class CancellationStatusQuantity
    {
        [XmlElement(ElementName = "unitOfMeasurement")]
        public string UnitOfMeasurement { get; set; }
        [XmlElement(ElementName = "amount")]
        public int Amount { get; set; }
    }

    [XmlRoot(ElementName = "orderLineStatus")]
    public class CancellationOrderLineStatus
    {
        [XmlElement(ElementName = "status")]
        public string Status { get; set; }
        [XmlElement(ElementName = "cancellationReason")]
        public OrderCancellationReasons CancellationReason { get; set; }
        [XmlElement(ElementName = "statusQuantity")]
        public CancellationStatusQuantity StatusQuantity { get; set; }
    }

    [XmlRoot(ElementName = "orderLineStatuses")]
    public class CancellationOrderLineStatuses
    {
        [XmlElement(ElementName = "orderLineStatus")]
        public CancellationOrderLineStatus OrderLineStatus { get; set; }
    }

    [XmlRoot(ElementName = "orderLine")]
    public class CancellationOrderLine
    {
        [XmlElement(ElementName = "lineNumber")]
        public string LineNumber { get; set; }
        [XmlElement(ElementName = "orderLineStatuses")]
        public CancellationOrderLineStatuses OrderLineStatuses { get; set; }
    }

    [XmlRoot(ElementName = "orderLines")]
    public class CancellationOrderLines
    {
        [XmlElement(ElementName = "orderLine")]
        public List<CancellationOrderLine> OrderLine { get; set; }

        public CancellationOrderLines()
        {
            OrderLine = new List<CancellationOrderLine>();
        }
    }

    [XmlRoot(ElementName = "orderCancellation", Namespace = "http://walmart.com/mp/v3/orders")]
    public class OrderCancellation:BasePayload
    {
        [XmlElement(ElementName = "orderLines")]
        public CancellationOrderLines OrderLines { get; set; }
    }

    

}
