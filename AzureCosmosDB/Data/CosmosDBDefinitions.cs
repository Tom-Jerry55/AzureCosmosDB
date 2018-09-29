﻿using AzureCosmosDB.Models.CosmosDB;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureCosmosDB.Data
{
    public static class CosmosDBDefinitions
    {
        private static string accountURI = "https://kciprojecttrackerdevdb.documents.azure.com:443/";
        private static string accountKey = "6bjCKmU1hwQFn9sGJG7w1f9OLXsthoFCvs7hTWT5lYfLAw9UJMiHIqhz0qkUIt4OnyphokoJk0SWokmxi0m1cg==";
        public static string DatabaseId { get; private set; } = "ToDoList";
        public static string ToDoItemsId { get; private set; } = "ToDoItems";


        //public static IDocumentDBConnection GetConnection()
        //{
        //    return new DocumentClient(accountURI, accountKey, DatabaseId);
        //}

        //public static async Task Initialize()
        //{
        //    var connection = GetConnection();

        //    await connection.Client
        //        .CreateDatabaseIfNotExistsAsync(
        //            new Database { Id = DatabaseId });

        //    DocumentCollection myCollection = new DocumentCollection();
        //    myCollection.Id = ToDoItemsId;
        //    myCollection.IndexingPolicy =
        //       new IndexingPolicy(new RangeIndex(DataType.String)
        //       { Precision = -1 });
        //    myCollection.IndexingPolicy.IndexingMode = IndexingMode.Consistent;
        //    var res = await connection.Client.CreateDocumentCollectionIfNotExistsAsync(
        //        UriFactory.CreateDatabaseUri(DatabaseId),
        //        myCollection);
        //    if (res.StatusCode == System.Net.HttpStatusCode.Created)
        //        await InitData(connection);
        //}
        //private static async Task InitData(IDocumentDBConnection connection)
        //{
        //    List<ToDoItem> allItems = new List<ToDoItem>();
        //    for (int i = 0; i < 6; i++)
        //    {
        //        var curr = new ToDoItem();
        //        allItems.Add(curr);
        //        curr.Name = "Name" + i;
        //        curr.Description = "Description" + i;
        //        curr.Completed = i % 2 == 0;
        //        curr.Id = Guid.NewGuid().ToString();
        //        curr.Owner = i > 3 ? "frank@fake.com" : "John@fake.com";
        //        if (i > 1)
        //            curr.AssignedTo = new Person
        //            {
        //                Name = "Francesco",
        //                Surname = "Abbruzzese",
        //                Id = Guid.NewGuid().ToString()
        //            };
        //        else
        //            curr.AssignedTo = new Person
        //            {
        //                Name = "John",
        //                Surname = "Black",
        //                Id = Guid.NewGuid().ToString()
        //            };
        //        var innerlList = new List<ToDoItem>();
        //        for (var j = 0; j < 4; j++)
        //        {
        //            innerlList.Add(new ToDoItem
        //            {
        //                Name = "ChildrenName" + i + "_" + j,
        //                Description = "ChildrenDescription" + i + "_" + j,
        //                Id = Guid.NewGuid().ToString()
        //            });
        //        }
        //        curr.SubItems = innerlList;
        //        var team = new List<Person>();
        //        for (var j = 0; j < 4; j++)
        //        {
        //            team.Add(new Person
        //            {
        //                Name = "TeamMemberName" + i + "_" + j,
        //                Surname = "TeamMemberSurname" + i + "_" + j,
        //                Id = Guid.NewGuid().ToString()
        //            });
        //        }
        //        curr.Team = team;
        //    }
        //    //foreach (var item in allItems)
        //    //{
        //    //    await connection.Client .CreateDocumentAsync( UriFactory.CreateDocumentCollectionUri( DatabaseId, ToDoItemsId),item);

        //    //}

        //  //  connection.Client.ConnectionPolicy.RetryOptions.MaxRetryWaitTimeInSeconds = 30;
        ////    connection.Client.ConnectionPolicy.RetryOptions.MaxRetryAttemptsOnThrottledRequests = 9;

        //  //  IBulkExecutor bulkExecutor = new BulkExecutor(client, dataCollection);
        //  //  await bulkExecutor.InitializeAsync();


        // //   connection.Client.ConnectionPolicy.RetryOptions.MaxRetryWaitTimeInSeconds = 0;
        //  //  connection.Client.ConnectionPolicy.RetryOptions.MaxRetryAttemptsOnThrottledRequests = 0;
        //}

        //public static void getData()
        //{
        //    var connection = GetConnection();
        //    using (var client = new DocumentClient(new Uri(accountURI), accountKey))
        //    {
        //        var response = client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri("ToDoList", "ToDoItems"),"select * from c").ToList();
        //        var document = response.First();
        //    }

        //}

        //public static async Task deleteDataAsync()
        //{
        //    var connection = GetConnection();
        //    using (var client = new DocumentClient(new Uri(accountURI), accountKey))
        //    {
        //        var response= await  client.DeleteDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, ToDoItemsId));
        //    }
        //}


    }
}
