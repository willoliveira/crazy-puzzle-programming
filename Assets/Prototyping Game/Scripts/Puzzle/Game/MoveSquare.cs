using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using UnityStandardAssets.CrossPlatformInput;

namespace PrototypingGame
{

	public class MoveSquare : MonoBehaviour
	{
		public Text getAxis;

		private bool axisHorizontalDown = false;
		private bool axisVerticalDown = false;
		private BoardManager mBoardManager;
		/// <summary>
		/// Inicia
		/// </summary>
		void Start()
		{
			mBoardManager = GetComponent<BoardManager>();
		}
		/// <summary>
		/// Metodo update
		/// Pega o eixo pressionado e passa para a funcao que move a peca
		/// </summary>
		void Update()
		{
			//pega a direção do movimento
			int HorizontalAxis = RoudAxis(CrossPlatformInputManager.GetAxis("Horizontal"));
			int VerticalAxis = RoudAxis(CrossPlatformInputManager.GetAxis("Vertical"));
			//mostra na tela o get axis
			getAxis.text = "HorizontalAxis: " + HorizontalAxis + " - VerticalAxis: " + VerticalAxis;
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
			//pega o game object
			Transform cacheGameObject;
			cacheGameObject = mBoardManager.Board.Find("square-" + (mBoardManager.posBlank.x - dirY) + "-" + (mBoardManager.posBlank.y - dirX));
			//se houver uma gameobject
			if (cacheGameObject == null)
			{
				//nao pode mover a peca
				Debug.Log("Não pode mover para essa direção");
				return;
			}
			//Muda a posicao da peca
			cacheGameObject.position = new Vector3(mBoardManager.posBlank.y * mBoardManager.Board.localScale.x, (mBoardManager.columns - 1 - mBoardManager.posBlank.x) * mBoardManager.Board.localScale.x, 0);
			cacheGameObject.name = "square-" + mBoardManager.posBlank.x + "-" + mBoardManager.posBlank.y;
			//atualiza o x da peça vazia
			mBoardManager.posBlank.x = mBoardManager.posBlank.x - dirY;
			mBoardManager.posBlank.y = mBoardManager.posBlank.y - dirX;
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
		public IEnumerator AnimateAndMoveSmooth(Transform piece, Vector3 end)
		{
			//http://answers.unity3d.com/questions/628200/get-length-of-animator-statetransition.html
			//faz a peca animar dando scale
			piece.GetComponent<Animator>().SetTrigger("ScaleIn");
			//espera o tempo da animacao acabar
			yield return new WaitForSeconds(1f);
			//faz a peça se mover
			yield return StartCoroutine(MovePieceSmooth(piece, end));
			//
			//yield return new WaitForSeconds(0.2f);
			//faz a peca se dar o scale de volta
			piece.GetComponent<Animator>().SetTrigger("ScaleOut");
		}
		/// <summary>
		/// Move a peca com smooth
		/// </summary>
		/// <param name="piece"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		public IEnumerator MovePieceSmooth(Transform piece, Vector3 end)
		{
			// Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter.
			//Square magnitude is used instead of magnitude because it's computationally cheaper.
			float sqrRemainingDistance = (piece.transform.position - end).sqrMagnitude;
			//While that distance is greater than a very small amount (Epsilon, almost zero):
			while (sqrRemainingDistance > float.Epsilon)
			{
				//Find a new position proportionally closer to the end, based on the moveTime
				Vector3 newPostion = end;
				newPostion = Vector3.MoveTowards(piece.position, end, 1f / 0.1f * Time.deltaTime);
				//newPostion.z = 3f;
				//Call MovePosition on attached Rigidbody2D and move it to the calculated position.
				piece.position = newPostion;
				//Recalculate the remaining distance after moving.
				sqrRemainingDistance = (piece.transform.position - end).sqrMagnitude;
				//Return and loop until sqrRemainingDistance is close enough to zero to end the function
				yield return new WaitForSeconds(0.01f);
			}
			yield return null;
		}
	}
}