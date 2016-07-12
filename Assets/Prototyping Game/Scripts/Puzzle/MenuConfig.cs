using UnityEngine;
using System.Collections;

public class MenuConfig : MonoBehaviour {

	public GameObject OptionsMenu;

	public GameObject btMusic;
	public GameObject btMusicNo;

	public GameObject btEffect;
	public GameObject btEffectNo;

	private AudioManager mAudioManager;

	// Use this for initialization
	void Start () {
		mAudioManager = AudioManager.instance;

		ConfigMenu();
	}
	
	public void OnButtonConfig()
	{
		OptionsMenu.SetActive(!OptionsMenu.activeSelf);
	}

	public void OnButtonMusic()
	{
		mAudioManager.SetMuteAudio(mAudioManager.BGMEnable, AudioType.BGM);

		if (mAudioManager.BGMEnable)
		{
			btMusic.SetActive(true);
			btMusicNo.SetActive(false);
		}
		else
		{
			btMusic.SetActive(false);
			btMusicNo.SetActive(true);
		}
	}

	public void OnButtonEffect()
	{
		mAudioManager.SetMuteAudio(mAudioManager.SFXEnable, AudioType.SFX);

		if (mAudioManager.SFXEnable)
		{
			btEffect.SetActive(true);
			btEffectNo.SetActive(false);
		}
		else
		{
			btEffect.SetActive(false);
			btEffectNo.SetActive(true);
		}
	}

	private void ConfigMenu()
	{
		if (mAudioManager.SFXEnable)
		{
			btEffect.SetActive(true);
			btEffectNo.SetActive(false);
		}
		else
		{
			btEffect.SetActive(false);
			btEffectNo.SetActive(true);
		}

		if (mAudioManager.BGMEnable)
		{
			btMusic.SetActive(true);
			btMusicNo.SetActive(false);
		}
		else
		{
			btMusic.SetActive(false);
			btMusicNo.SetActive(true);
		}
	}

	public void OnButtonPushNotification()
	{
		Debug.Log("Ainda não tem :(");
	}
}
