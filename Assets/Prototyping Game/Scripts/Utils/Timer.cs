using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	private float timer;

	private int seconds;
	private int minutes;
	private int fraction;
	
	public Text TimeTextUI;

	public bool IsEnable;
	public bool IsPause;

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
	}
	
	void Update()
	{
		if (IsEnable)
		{
			timer += Time.deltaTime;
			//converte para tempo
			minutes = (int)timer / 60;
			seconds = (int)timer % 60;
			fraction = (int)(timer * 100) % 100;
			//se houver texto
			if (TimeTextUI)
			{
				TimeTextUI.text = TimerFormatted();
			}
		}
	}

	public void ClearTimer()
	{
		timer = 0;
	}
	

	public string TimerFormatted()
	{
		return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
	}
}
