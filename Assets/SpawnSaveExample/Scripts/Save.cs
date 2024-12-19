using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Save
{
    [Serializable]
    public struct SpawnedObject
    {
        public string Key;
        public Vector3 Position;
        public Quaternion Rotation;
    }

    public SpawnedObject[] SpawnedObjects;
}
