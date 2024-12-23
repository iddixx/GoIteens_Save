using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public ObjectSpawner Spawner;

    private string _pathToFile => Path.Combine(Application.persistentDataPath, "saves.json");

    public void Load()
    {
        if (File.Exists(_pathToFile) == false)
        {
            Debug.Log("No save to load");
            return;
        }

        string json = File.ReadAllText(_pathToFile);
        Save save = JsonUtility.FromJson<Save>(json);

        RestoreState(save);
    }

    private void RestoreState(Save save)
    {
        Spawner.RestoreState(save.SpawnedObjects);
    }

    public void Save()
    {
        Save save = new Save()
        {
            SpawnedObjects = Spawner.SpawnedObjects.Select(entry =>
            {
                Transform transform = entry.Object.transform;

                return new Save.SpawnedObject()
                {
                    Key = entry.Key,
                    Position = transform.localPosition,
                    Scale = transform.localScale,
                    Rotation = transform.localRotation
                };
            }).ToArray()
        };

        string json = JsonUtility.ToJson(save);
        File.WriteAllText(_pathToFile, json);

        Debug.Log($"Path to save file: {_pathToFile}");
    }
}
