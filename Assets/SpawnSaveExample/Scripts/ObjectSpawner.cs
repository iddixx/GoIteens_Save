using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] Catalogue;
    public Dictionary<string, GameObject> ObjectsMap;
    public Vector3 SpawnDistance;
    public bool KeepScaleInProportions = false; 
    public Vector3 ScaleSize;
    public Vector3 SpawnCenter;

    public List<Entry> SpawnedObjects = new List<Entry>();

    public struct Entry
    {
        public string Key;
        public GameObject Object;

        public Entry(string key, GameObject o)
        {
            Key = key;
            Object = o;
        }
    }

    private void Start()
    {
        ObjectsMap = Catalogue.ToDictionary(p => p.name, p => p);
    }

    public void Spawn(string key)
    {
        GameObject prefab = ObjectsMap[key];

        GameObject copy = Instantiate(prefab);
        SpawnedObjects.Add(new Entry(key, copy));

        copy.transform.localPosition = GetRandomPoint() + SpawnCenter;
        copy.transform.localScale = GetRandomScale(KeepScaleInProportions);
        copy.transform.localRotation = Random.rotation;
    }

    private Vector3 GetRandomPoint()
    {
        float x = Random.Range(-SpawnDistance.x, SpawnDistance.x);
        float y = Random.Range(-SpawnDistance.y, SpawnDistance.y);
        float z = Random.Range(-SpawnDistance.z, SpawnDistance.z);

        return new Vector3(x, y, z);
    }

    private Vector3 GetRandomScale(bool keepProportions)
    {
        float x = 0f, y = 0f, z = 0f;

        if(keepProportions)
        {
            x = y = z = Random.Range(1f, ScaleSize.x);
        }
        else
        {
            x = Random.Range(1f, ScaleSize.x);
            y = Random.Range(1f, ScaleSize.y);
            z = Random.Range(1f, ScaleSize.z);
        }

        return new Vector3(x, y, z);
    }

    public void RestoreState(Save.SpawnedObject[] savedObjects)
    {
        foreach (Entry entry in SpawnedObjects)
        {
            Destroy(entry.Object);
        }

        foreach (Save.SpawnedObject savedObject in savedObjects)
        {
            GameObject prefab = ObjectsMap[savedObject.Key];

            GameObject copy = Instantiate(prefab);
            SpawnedObjects.Add(new Entry(savedObject.Key, copy));

            copy.transform.localPosition = savedObject.Position;
            copy.transform.localScale = savedObject.Scale;
            copy.transform.localRotation = savedObject.Rotation;
        }
    } 
}
