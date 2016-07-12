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
	public GameObject Screens;
	#endregion

	#region PRIVATE VARS
	private bool QuitOpen;

	bool yetPaused = false;
	#endregion

	// Update is called once per frame
	void Update () {		
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (!QuitScreen.activeSelf)
			{
				if (Time.timeScale == 0)
					yetPaused = true;
				else
					Time.timeScale = 0;
				QuitScreen.SetActive(true);
			}
			else
			{
				Debug.Log("Hey");
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

		yetPaused = false;
	}
	/// <summary>
	/// 
	/// </summary>
	public void ButtonCancel()
	{
		if (!yetPaused)
			Time.timeScale = 1;
		QuitScreen.SetActive(false);

		yetPaused = false;
	}
	#endregion

	#region PRIVATE METHODS
	/// <summary>
	/// 
	/// </summary>
	private void ManageBackScene()
	{
		Debug.Log(SceneManager.GetActiveScene().buildIndex);
		if (SceneManager.GetActiveScene().buildIndex > 0)
		{
			if (!yetPaused)
				Time.timeScale = 1;
			yetPaused = false;

			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
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
