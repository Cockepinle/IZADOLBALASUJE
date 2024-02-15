using Newtonsoft.Json;
using System.IO;

namespace EjednevnicDZ
{
    public class Serializer
    {
        public static void Serialize<T>(T data, string filePath)
        {
            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(filePath, json);
        }

        public static T Deserialize<T>(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
