using AzureCosmosDB.Models.CosmosDB;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AzureCosmosDB.Data
{
    public class SampleServices
    {
        private const string EndpointUri = "https://kciprojecttrackerdevdb.documents.azure.com:443/";
        private const string PrimaryKey = "6bjCKmU1hwQFn9sGJG7w1f9OLXsthoFCvs7hTWT5lYfLAw9UJMiHIqhz0qkUIt4OnyphokoJk0SWokmxi0m1cg==";
        public static string DatabaseId { get; private set; } = "ToDoList";
        public static string ToDoItemsId { get; private set; } = "ToDoItems";
        private static DocumentClient client;
        private static Uri myStoreCollectionUri => UriFactory.CreateDocumentCollectionUri(DatabaseId, ToDoItemsId);

        public static async Task GetStartedDemoAsync()
        {


            client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
            await client.CreateDatabaseIfNotExistsAsync(new Database { Id = DatabaseId });
            await client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(DatabaseId), new DocumentCollection { Id = ToDoItemsId });

            Family andersenFamily = new Family
            {
                Id = "Andersen.1",
                LastName = "Andersen",
                Parents = new Parent[]
        {
                new Parent { FirstName = "Thomas" },
                new Parent { FirstName = "Mary Kay" }
        },
                Children = new Child[]
        {
                new Child
                {
                        FirstName = "Henriette Thaulow",
                        Gender = "female",
                        Grade = 5,
                        Pets = new Pet[]
                        {
                                new Pet { GivenName = "Fluffy" }
                        }
                }
        },
                Address = new Address { State = "WA", County = "King", City = "Seattle" },
                IsRegistered = true
            };



            try
            {
                await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, ToDoItemsId, andersenFamily.Id));

            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    var x = await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, ToDoItemsId), andersenFamily);
                }
                else
                {
                    throw;
                }
            }
        }

        public static void getData()
        {
            using (var client = new DocumentClient(new Uri(EndpointUri), PrimaryKey))
            {
                var response = client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri("ToDoList", "ToDoItems"), "select * from c").ToList();
                var document = response.FirstOrDefault();
                var id = document.id;
                var self = document._self;
            }

        }

        public static async Task ReplaceFamilyDocument()
        {
            Family andersenFamily = new Family
            {
                Id = "Andersen.1",

                LastName = "Andersen",
                Parents = new Parent[]
       {
                new Parent { FirstName = "Thomas" },
                new Parent { FirstName = "Mary Kay" }
       },
                Children = new Child[]
       {
                new Child
                {
                        FirstName = "Henriette Thaulow",
                        Gender = "female",
                        Grade = 5,
                        Pets = new Pet[]
                        {
                                new Pet { GivenName = "Fluffy" }
                        }
                }
       },
                Address = new Address { State = "WA", County = "King", City = "Seattle" },
                IsRegistered = true
            };
            andersenFamily.LastName = "Venkat";

            await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, ToDoItemsId, "Andersen.1"), andersenFamily);

        }

        public static async Task DeleteFamilyDocument()
        {
            await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, ToDoItemsId, "Andersen.1"));
        }


        public static async Task BulkInsert()
        {
            List<Family> docs = new List<Family>();
            for (int i = 100; i < 110; i++)
            {
                Family item = new Family
                {
                    Id = "Andersen." + i,
                    LastName = "Andersen" + i,
                    Parents = new Parent[]
    {
                new Parent { FirstName = "Thomas" },
                new Parent { FirstName = "Mary Kay" }
    },
                    Children = new Child[]
    {
                new Child
                {
                        FirstName = "Henriette Thaulow",
                        Gender = "female",
                        Grade = 5,
                        Pets = new Pet[]
                        {
                                new Pet { GivenName = "Fluffy" }
                        }
                }
    },
                    Address = new Address { State = "WA", County = "King", City = "Seattle" },
                    IsRegistered = true
                };
                docs.Add(item);
            }

            var client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
            var total = 30;
            var uri = UriFactory.CreateStoredProcedureUri(DatabaseId, ToDoItemsId, "bulkImport");
            var options = new RequestOptions { PartitionKey = new PartitionKey(string.Empty) };
            var totalInserted = 0;
            while (totalInserted < total)
            {
                var result = await client.ExecuteStoredProcedureAsync<int>(uri, docs);
                var inserted = result.Response;
                totalInserted += inserted;
                var remaining = total - totalInserted;
                docs = docs.GetRange(inserted, docs.Count - inserted);
            }

        }


        public static async Task BulkUpdate()
        {
            List<Family> docs = new List<Family>();
            for (int i = 109; i < 110; i++)
            {
                Family item = new Family
                {
                    Id = "Andersen." + i,
                    LastName = "Andersen" + i,
                    Parents = new Parent[]
    {
                new Parent { FirstName = "Thomas" },
                new Parent { FirstName = "Mary Kay" }
    },
                    Children = new Child[]
    {
                new Child
                {
                        FirstName = "Henriette Thaulow",
                        Gender = "female",
                        Grade = 5,
                        Pets = new Pet[]
                        {
                                new Pet { GivenName = "Fluffy" }
                        }
                }
    },
                    Address = new Address { State = "WA", County = "King", City = "Seattle" },
                    IsRegistered = true
                };
                docs.Add(item);
            }
            var client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
            var response = client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri("ToDoList", "ToDoItems"), "select * from c").ToList();
            var docs2 = response;
            var sampledoc = docs2[79];
            docs[0]._self = sampledoc._self;
            var total = docs.Count;
            var uri = UriFactory.CreateStoredProcedureUri(DatabaseId, ToDoItemsId, "bulkUpdate");
            var options = new RequestOptions { PartitionKey = new PartitionKey(string.Empty) };
            var totalInserted = 0;
            try
            {
                while (totalInserted < total)
                {
                    var result = await client.ExecuteStoredProcedureAsync<int>(uri, docs);
                    var inserted = result.Response;
                    totalInserted += inserted;
                    var remaining = total - totalInserted;
                    docs = docs.GetRange(inserted, docs.Count - inserted);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e);
            }

        }


        public async static Task CreateStoredProcedure(string sprocId)
        {
            var client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
            var sprocBody = File.ReadAllText($@"{sprocId}.js");
            var sprocDefinition = new StoredProcedure
            {
                Id = sprocId,
                Body = sprocBody
            };

            var result = await client.CreateStoredProcedureAsync(myStoreCollectionUri, sprocDefinition);
            var sproc = result.Resource;

        }

        public static void viewStoredProcedure()
        {
            var client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
            var results = client.CreateStoredProcedureQuery(myStoreCollectionUri).ToList();
        }

        public static async Task DeleteStoredProcedureAsync()
        {
            var client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
            var uri = UriFactory.CreateStoredProcedureUri(DatabaseId, ToDoItemsId, "bulkUpdate");
            var results = await client.DeleteStoredProcedureAsync(uri);
        }
    }


}
