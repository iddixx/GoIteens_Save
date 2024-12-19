using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] Catalogue;
    public Dictionary<string, GameObject> ObjectsMap;
    public Vector3 SpawnDistance;
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
        copy.transform.localRotation = Random.rotation;
    }

    private Vector3 GetRandomPoint()
    {
        float x = Random.Range(-SpawnDistance.x, SpawnDistance.x);
        float y = Random.Range(-SpawnDistance.y, SpawnDistance.y);
        float z = Random.Range(-SpawnDistance.z, SpawnDistance.z);

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
            copy.transform.localRotation = savedObject.Rotation;
        }
    } 
}
