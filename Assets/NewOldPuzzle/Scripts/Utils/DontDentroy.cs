﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace CountingSheeps.NewOldPuzzle
{
	public class DontDentroy : MonoBehaviour
	{
		private static GameObject instanceGO;

		public string nameGameObject;

		// Use this for initialization
		void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}

	}
}