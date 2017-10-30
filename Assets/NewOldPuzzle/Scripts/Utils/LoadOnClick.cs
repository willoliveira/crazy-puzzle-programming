using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace CountingSheeps.NewOldPuzzle
{
	public class LoadOnClick : MonoBehaviour
	{
		public GameObject loadingImage;

		public void LoadScene(int level)
		{
			loadingImage.SetActive(loadingImage);

			SceneManager.LoadScene(level);
		}
	}
}