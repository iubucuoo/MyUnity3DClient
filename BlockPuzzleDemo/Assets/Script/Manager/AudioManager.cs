using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public static AudioManager Instance=null;

	public bool isPlaying= true;

	void Awake()
	{
		Instance = this;
	}
    

	public void PlayMusic(string music)
	{
		if (isPlaying == false) {
			return;
		}

		AudioController.PlayMusic (music);
	}

	public void Play(string sound)
	{
		if (isPlaying == false) {
			return;
		}

		AudioController.Play (sound);
	}

	public void ButtonClick()
	{
		if (isPlaying == false) {
			return;
		}
		Play ("click");
	}
    public void PlayGameOver()
    {
        if (isPlaying == false)
        {
            return;
        }
        Play("gameover");
    }
    public void PlayGameOpen()
    {
        if (isPlaying == false)
        {
            return;
        }
        Play("gameopen");
    }
    public void PlayPick()
	{
		if (isPlaying == false) {
			return;
		}
		Play ("pick");
	}
    public void PlayReturn()
    {
        if (isPlaying == false)
        {
            return;
        }
        Play("return");
    }
    public void PlayPlace()
	{
		if (isPlaying == false) {
			return;
		}
		Play ("place");
	}
}
