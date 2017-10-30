using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace CountingSheeps.NewOldPuzzle
{
	public class ObjectImage : MonoBehaviour, IPointerClickHandler
	{
		#region PRIVATE VARS
		private string imageURL;
		private Texture2D imageTexture;
		private SelectImage mSelectImages;
		#endregion

		#region PUBLIC VARS
		public string ImageURL
		{
			get { return imageURL; }
			set { imageURL = value; }
		}

		public Texture2D ImageTexture
		{
			get { return imageTexture; }
			set { imageTexture = value; }
		}
		#endregion

		void Start()
		{
			//guarda referencia do select images
			mSelectImages = SelectImage.instance;
		}

		#region PUBLIC METHODS
		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		public void OnPointerClick(PointerEventData data)
		{
			mSelectImages.SetImageChoice(gameObject);
		}
		#endregion

		//private 	
	}
}