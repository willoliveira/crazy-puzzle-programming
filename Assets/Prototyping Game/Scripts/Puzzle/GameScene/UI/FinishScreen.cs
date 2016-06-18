using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace PrototypingGame
{
	public class FinishScreen : MonoBehaviour
	{

		public Text TimerText;

		// Use this for initialization
		void Start()
		{

		}

		public void Show(string StringTimer)
		{
			//ativa a janela
			gameObject.SetActive(true);
			//Seta o tempo
			TimerText.text = StringTimer;
		}

		private void VisibleBoard()
		{
			transform.gameObject.SetActive(true);
		}

	}
}