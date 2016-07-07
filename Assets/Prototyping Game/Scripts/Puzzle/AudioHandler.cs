using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


public class AudioHandler : MonoBehaviour, IPointerDownHandler
{

	#region PUBLIC VARS
	public AudioClip OnClickAudio;
	#endregion

	#region PRIVATE VARS

	private bool IsOnClickAudio = false;
	private AudioManager mAudioManager;

	#endregion

	// Use this for initialization
	void Start () {
		mAudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
	}
	
	public void OnPointerDown(PointerEventData data)
	{
		if (!IsOnClickAudio)
		{
			IsOnClickAudio = true;
			//limpa a variavel depois que tocar o som
			Invoke("ClearAudioBool", OnClickAudio.length);
			//
			mAudioManager.Play(OnClickAudio, this.name, AudioType.SFX);
		}
	}

	private void ClearAudioBool()
	{
		IsOnClickAudio = false;
	}


}
