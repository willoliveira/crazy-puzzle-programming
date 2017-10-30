using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CountingSheeps.NewOldPuzzle
{
	public class FinishScreen : MonoBehaviour
	{
		#region PUBLIC VARS
		public Text TimerText;
		public AudioClip FinishSound;
		#endregion

		private AudioManager mAudioManager;

		#region PUBLIC METHODS
		/// <summary>
		/// 
		/// </summary>
		/// <param name="StringTimer"></param>
		public void Show(string StringTimer)
		{
			mAudioManager = AudioManager.instance;
			//ativa a janela
			gameObject.SetActive(true);
			//Seta o tempo
			TimerText.text = StringTimer;
			//som de vitoria
			mAudioManager.Play(FinishSound, this.name, AudioType.SFX);
		}
		/// <summary>
		/// 
		/// </summary>
		public void ButtonOk()
		{
			mAudioManager.Stop(this.name, AudioType.SFX);
		}

		#endregion

	}
}