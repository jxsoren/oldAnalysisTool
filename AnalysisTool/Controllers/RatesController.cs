using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using AnalysisTool.Models;
using System.Linq;
using System.Net.Http.Json;

namespace AnalysisTool.Controllers
{
    public class RatesController : Controller
    {
        public HttpClient client = new HttpClient();
        public Shipments shipments = new();
        public List<int> serviceIds = new();
        public Dictionary<int, float[,]> tables = new Dictionary<int, float[,]>();
        public int maxWeight;
        public byte[] archiveFile;
        public byte[] data;
        public string customer = "";
        public string carrier = "";
        public List<InMemoryFile> files = new();

        public async Task ServicesRequest(string cust, string apiKey, string carr, int weight)
        {
            customer = cust.Trim();
            apiKey = apiKey.Trim();
            maxWeight = weight;
            carrier = carr.Trim();

            client.BaseAddress = new Uri("https://api.essentialhub.com/");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            var result = await client.GetStringAsync("api/v2/services?category=shipping");
            JObject j = JObject.Parse(result);

            var h = j.Children();
            var i = h.Children();
            var k = i.Children();

            foreach (var child in k)
            {
                if (child.Values().Contains(carrier))
                {
                    int s = child.Value<int>("service_id");
                    serviceIds.Add(s);
                }
            }
        }

        public async Task RatesRequests()
        {
            List<Shipments.Root> roots = new()
            {
                shipments.shipment1,
                shipments.shipment2,
                shipments.shipment3,
                shipments.shipment4,
                shipments.shipment5,
                shipments.shipment6,
                shipments.shipment7,
                shipments.shipment8
            };

            foreach (var service in serviceIds)
            {
                tables.Add(service, new float[maxWeight + 1, 9]);
            }
            var zone = 1;

            var iterations = new List<int>();
            for (int i = 1; i < maxWeight + 1; i++)
            {
                iterations.Add(i);
            }

            foreach (var shipment in roots)
            {
                var shipmentList = new List<Shipments.Root>();

                string label = shipment.shipment.label_format;

                foreach (int i in iterations)
                {
                    Shipments.Root ship = (Shipments.Root)shipments.Clone();
                    ship.shipment = new Shipments.Shipment();
                    Shipments.Parcel parcel = (Shipments.Parcel)shipments.CloneParcel(shipment.shipment.parcels[0]);

                    ship.shipment.include_services = serviceIds;
                    ship.shipment.label_format = label;
                    ship.shipment.parcels = new()
                    {
                        parcel
                    };
                    ship.shipment.parcels[0].weight = i * 16;
                    ship.shipment.to_location = shipment.shipment.to_location;
                    ship.shipment.from_location = shipment.shipment.from_location;

                    shipmentList.Add(ship);
                }

                var results = await GetRatesInParallelFixed(shipmentList);

                var w = 16;

                foreach (var result in results)
                {
                    string response = "";

                    if (result != null)
                    {
                        response = result.Content.ReadAsStringAsync().Result;
                    }

                    JObject r = JObject.Parse(response ?? "didn't work");
                    var s = r.Children();
                    var t = s.Children();
                    var u = t.Children();

                    foreach (var child in u)
                    {
                        try
                        {
                            if (child.Value<string>("carrier_code") == carrier) // make sure to change the carrier to match the service ids
                            {
                                var x = child.Value<int>("service_id");
                                string y;
                                if (child.Value<string>("rate") == null)
                                {
                                    y = "0";
                                }
                                else
                                {
                                    y = child.Value<decimal>("rate").ToString();
                                }

                                tables[x][int.Parse((w / 16).ToString()) - 1, int.Parse(zone.ToString()) - 1] = float.Parse(y);

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }


                    }
                    w += 16;

                }
                Console.WriteLine(zone.ToString());
                zone++;
            }



            foreach (var t in tables)
            {
                StringBuilder serviceTable = new StringBuilder();
                serviceTable.Append("weight,1,2,3,4,5,6,7,8");
                for (var i = 0; i < maxWeight; i++)
                {
                    serviceTable.Append("\r\n");
                    serviceTable.Append($"{i + 1},{t.Value[i, 0]},{t.Value[i, 1]},{t.Value[i, 2]},{t.Value[i, 3]},{t.Value[i, 4]},{t.Value[i, 5]},{t.Value[i, 6]},{t.Value[i, 7]}");
                }

                string name = $"{customer} rate verification {carrier} {t.Key}.csv";
                byte[] bytes = Encoding.UTF8.GetBytes(serviceTable.ToString());

                InMemoryFile sheet = new InMemoryFile()
                {
                    FileName = name,
                    Content = bytes,
                };

                files.Add(sheet);
            }
            Console.WriteLine("Done!");



            ByteArrayZip();
        }

        public async Task<IEnumerable<HttpResponseMessage>> GetRatesInParallelFixed(IEnumerable<Shipments.Root> shipments)
        {
            var results = new List<HttpResponseMessage>();
            var batchSize = 70;
            int numberOfBatches = (int)Math.Ceiling((double)shipments.Count() / batchSize);

            for (int i = 0; i < numberOfBatches; i++)
            {
                var currentShipments = shipments.Skip(i * batchSize).Take(batchSize);
                var tasks = currentShipments.Select(shipment => client.PostAsJsonAsync("api/v2/rates", shipment));
                results.AddRange(await Task.WhenAll(tasks));
            }

            return results;
        }

        public void ByteArrayZip()
        {
            using (var archiveStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        var zipArchiveEntry = archive.CreateEntry(file.FileName, CompressionLevel.Fastest);

                        using var zipStream = zipArchiveEntry.Open();
                        zipStream.Write(file.Content, 0, file.Content.Length);
                    }
                }

                archiveFile = archiveStream.ToArray();
            }
        }

        public class InMemoryFile
        {
            public string FileName { get; set; }
            public byte[] Content { get; set; }
        }
    }
}
