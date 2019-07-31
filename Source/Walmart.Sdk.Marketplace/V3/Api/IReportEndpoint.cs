using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Walmart.Sdk.Marketplace.V3.Payload.Report;

namespace Walmart.Sdk.Marketplace.V3.Api
{
    public interface IReportEndpoint
    {
        Task<ReconciliationReportAvailableDates> GetAvailableReconciliationReportDates();
        Task<byte[]> GetReportContent(string date);
    }
}
