
namespace Rnai.WebApi.Tools.Configuration
{    
    using System.Linq;
    using System.Net.Http.Formatting;

    using Rnai.WebApi.Tools.Formatters;    

    public class MediaTypeFormatterConfig
    {
        public static void RegisterJsonNetMediaTypeFormatter(MediaTypeFormatterCollection formatters)
        {
            formatters.Remove(formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault());
            formatters.Add(new JsonNetMediaTypeFormatter());
        }
    }
}
