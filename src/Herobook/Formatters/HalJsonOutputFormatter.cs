
using System.Buffers;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

public class HalJsonOutputFormatter : JsonOutputFormatter
{
    public HalJsonOutputFormatter(JsonSerializerSettings serializerSettings, ArrayPool<char> charPool) : base(serializerSettings, charPool)
    {
        serializerSettings.Formatting = Formatting.Indented;
        serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/hal+json"));
    }
}