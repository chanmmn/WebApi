// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


using System;
using Azure; 
using Azure.Maps.Search; 
using Azure.Maps.Search.Models;

// Use Azure Maps subscription key authentication 
string SUBSCRIPTION_KEY = "Azure Map Primary Key";
//var subscriptionKey = Environment.GetEnvironmentVariable("SUBSCRIPTION_KEY") ?? string.Empty;
//var subscriptionKey = Environment.GetEnvironmentVariable(SUBSCRIPTION_KEY) ?? string.Empty;
//var subscriptionKey = Environment.GetEnvironmentVariable(SUBSCRIPTION_KEY);
//var credential = new AzureKeyCredential(subscriptionKey);
var credential = new AzureKeyCredential(SUBSCRIPTION_KEY);
var client = new MapsSearchClient(credential); 

//Response<GeocodingResponse> searchResult = client.GetGeocoding(
 //   "1 Microsoft Way, Redmond, WA 98052");
//Response<GeocodingResponse> searchResult = client.GetGeocoding(
//    "NO L 103 - L 106, PUBLIC ARRIVAL CONCOURSE, JALAN LAPANGAN TERBANG KUCHING 93728");
Response<GeocodingResponse> searchResult = client.GetGeocoding(
    "STALL NO. 5, CONCOURSE FLOOR, KUALA LUMPUR CONVENTION CENTRE KUALA LUMPUR 50088");

for (int i = 0; i < searchResult.Value.Features.Count; i++)
{
    Console.WriteLine("Coordinate:" + string.Join(",", searchResult.Value.Features[i].Geometry.Coordinates));
}