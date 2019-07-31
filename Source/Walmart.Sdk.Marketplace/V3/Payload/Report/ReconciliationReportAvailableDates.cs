using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Walmart.Sdk.Base.Primitive;

namespace Walmart.Sdk.Marketplace.V3.Payload.Report
{

    [XmlRoot(ElementName = "availableApReportDates")]
    public class ReconciliationReportAvailableDates:BasePayload
    {
        [XmlElement(ElementName = "availableApReportDate")]
        public List<string> AvailableReportDates { get; set; }
    }
}
