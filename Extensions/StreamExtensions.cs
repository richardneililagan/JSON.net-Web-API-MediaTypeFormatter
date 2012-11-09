
namespace Rnai.WebApi.Tools.Extensions
{    
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    internal static class StreamExtensions
    {
        internal static Task<object> ReadAsJson(this Stream stream, Type instanceType, JsonSerializer serializer) 
        {
            // failsafe
            if (stream == null) { return null; }

            return Task<object>.Factory.StartNew(
                () => {
                    using (var reader = new JsonTextReader(new StreamReader(stream)))
                    {
                        var obj = serializer.Deserialize(reader);

                        //  We want to try deserialization without specifying an explicit type first,
                        //  then see if the resulting type is compatible with the type that is expected
                        //  from the Web API stack stream.
                        //  If not, then we try to read it again using an explicit type
                        //  (although it probably won't work anyway still :p)

                        return obj.GetType().IsSubclassOf(instanceType) ? obj : serializer.Deserialize(reader, instanceType);
                    }
                }
            );
        }

        internal static Task WriteAsJson(this Stream stream, object instance, JsonSerializer serializer)
        {
            // failsafe
            if (instance == null) { return null; }

            return Task.Factory.StartNew(
                () => {
                    using (var writer = new JsonTextWriter(new StreamWriter(stream)))
                    {                                   
                        serializer.Serialize(writer, instance);
                        writer.Flush();
                    }
                }
            );            
        }
    }
}
