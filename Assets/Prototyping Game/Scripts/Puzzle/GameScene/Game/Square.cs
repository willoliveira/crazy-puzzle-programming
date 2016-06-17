using UnityEngine;
using System.Collections;

namespace PrototypingGame
{
	public class Square : MonoBehaviour
	{
		private int row;
		private int column;

		private int rowCorrect;
		private int columnCorrect;

		private int order;
		/// <summary>
		/// Informacao da linha que a peca esta
		/// </summary>
		public int Row
		{
			get { return row; }
			set { row = value; }
		}
		/// <summary>
		/// Informacao da coluna que a peca esta
		/// </summary>
		public int Column
		{
			get { return column; }
			set { column = value; }
		}

		/// <summary>
		/// Informacao da linha que a peca esta
		/// </summary>
		public int RowCorrect
		{
			get { return rowCorrect; }
			set { rowCorrect = value; }
		}
		/// <summary>
		/// Informacao da coluna que a peca esta
		/// </summary>
		public int ColumnCorrect
		{
			get { return columnCorrect; }
			set { columnCorrect = value; }
		}

		/// <summary>
		/// Nao lembro por que coloquei isso...
		/// </summary>
		public int Order
		{
			get { return order; }
			set { order = value; }
		}
		/// <summary>
		/// Normaliza o nome de peca referente ao tile que ela esta
		/// </summary>
		public void NormalizePieceName() {
			//renomeia a peça pela posição que ela ocupa
			transform.name = "square-" + Row + "-" + Column;
		}
	}
}