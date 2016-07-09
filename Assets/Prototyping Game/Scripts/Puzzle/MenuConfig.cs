using UnityEngine;
using System.Collections;

public class MenuConfig : MonoBehaviour {

	public GameObject OptionsMenu;

	private AudioManager mAudioManager;

	// Use this for initialization
	void Start () {
		mAudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
	}
	
	public void OnButtonConfig()
	{
		OptionsMenu.SetActive(!OptionsMenu.activeSelf);
	}

	public void OnButtonMusic()
	{
		mAudioManager.SetMuteAudio(mAudioManager.BGMEnable, AudioType.BGM);
	}

	public void OnButtonEffect()
	{
		mAudioManager.SetMuteAudio(mAudioManager.SFXEnable, AudioType.SFX);
	}

	public void OnButtonPushNotification()
	{
		Debug.Log("Ainda não tem :(");
	}
}
