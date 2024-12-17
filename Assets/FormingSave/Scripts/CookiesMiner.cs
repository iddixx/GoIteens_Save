using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FormingSave
{

	public class CookiesMiner : MonoBehaviour
	{
		public int Cookies;
		public TextMeshProUGUI Text;

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