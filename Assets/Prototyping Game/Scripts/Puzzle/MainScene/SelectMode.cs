using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class SelectMode : MonoBehaviour
{

	public Text test;

	#region PRIVATE VARS
	public GameObject[] OptionsMode;
	private GameManager mGameManager;
	#endregion

	
	void OnEnable()
	{
		EventManager.StartListening("Prev Mode", prevModeTrigger);
		EventManager.StartListening("Next Mode", nextModeTrigger);
	}

	void OnDisable()
	{
		EventManager.StopListening("Prev Mode", prevModeTrigger);
		EventManager.StopListening("Next Mode", nextModeTrigger);
	}

	// Use this for initialization
	void Start()
	{
		mGameManager = GameManager.instance;


		//Seta o classico com Classic
		mGameManager.mGameMode = GameMode.Classic;
		//Seta o classico com Classic
		mGameManager.mImageMode = ImageMode.Default;
	}

	void Update()
	{
		if (CrossPlatformInputManager.GetAxis("Swipe X") > 0)
			test.text = "X: " + CrossPlatformInputManager.GetAxis("Swipe X") + " Y: " + CrossPlatformInputManager.GetAxis("Swipe Y");
	}

	private void prevModeTrigger()
	{
		Debug.Log("prevModeTrigger");
		btNavMode(-1);
	}

	private void nextModeTrigger()
	{
		Debug.Log("nextModeTrigger");
		btNavMode(1);
	}

	#region PUBLIC METHODS
	/// <summary>
	/// 0: Volta um modo
	/// 1: Avanca um modo
	/// </summary>
	public void btNavMode(int selectionMode)
	{
		int GameModeEnumLength = Enum.GetNames(typeof(GameMode)).Length;
		int indexMode = (int)mGameManager.mGameMode;
		
		//navega entre os modulos
		if (selectionMode == -1 && indexMode == 0)
		{
			//se estiver na posicao 0, vai pra ultima
			mGameManager.mGameMode = (GameMode)(GameModeEnumLength - 1);
			selectionMode = GameModeEnumLength - 1;
		}
		else if (selectionMode == 1 && indexMode == GameModeEnumLength - 1)
		{
			//se estiver na posicao 0, vai pra ultima
			mGameManager.mGameMode = (GameMode) 0;
			selectionMode = (GameModeEnumLength - 1) * -1;
		}
		//atualiza o modo de jogo no game manager
		mGameManager.mGameMode = (GameMode)(indexMode + selectionMode);

		//Desativa a selecao anterior
		//OptionsMode[indexMode].SetActive(false);
		//Ativa a selecao atual
		//OptionsMode[(int)mGameManager.mGameMode].SetActive(true);
	}
	#endregion
}
