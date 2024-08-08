using DynamicMapping.Model;
using DynamicMapping.Utilities;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

//Custom error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

//XML Test Data
var xml = @"<?xml version='1.0'?>
<Reservation>
  <Id>123</Id>
  <Name>John</Name>
  <BookingType>Room</BookingType>
  <Amount>3</Amount>
</Reservation>";

//JSON Test Data
string json = @"
{
    ""Reservation"": {
        ""Id"": 123,
        ""Name"": ""John"",
        ""BookingType"": ""Room"",
        ""Amount"": 32
    }
}";

//Conversion done within the program for test purposes
var resultjson = MapHandler.XmlToJson<Reservation>(xml);
var resultxml = MapHandler.JsonToXml<ReservationCollectionModel>(json);

//APIs
var mappingApi = app.MapGroup("/reservation");
mappingApi.MapPost("/convert", async (HttpRequest request) =>
{
    var sourceType = request.Query["sourceType"].ToString();
    var targetType = request.Query["targetType"].ToString();

    string requestBody;
    using (var reader = new StreamReader(request.Body))
    {
        requestBody = await reader.ReadToEndAsync();
    }

    //Model can be bind here and changed depending on the need
    var result = MapHandler.Map<ReservationCollectionModel>(sourceType, targetType, requestBody);
    return Results.Ok(result);
});
mappingApi.MapGet("/xml-to-json", () => resultjson);
mappingApi.MapGet("/json-to-xml", () => resultxml);

app.Run();


[JsonSerializable(typeof(ReservationCollectionModel[]))]
[JsonSerializable(typeof(Reservation[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
