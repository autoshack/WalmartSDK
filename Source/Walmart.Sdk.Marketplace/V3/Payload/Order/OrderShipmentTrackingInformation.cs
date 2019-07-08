using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Walmart.Sdk.Base.Primitive;
using Walmart.Sdk.Marketplace.Enums;

namespace Walmart.Sdk.Marketplace.V3.Payload.Order
{
    [XmlRoot(ElementName = "statusQuantity")]
    public class ShipmentStatusQuantity
    {
        [XmlElement(ElementName = "unitOfMeasurement")]
        public string UnitOfMeasurement { get; set; }
        [XmlElement(ElementName = "amount")]
        public int Amount { get; set; }
    }

    [XmlRoot(ElementName = "carrierName")]
    public class ShipmentCarrierName
    {
        [XmlElement(ElementName = "carrier")]
        public ShippingCarrier Carrier { get; set; }
        [XmlElement(ElementName = "otherCarrier")]
        public string OtherCarrier { get; set; }
    }

    [XmlRoot(ElementName = "trackingInfo")]
    public class ShipmentTrackingInfo
    {
        [XmlElement(ElementName = "shipDateTime")]
        public string ShipDateTime { get; set; }
        [XmlElement(ElementName = "carrierName")]
        public ShipmentCarrierName CarrierName { get; set; }
        [XmlElement(ElementName = "methodCode")]
        public ShippingMethodCode MethodCode { get; set; }
        [XmlElement(ElementName = "trackingNumber")]
        public string TrackingNumber { get; set; }
        [XmlElement(ElementName = "trackingURL")]
        public string TrackingURL { get; set; }
    }

    [XmlRoot(ElementName = "orderLineStatus")]
    public class ShipmentOrderLineStatus
    {
        [XmlElement(ElementName = "status")]
        public string Status { get; set; }
        [XmlElement(ElementName = "statusQuantity")]
        public ShipmentStatusQuantity StatusQuantity { get; set; }
        [XmlElement(ElementName = "trackingInfo")]
        public ShipmentTrackingInfo TrackingInfo { get; set; }
    }

    [XmlRoot(ElementName = "orderLineStatuses")]
    public class ShipmentOrderLineStatuses
    {
        [XmlElement(ElementName = "orderLineStatus")]
        public ShipmentOrderLineStatus OrderLineStatus { get; set; }
    }

    [XmlRoot(ElementName = "orderLine")]
    public class ShipmentOrderLine
    {
        [XmlElement(ElementName = "lineNumber")]
        public string LineNumber { get; set; }
        [XmlElement(ElementName = "orderLineStatuses")]
        public ShipmentOrderLineStatuses OrderLineStatuses { get; set; }
    }

    [XmlRoot(ElementName = "orderLines")]
    public class ShipmentOrderLines
    {
        [XmlElement(ElementName = "orderLine")]
        public List<ShipmentOrderLine> OrderLines { get; set; }


    }

    [XmlRoot(ElementName = "orderShipment",Namespace = "http://walmart.com/mp/v3/orders")]
    public class OrderShipmentTrackingInformation:BasePayload
    {

        [XmlElement(ElementName = "orderLines")]
        public ShipmentOrderLines OrderLines { get; set; }

    }

}

