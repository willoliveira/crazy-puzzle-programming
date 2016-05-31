using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using UnityStandardAssets.CrossPlatformInput;

namespace PrototypingGame
{

	public class MoveSquare : MonoBehaviour
	{

		private bool axisHorizontalDown = false;
		private bool axisVerticalDown = false;

		//texto
		public Text getAxis;

		private BoardManager gameManager;

		// Use this for initialization
		void Start()
		{

			gameManager = GetComponent<BoardManager>();
		}

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

		private int RoudAxis(float num)
		{
			int floatToInt = 0;
			if (num > 0)
				floatToInt = Mathf.CeilToInt(num);
			else if (num < 0)
				floatToInt = Mathf.FloorToInt(num);
			return floatToInt;
		}


		private void MovePuzzle(int dirX, int dirY)
		{
			//pega o game object
			Transform cacheGameObject;
			cacheGameObject = gameManager.Board.Find("square-" + (gameManager.posBlank.x - dirY) + "-" + (gameManager.posBlank.y - dirX));
			if (cacheGameObject == null)
			{
				Debug.Log("Não pode mover para essa direção");
				return;
			}
			//move
			//new Vector3(1, 2, 2);
			cacheGameObject.position = new Vector3(gameManager.posBlank.y * gameManager.Board.localScale.x, (gameManager.columns - 1 - gameManager.posBlank.x) * gameManager.Board.localScale.x, 0);
			cacheGameObject.name = "square-" + gameManager.posBlank.x + "-" + gameManager.posBlank.y;
			//posição correta da peça
			//Debug.Log(cacheGameObject.GetComponent<Square>().Row + "-" + cacheGameObject.GetComponent<Square>().Column + "- Position Board: " + Board.localScale.x);
			//atualiza o x da peça vazia
			gameManager.posBlank.x = gameManager.posBlank.x - dirY;
			gameManager.posBlank.y = gameManager.posBlank.y - dirX;
		}

		public IEnumerator MovePieceSmooth(Transform piece, Vector3 end)
		{
			// Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter.
			//Square magnitude is used instead of magnitude because it's computationally cheaper.
			float sqrRemainingDistance = (piece.transform.position - end).sqrMagnitude;
			//While that distance is greater than a very small amount (Epsilon, almost zero):
			while (sqrRemainingDistance > float.Epsilon)
			{
				//Find a new position proportionally closer to the end, based on the moveTime
				Vector3 newPostion;
				newPostion = Vector3.MoveTowards(piece.position, end, 1f / 0.1f * Time.deltaTime);
				//newPostion.z = 3f;
				//Call MovePosition on attached Rigidbody2D and move it to the calculated position.
				piece.position = newPostion;
				//Recalculate the remaining distance after moving.
				sqrRemainingDistance = (piece.transform.position - end).sqrMagnitude;
				//Return and loop until sqrRemainingDistance is close enough to zero to end the function
				yield return new WaitForSeconds(0.01f);
			}
		}
	}
}