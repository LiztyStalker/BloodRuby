using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//A0.8
public class TipFactoryClass : SingletonClass<TipFactoryClass>
{


	List<TipClass> m_tipList = new List<TipClass>();

	public TipFactoryClass(){
		initParse ();
	}


	void initParse(){

		Sprite[] images = Resources.LoadAll<Sprite> (PrepClass.getLanguagePath(PrepClass.tipImagePath));

		TextAsset tAsset = Resources.Load<TextAsset> (PrepClass.getLanguagePath(PrepClass.tipDataPath));

		if (tAsset != null && images.Length > 0) {
			string[] splitList = tAsset.text.Split ('\n');

			foreach (string splitStr in splitList) {
				string[] data = splitStr.Split ('\t');

				Sprite image = getSprite (images, data [0]);
				if(image != null){
					TipClass tipData = new TipClass (image, data [1], data [2]);
					m_tipList.Add (tipData);
				}
			}

		} else {
			Debug.LogError (PrepClass.tipImagePath + " is not Found");
		}
	}

	/// <summary>
	/// 스프라이트 찾기
	/// </summary>
	/// <returns>The sprite.</returns>
	/// <param name="images">Images.</param>
	/// <param name="key">Key.</param>
	Sprite getSprite(Sprite[] images, string key){
		if(images.Length > 0){
			return images.Where (spr => spr.name == key).SingleOrDefault ();
		}
		return null;
	}

	/// <summary>
	/// 임의로 팁 데이터 가져오기
	/// </summary>
	/// <returns>The random tip.</returns>
	public TipClass getRandomTip(){
		if(m_tipList.Count > 0)
			return m_tipList[Random.Range(0, m_tipList.Count)];
		return null;
	}

}


