using Newtonsoft.Json;

public static class JsonExtensions
{
    public static string NormalizeJson(this string input)
    {
        var jsonObject = JsonConvert.DeserializeObject(input);
        return JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
    }
}