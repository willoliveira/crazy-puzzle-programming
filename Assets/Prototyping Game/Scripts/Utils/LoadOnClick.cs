﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadOnClick : MonoBehaviour
{
	public GameObject loadingImage;

	public void LoadScene(int level)
	{
		loadingImage.SetActive(loadingImage);
		//depreciado
		//Application.LoadLevel(level);
		SceneManager.LoadScene(level);
	}
}