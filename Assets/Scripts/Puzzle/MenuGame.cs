using UnityEngine;
using System.Collections;

public class MenuGame : MonoBehaviour {
	//
	public GameObject MainMenu;
	public GameObject ModeSelect;
	public GameObject DifficultySelect;
	//
	public GameObject gameManagerObject;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		//
		gameManager = gameManagerObject.GetComponent<GameManager>();
		//
		MainMenu.SetActive(true);
		ModeSelect.SetActive(false);
		DifficultySelect.SetActive(false);
	}

	public void btJogar()
	{
		MainMenu.SetActive(false);
		ModeSelect.SetActive(true);
	}

	public void btSelectMode(int mode)
	{
		//atribui o modo de jogo selecionado ao GameManager
		gameManager.mSelectMode = (SelectMode)mode;

		ModeSelect.SetActive(false);
		DifficultySelect.SetActive(true);
	}

	public void btSelectDifficulty(int mode)
	{
		//atribui o modo de jogo selecionado ao GameManager
		gameManager.mDiffilcultyMode = (DiffilcultyMode)mode;

		DifficultySelect.SetActive(false);
	}
}