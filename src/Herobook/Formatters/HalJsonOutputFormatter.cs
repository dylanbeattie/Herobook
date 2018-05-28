
using System.Buffers;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;

public class HalJsonOutputFormatter : JsonOutputFormatter
{
    public HalJsonOutputFormatter(JsonSerializerSettings serializerSettings, ArrayPool<char> charPool) : base(serializerSettings, charPool)
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/hal+json"));
    }
}