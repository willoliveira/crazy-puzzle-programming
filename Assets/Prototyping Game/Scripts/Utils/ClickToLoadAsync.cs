using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ClickToLoadAsync : MonoBehaviour
{

	public Slider loadingBar;
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
			loadingBar.value = async.progress;
			yield return null;
		}
	}
}