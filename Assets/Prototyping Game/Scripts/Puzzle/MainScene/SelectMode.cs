using UnityEngine;
using System;
using System.Collections;
using PrototypingGame;

public class SelectMode : MonoBehaviour
{
	public GameManager gameManager;

	public GameObject[] OptionsMode;
	
	// Use this for initialization
	void Start()
	{
		//Seta o classico com Classic
		gameManager.mGameMode = GameMode.Classic;
		//Seta o classico com Classic
		gameManager.mImageMode = ImageMode.Default;
	}
	/// <summary>
	/// 0: Volta um modo
	/// 1: Avanca um modo
	/// </summary>
	public void btNavMode(int selectionMode)
	{
		int GameModeEnumLength = Enum.GetNames(typeof(GameMode)).Length;
		int indexMode = (int)gameManager.mGameMode;
		
		//navega entre os modulos
		if (selectionMode == -1 && indexMode == 0)
		{
			//se estiver na posicao 0, vai pra ultima
			gameManager.mGameMode = (GameMode)(GameModeEnumLength - 1);
			selectionMode = GameModeEnumLength - 1;
		}
		else if (selectionMode == 1 && indexMode == GameModeEnumLength - 1)
		{
			//se estiver na posicao 0, vai pra ultima
			gameManager.mGameMode = (GameMode) 0;
			selectionMode = (GameModeEnumLength - 1) * -1;
		}		
		//atualiza o modo de jogo no game manager
		gameManager.mGameMode = (GameMode)(indexMode + selectionMode);

		//Debug.Log("indexMode: " + indexMode + " | mGameMode: " + (int)gameManager.mGameMode);

		//Desativa a selecao anterior
		OptionsMode[indexMode].SetActive(false);
		//Ativa a selecao atual
		OptionsMode[(int)gameManager.mGameMode].SetActive(true);
	}

	public void SetStateImageMode(int ImageMode)
	{
		//Seta o classico com Classic
		gameManager.mImageMode = (ImageMode) ImageMode;

		Debug.Log(gameManager.mImageMode);
	}
}
