using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class SquareDrop : MonoBehaviour, IDropHandler
{
	#region PUBLIC VARS
	[HideInInspector]
	public static RectTransform DropArea;
	public AudioClip DropSound;
	#endregion

	#region PRIVATE VARS
	private BoardManager mBoardManager;
	private AudioManager mAudioManager;
	private DragAndDropUI mDragAndDropUI;
	#endregion

	void Start()
	{
		mBoardManager = BoardManager.instance;
		mAudioManager = AudioManager.instance;
	}

	#region PUBLIC METHODS
	public void OnDrop(PointerEventData eventData)
	{
		//Debug.Log("OnDrop");
		if (eventData.pointerDrag)
		{
			//Pega a referencia do objeto que estava sendo draggado
			mDragAndDropUI = eventData.pointerDrag.GetComponent<DragAndDropUI>();
			//verifica se essa peca possui o drag habilitado
			if(mDragAndDropUI.EnabledDrag)
			{
				//seta a peca dragada como dropada, indicando que ela foi colocada no drop area
				mDragAndDropUI.IsDropped = true;
				//seta a posicao da peca droppada, a posicao da area de drop
				RectTransform SquareDropped = eventData.pointerDrag.GetComponent<RectTransform>();
				//seta a posicao na peca
				SquareDropped.anchoredPosition = SquareDrop.DropArea.anchoredPosition;//GameObject.Find("DropArea").GetComponent<RectTransform>().anchoredPosition;
				//avisa ao board que a peca foi droppada
				mBoardManager.SetPositionSquareBlank(eventData.pointerDrag);
				//toca o som de drop
				mAudioManager.Play(DropSound, this.name, AudioType.SFX);
			}
		}
	}
	#endregion
}