using UnityEngine;
using UnityEngine.UI;
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
		mAudioManager = AudioManager.instance;
	}
	
	public void OnPointerDown(PointerEventData data)
	{
		Button thisButton = gameObject.GetComponent<Button>();
		if (!IsOnClickAudio && (thisButton == null || (thisButton != null && thisButton.interactable)))
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