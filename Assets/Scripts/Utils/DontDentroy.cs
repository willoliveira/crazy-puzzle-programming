using UnityEngine;
using System.Collections;

public class DontDentroy : MonoBehaviour {


	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(gameObject);
	}
	
}
