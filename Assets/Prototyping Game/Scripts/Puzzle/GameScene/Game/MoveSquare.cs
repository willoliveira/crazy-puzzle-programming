using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using UnityStandardAssets.CrossPlatformInput;

namespace PrototypingGame
{

	public class MoveSquare : MonoBehaviour
	{
		#region TESTES UI
		public Text getAxis;
		#endregion
		private bool axisHorizontalDown = false;
		private bool axisVerticalDown = false;
		private BoardManager mBoardManager;

		private int SquareAnimationsEnd = 0;
		private int columns;

		public bool IsEnable = false;
		/// <summary>
		/// Inicia
		/// </summary>
		void Start()
		{
			mBoardManager = GetComponent<BoardManager>();
		}

		#region PRIVATE METHODS
		/// <summary>
		/// Metodo update
		/// Pega o eixo pressionado e passa para a funcao que move a peca
		/// </summary>
		void Update()
		{
			if (IsEnable)
			{
				//pega a direção do movimento
				int HorizontalAxis = RoudAxis(CrossPlatformInputManager.GetAxis("Horizontal"));
				int VerticalAxis = RoudAxis(CrossPlatformInputManager.GetAxis("Vertical"));
				//mostra na tela o get axis
				//getAxis.text = "HorizontalAxis: " + HorizontalAxis + " - VerticalAxis: " + VerticalAxis;
				/*Horizontal*/
				if (HorizontalAxis == 1 || HorizontalAxis == -1)
				{
					if (!axisHorizontalDown)
					{
						int dir = HorizontalAxis;
						MovePuzzle(dir, 0);
						axisHorizontalDown = true;
					}
				}
				else if (HorizontalAxis == 0)
				{
					axisHorizontalDown = false;
				}
				/*Vertical*/
				if (VerticalAxis == 1 || VerticalAxis == -1)
				{
					if (!axisVerticalDown)
					{
						int dir = VerticalAxis;
						MovePuzzle(0, dir * -1);
						axisVerticalDown = true;
					}
				}
				else if (VerticalAxis == 0)
				{
					axisVerticalDown = false;
				}
			}
		}
		/// <summary>
		/// Arredonda o eixo precionado, Horizontal e Vertical
		/// </summary>
		/// <param name="num"></param>
		/// <returns></returns>
		private int RoudAxis(float num)
		{
			int floatToInt = 0;
			if (num > 0)
				floatToInt = Mathf.CeilToInt(num);
			else if (num < 0)
				floatToInt = Mathf.FloorToInt(num);
			return floatToInt;
		}
		/// <summary>
		/// Move a peca para posicao desejada, com base na posicao desejada
		/// </summary>
		/// <param name="dirX"></param>
		/// <param name="dirY"></param>
		private void MovePuzzle(int dirX, int dirY)
		{
			RectTransform SquareRectTransform;
			Transform SquareTransform;
			//pega o game object
			SquareTransform = mBoardManager.Board.Find("square-" + (mBoardManager.PositionBlank.Row - dirY) + "-" + (mBoardManager.PositionBlank.Column - dirX));
			//se houver uma gameobject
			if (SquareTransform == null)
			{
				//TODO: colocar um audio de error
				//nao pode mover a peca
				Debug.Log("Não pode mover para essa direção");
				return;
			}
			//pega o rect transform do square
			SquareRectTransform = SquareTransform.GetComponent<RectTransform>();
			//Muda a posicao da peca
			SquareRectTransform.anchoredPosition = new Vector3((mBoardManager.PositionBlank.Column * mBoardManager.PieceSize) + mBoardManager.PieceSize/2, (mBoardManager.PositionBlank.Row * -mBoardManager.PieceSize) - mBoardManager.PieceSize/2, 0);
			//seta a posicao do PositionBlank GameObject
			mBoardManager.SetPositionSquareBlank(SquareTransform.gameObject);
		}
		/// <summary>
		/// Seta o fim da animacao de movimento
		/// </summary>
		private void SetEndMoveAnimationSquare()
		{
			SquareAnimationsEnd += 1;
			if ((mBoardManager.columns * mBoardManager.columns) - 1 == SquareAnimationsEnd)
			{
				Debug.Log("SetEndMoveAnimationSquare");
				mBoardManager.EndRandomPieces();
			}
		}
		#endregion

		#region PUBLIC METHODS
		/// <summary>
		/// TODO: Nao sei se é o melhor lugar pra isso ficar, mas no board ia ficar meio zoneado demais
		/// </summary>
		public void InitRandomPieces()
		{
			SquareAnimationsEnd = 0;
		}
		/// <summary>
		/// Anima e move a peca para a posicao desejada
		/// - Anima o scaleIn da peca
		/// - Muda ela para a posicao randomizada
		/// - Volta para o tamanho torma, com o scaleOut
		/// TODO: conseguir contar o tempo da animacao e tirar os valores fixos
		/// </summary>
		/// <param name="piece"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		public IEnumerator AnimateAndMoveSmooth(RectTransform piece, Vector3 end)
		{
			//http://answers.unity3d.com/questions/628200/get-length-of-animator-statetransition.html
			//faz a peca animar dando scale
			piece.GetComponent<Animator>().SetTrigger("ScaleIn");
			//espera o tempo da animacao acabar
			yield return new WaitForSeconds(1f);
			//faz a peça se mover
			yield return StartCoroutine(MovePieceSmooth(piece, end));
			//faz a peca se dar o scale de volta
			piece.GetComponent<Animator>().SetTrigger("ScaleOut");
			//espera o tempod a animacao acabar
			yield return new WaitForSeconds(1f);
			//seta o fim da movimentacao
			SetEndMoveAnimationSquare();
		}
		/// <summary>
		/// Move a peca com smooth
		/// </summary>
		/// <param name="piece"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		public IEnumerator MovePieceSmooth(RectTransform piece, Vector2 end)
		{
			// Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter.
			//Square magnitude is used instead of magnitude because it's computationally cheaper.
			float sqrRemainingDistance = (piece.anchoredPosition - end).sqrMagnitude;
			//While that distance is greater than a very small amount (Epsilon, almost zero):
			while (sqrRemainingDistance > float.Epsilon)
			{
				//Find a new position proportionally closer to the end, based on the moveTime
				Vector3 newPostion = Vector3.MoveTowards(piece.anchoredPosition, end, 1f / 0.01f * Time.deltaTime);
				//Call MovePosition on attached Rigidbody2D and move it to the calculated position.
				piece.anchoredPosition = newPostion;
				//Recalculate the remaining distance after moving.
				sqrRemainingDistance = (piece.anchoredPosition - end).sqrMagnitude;
				//Return and loop until sqrRemainingDistance is close enough to zero to end the function
				yield return null;
			}
			//yield return null;
		}
		#endregion
	}
}