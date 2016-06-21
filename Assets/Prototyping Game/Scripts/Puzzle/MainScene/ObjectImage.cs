using UnityEngine;
using System.Collections;

public class ObjectImage : MonoBehaviour
{
	private int webformatWidth;
	private int webformatHeight;
	private string webformatURL;

	public int WebformatWidth
	{
		get { return webformatWidth; }
		set { webformatWidth = value; }
	}

	public int WebformatHeight
	{
		get { return webformatHeight; }
		set { webformatHeight = value; }
	}

	public string WebformatURL
	{
		get { return webformatURL; }
		set { webformatURL = value; }
	}
}