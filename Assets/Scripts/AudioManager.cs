using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] sfx;

    [SerializeField]
    private AudioSource[] bgm;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(int soundToPlay)
    {
        if (soundToPlay < sfx.Length)
            sfx[soundToPlay].Play();
    }

    public void PlayBgm(int musicToPlay)
    {
        if (!bgm[musicToPlay].isPlaying)
        {
			StopMusic();

			if (musicToPlay < bgm.Length)
				bgm[musicToPlay].Play();
		}
    }

    public void StopMusic()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
}
