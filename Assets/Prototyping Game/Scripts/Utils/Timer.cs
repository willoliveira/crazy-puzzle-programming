using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	private float timer;

	private int seconds;
	private int minutes;
	private int fraction;

	private Text TimeTextUI;

	private bool disable;

	//Get's
	public int Seconds{
		get { return seconds; }
	}

	public int Minutes
	{
		get { return minutes; }
	}

	public int Fraction
	{
		get { return fraction; }
	}

	// Use this for initialization
	void Start () {
		timer = 0;
		//pega o texto do timer
		TimeTextUI = GetComponent<Text>();
	}
	
	void Update()
	{
		timer += Time.deltaTime;
		//converte para tempo
		minutes = (int) timer / 60;
		seconds = (int) timer % 60;
		fraction = (int) (timer * 100) % 100;
		//se houver texto
		if (TimeTextUI) {
			//TimeTextUI.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
			TimeTextUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
		}
	}
}
