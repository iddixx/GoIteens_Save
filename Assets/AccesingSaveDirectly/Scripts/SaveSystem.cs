using System;
using System.IO;
using UnityEngine;

namespace AccesingSaveDirectly
{
	public class SaveSystem : MonoBehaviour
	{
		public event Action SaveChanged;
		public Save GameState = new Save();

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

			GameState = save;
			SaveChanged?.Invoke();
		}

		public void Save()
		{
			string json = JsonUtility.ToJson(GameState);
			File.WriteAllText(_pathToFile, json);

			Debug.Log($"Path to save file: {_pathToFile}");
		}
	}
}