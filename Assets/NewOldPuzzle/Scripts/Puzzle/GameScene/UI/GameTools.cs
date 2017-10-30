using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CountingSheeps.NewOldPuzzle
{
	public class GameTools : MonoBehaviour
	{

		public GameObject btResume;
		public GameObject btPause;

		public Button btInfo;
		public GameObject labelInfo;

		public GameObject InfoGameScreen;
		public Image InfoImageSelect;

		public GameObject RestartScreen;

		private bool gameAlreadyPaused;

		void Start()
		{
			btResume.SetActive(false);
		}

		public void StartGame()
		{
			BoardManager.instance.StartGame();
		}

		public void PauseGame()
		{
			if (Time.timeScale == 1)
			{
				Time.timeScale = 0;

				btResume.SetActive(true);
				btPause.SetActive(false);

				BoardManager.instance.PauseGame(true);

				btInfo.interactable = false;
				labelInfo.SetActive(false);
			}
			else
			{
				Time.timeScale = 1;

				btResume.SetActive(false);
				btPause.SetActive(true);

				BoardManager.instance.PauseGame(false);

				btInfo.interactable = true;
				labelInfo.SetActive(true);
			}
		}

		public void OpenInfoGame()
		{
			InfoGameScreen.SetActive(true);
			//coloca a imagem no role
			InfoImageSelect.sprite = Sprite.Create(GameManager.instance.ImageSelect, GameManager.instance.ImageCropRect, new Vector2(0, 0));
		}

		public void CloseInfoGame()
		{
			InfoGameScreen.SetActive(false);
		}


		public void OpenRestartScreen()
		{
			if (Time.timeScale == 0)
			{
				gameAlreadyPaused = true;
			}
			else
			{
				Time.timeScale = 0;
			}

			RestartScreen.SetActive(true);
		}


		public void btCancelRestartScreen()
		{
			if (!gameAlreadyPaused)
			{
				Time.timeScale = 1;
			}

			RestartScreen.SetActive(false);
		}


		public void OkRestartScreen()
		{
			Time.timeScale = 1;

			btPause.SetActive(true);
			btResume.SetActive(false);

			RestartScreen.SetActive(false);

			btInfo.interactable = true;

			BoardManager.instance.PauseGame(false);
		}
	}
}