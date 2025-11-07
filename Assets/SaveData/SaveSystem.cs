using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public static class SaveSystem
{
    private static string folderPath => Path.Combine(Application.persistentDataPath, "Saves");

    public static void Save<T>(T data, string fileName)
    {
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Path.Combine(folderPath, fileName + ".json"), json);
    }

    public static T Load<T>(string fileName)
    {
        string path = Path.Combine(folderPath, fileName + ".json");
        if (!File.Exists(path))
            return default;

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(json);
    }

    public static bool Exists(string fileName)
    {
        return File.Exists(Path.Combine(folderPath, fileName + ".json"));
    }
}
