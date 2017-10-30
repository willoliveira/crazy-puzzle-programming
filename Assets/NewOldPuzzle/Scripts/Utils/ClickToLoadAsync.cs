using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace CountingSheeps.NewOldPuzzle
{
	public class ClickToLoadAsync : MonoBehaviour
	{

		public RectTransform loadingBar;
		public GameObject loadingImage;

		private AsyncOperation async;

		public void ClickAsync(int level)
		{
			loadingImage.SetActive(true);
			StartCoroutine(LoadLevelWithBar(level));
		}

		IEnumerator LoadLevelWithBar(int level)
		{
			//async = Application.LoadLevelAsync(level);
			async = SceneManager.LoadSceneAsync(level);
			while (!async.isDone)
			{
				loadingBar.localScale = new Vector3(async.progress, 1, 1);
				yield return null;
			}
		}
	}
}