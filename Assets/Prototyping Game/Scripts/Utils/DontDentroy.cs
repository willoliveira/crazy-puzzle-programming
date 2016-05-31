using UnityEngine;
using System.Collections;

namespace PrototypingGame
{
	public class DontDentroy : MonoBehaviour
	{


		// Use this for initialization
		void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}

	}
}