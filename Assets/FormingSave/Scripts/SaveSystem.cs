using System.IO;
using UnityEngine;

namespace FormingSave
{
	public class SaveSystem : MonoBehaviour
	{
		public CookiesMiner CookiesMiner;

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
			CookiesMiner.Cookies = save.Cookies;
			CookiesMiner.UpdateText();
		}

		public void Save()
		{
			Save save = new Save()
			{
				Cookies = CookiesMiner.Cookies
			};

			string json = JsonUtility.ToJson(save);
			File.WriteAllText(_pathToFile, json);

			Debug.Log($"Path to save file: {_pathToFile}");
		}
	}
}