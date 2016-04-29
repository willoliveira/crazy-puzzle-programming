using UnityEngine;
using System.Collections;

public class ChangeMusic : MonoBehaviour {

    public AudioClip level1Music;
    public AudioClip level2Music;
    private AudioSource source;

	// Use this for initialization
	void Awake ()
    {
        source = GetComponent<AudioSource>();
	}
	
	void OnLevelWasLoaded (int level)
    {
	    if (level == 1)
        {
            source.clip = level1Music;
            source.Play();
        } else if ( level == 2)
        {
            source.clip = level2Music;
            source.Play();
        }
	}
}
