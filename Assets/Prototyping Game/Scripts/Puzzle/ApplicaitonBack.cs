using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ApplicaitonBack : MonoBehaviour {

	#region PUBLIC VARS
	public GameObject Loading;
	public GameObject QuitScreen;
	#endregion

	#region PRIVATE VARS
	private bool QuitOpen;
	#endregion

	// Update is called once per frame
	void Update () {		
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (!QuitScreen.activeSelf)
			{
				Time.timeScale = 0;
				QuitScreen.SetActive(true);
			}
			else
			{
				ManageBackScene();
			}
		}
	}

	#region PUBLIC METHODS
	/// <summary>
	/// 
	/// </summary>
	public void ButtonOk()
	{
		ManageBackScene();
	}
	/// <summary>
	/// 
	/// </summary>
	public void ButtonCancel()
	{
		Time.timeScale = 1;
		QuitScreen.SetActive(false);
	}
	#endregion

	#region PRIVATE METHODS
	/// <summary>
	/// 
	/// </summary>
	private void ManageBackScene()
	{
		if (SceneManager.GetActiveScene().buildIndex > 0)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
			Time.timeScale = 1;
		}
		else
		{
#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}
	}
	#endregion

}
