using UnityEngine;
using Newtonsoft.Json;
public static class JsonParser 
{
    public static T ParseFile<T>(string path) where T : class
    {
        TextAsset targetFile = Resources.Load<TextAsset>(path);
        var gameData = JsonConvert.DeserializeObject<T>(targetFile.text);
        return gameData;
    }
}
