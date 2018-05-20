using System;
using UnityEngine;
using UnityEngine.UI;

public class UILobbyNewsClass : MonoBehaviour
{

	const int c_patchCount = 10;

	void Start(){


		if (transform.childCount == 0) {

			GameObject textObj = new GameObject ();
			Font font = Resources.Load<Font> (string.Format ("{0}/{1}", PrepClass.fontPath, PrepClass.fontName));


			textObj.AddComponent<Text> ().fontSize = 36;
			textObj.GetComponent<Text> ().font = font;
			textObj.GetComponent<Text> ().color = Color.black;




			textObj.AddComponent<LayoutElement> ().minWidth = GetComponent<RectTransform>().rect.width - 40f;
			textObj.GetComponent<LayoutElement> ().minHeight = 250f;
			//textObj.GetComponent<LayoutElement> ().preferredHeight = 1750f;

			TextAsset[] textAssets = Resources.LoadAll<TextAsset> (PrepClass.getLanguagePath(PrepClass.newsPath));

			if (textAssets != null) {


				for (int i = 1; i < c_patchCount + 1; i++) {

					if (textAssets.Length - i < 0) break;
					
					GameObject tmpText = (GameObject)Instantiate (textObj);
					tmpText.GetComponent<Text> ().text = textAssets [textAssets.Length - i].text;
					tmpText.transform.SetParent (transform);
					tmpText.transform.localScale = Vector3.one;
				

				}


			}
		}

	}
}


