using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Globalization;
using SmartLocalization;

public class TranslateSmartLocalization : MonoBehaviour {

	LanguageManager mLanguageManager;
		
	void Start()
	{
		mLanguageManager = LanguageManager.Instance;
		TextTranslate();
	}
	
	public void TextTranslate()
	{
		string str = mLanguageManager.GetTextValue(this.name);
		if (!string.IsNullOrEmpty(str))
		{
			GetComponent<Text>().text = str;
		}
		else
		{
			Debug.LogWarning("A string " + this.name + " não possui uma traducao configurada");
		}
	}
}
