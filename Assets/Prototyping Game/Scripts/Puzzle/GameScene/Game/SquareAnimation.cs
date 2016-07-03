using UnityEngine;
using System.Collections;

public class SquareAnimation : MonoBehaviour {

	private Animator mAnimation;

	void Start ()
	{
		mAnimation = GetComponent<Animator>();
	}

	public void EndScaleInAnimation()
	{
		//Debug.Log("EndScaleInAnimation");
		//mAnimation.SetTrigger("ScaleIn");
	}

	public void EndScaleOutAnimation()
	{
		//Debug.Log("EndScaleOutAnimation");
		//mAnimation.SetTrigger("ScaleOut");
	}
}
