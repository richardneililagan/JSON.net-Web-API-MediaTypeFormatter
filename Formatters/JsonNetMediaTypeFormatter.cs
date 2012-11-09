
namespace Rnai.WebApi.Tools.Formatters
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    
    using Newtonsoft.Json;
    using Rnai.WebApi.Tools.Extensions;

    /// <summary>
    /// Handles (de)serialization of JSON through the ASP.NET Web API for specific media types.
    /// </summary>
    public sealed class JsonNetMediaTypeFormatter : JsonMediaTypeFormatter
    {
        public JsonSerializer Serializer { get; private set; }
        
        /// <summary>
        /// Specify the media types that this MediaTypeFormatter handles
        /// </summary>
        public JsonNetMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json") { CharSet = "utf-8" });
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/json") { CharSet = "utf-8" });

            Serializer = new JsonSerializer
            {
                TypeNameHandling = TypeNameHandling.Objects
            };
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            return readStream.ReadAsJson(type, Serializer);                
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, System.Net.TransportContext transportContext)
        {
            return writeStream.WriteAsJson(value, Serializer);
        }
    }
}
