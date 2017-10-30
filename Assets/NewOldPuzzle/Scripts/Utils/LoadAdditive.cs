using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace CountingSheeps.NewOldPuzzle
{
	public class LoadAdditive : MonoBehaviour
	{

		public void LoadAddOnClick(int level)
		{
			SceneManager.LoadScene(level, LoadSceneMode.Additive);
		}
	}
}