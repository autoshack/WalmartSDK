using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walmart.Sdk.Base.Primitive;
using Walmart.Sdk.Marketplace.V3.Payload;
using Walmart.Sdk.Marketplace.V3.Payload.Report;

namespace Walmart.Sdk.Marketplace.V3.Api
{
    public class ReportEndpoint: ApiEndpoint, IReportEndpoint
    {
        public ReportEndpoint(IEndpointClient apiClient) : base(apiClient)
        {
            payloadFactory = new PayloadFactory();
        }

        public async Task<ReconciliationReportAvailableDates> GetAvailableReconciliationReportDates()
        {
            // to avoid deadlock if this method is executed synchronously
            await new ContextRemover();

            var request = CreateRequest();
            request.EndpointUri = BuildEndpointUrl("report/reconreport/availableReconFiles");
            var result = await ProcessRequestTask<ReconciliationReportAvailableDates>(client.GetAsync(request));
            return result;
        }

        public async Task<byte[]> GetReportContent(string date)
        {
            await new ContextRemover();

            var request = CreateRequest();
            request.EndpointUri = BuildEndpointUrl($"report/reconreport/reconFile?reportDate={date}");
            var response  = await client.GetAsync(request);
            var zippedByteArray= await response.RawResponse.Content.ReadAsByteArrayAsync();
            return UnZip(zippedByteArray);
        }

        private static byte[] UnZip(byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            using (var zipArchive = new ZipArchive(compressedStream))
            using(var memoryStream = new MemoryStream())
            {
                var entry = zipArchive.Entries.FirstOrDefault() ??
                            throw new System.Exception("Zip Archive does not contain entries");
                using (var entryStream = entry.Open())
                {
                    entryStream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }

    }
}
