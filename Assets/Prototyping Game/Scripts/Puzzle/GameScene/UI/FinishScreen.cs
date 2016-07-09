using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinishScreen : MonoBehaviour
{
	#region PUBLIC VARS
	public Text TimerText;
	public AudioClip FinishSound;
	#endregion

	private AudioManager mAudioManager;

	// Use this for initialization 
	void Start()
	{
		mAudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
	}

	#region PUBLIC METHODS
	/// <summary>
	/// 
	/// </summary>
	/// <param name="StringTimer"></param>
	public void Show(string StringTimer)
	{
		//ativa a janela
		gameObject.SetActive(true);
		//Seta o tempo
		TimerText.text = StringTimer;
		//
		mAudioManager.Play(FinishSound, this.name, AudioType.SFX);
	}
	#endregion

	#region PRIVATE METHODS

	/// <summary>
	/// 
	/// </summary>
	private void VisibleBoard()
	{
		transform.gameObject.SetActive(true);
	}
	#endregion
}