﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Globalization;
using SmartLocalization;

public class TranslateManager : MonoBehaviour {

	LanguageManager mLanguageManager;

	private string returnLanguageCode()
	{
		string retorno = "";
		switch (Application.systemLanguage + "")
		{
			case "English":
				retorno = "en";
				break;
			case "French":
				retorno = "fr";
				break;
			case "German":
				retorno = "de";
				break;
			case "Korean":
				retorno = "Ko";
				break;
			case "Portuguese":
				retorno = "pt";
				break;
			case "Spanish":
				retorno = "es";
				break;
			case "Italian":
				retorno = "it";
				break;
		}

		return retorno;
	}

	void Awake()
	{
		mLanguageManager = LanguageManager.Instance;

		SetLanguageGame();
	}

	private void SetLanguageGame()
	{
		string strLanguageCode;
		string systemLanguage = Application.systemLanguage + "";

		if (LanguageManager.Instance.IsLanguageSupportedEnglishName(systemLanguage))
		{
			strLanguageCode = returnLanguageCode();
		}
		else
		{
			strLanguageCode = "en";
		}
		LanguageManager.Instance.ChangeLanguage(strLanguageCode);
	}
}
