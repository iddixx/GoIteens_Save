using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AccesingSaveDirectly
{
	public class CookiesMiner : MonoBehaviour
	{
		public SaveSystem SaveSystem;

		public int Cookies // Значення тепер зберігається та береться з сейву напряму
		{
			get => SaveSystem.GameState.Cookies;
			set => SaveSystem.GameState.Cookies = value;
		}
		public TextMeshProUGUI Text;

		private void Awake()
		{
			SaveSystem.SaveChanged += UpdateText; // Підписуємось на оновлення стану
			UpdateText();
		}

		public void Mine()
		{
			Cookies++;
			UpdateText();
		}

		public void UpdateText()
		{
			Text.text = $"Cookies: {Cookies}";
		}
	}
}