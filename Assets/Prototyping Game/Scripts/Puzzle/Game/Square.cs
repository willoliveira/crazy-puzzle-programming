using UnityEngine;
using System.Collections;

namespace PrototypingGame
{
	public class Square : MonoBehaviour
	{

		private int row;
		private int column;
		private int order;

		public int Row
		{
			get { return row; }
			set { row = value; }
		}

		public int Column
		{
			get { return column; }
			set { column = value; }
		}

		public int Order
		{
			get { return order; }
			set { order = value; }
		}

		public void NormalizePiece() { }


		//public Square(int x, int y, int order)
		//{
		//    this.x = x;
		//    this.y = y;
		//    this.order = order;
		//}
	}
}