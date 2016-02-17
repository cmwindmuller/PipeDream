using UnityEngine;
using System.Collections;

public class AudioHandler : MonoBehaviour {

	private static AudioHandler _instance;

	private AudioSource audioSource;

	[System.Serializable]
	public struct Music
	{
		public AudioClip clip;
		public float volume;
		public bool loop;
	}
	[System.Serializable]
	public struct Sound
	{
		public AudioClip clip;
		public float volume;
	}

	[Header("Songs")]
	public Music song_logo;
	public Music song_game;
	public Music song_menu;
	//public Music song_death;
	private Music current;
	private Music next;
	private bool isPreparedToPlay;
	private const float fadeDuration = 0.3f;
	private float fadeTime;
	[Header("Audio")]
	public Sound title;
	public Sound start;
	public Sound death;
	public Sound hiScore;
	public Sound jump;
	public Sound turn;
	public Sound hit;
	public Sound shark;
	public Sound emerge;
	public Sound ring;
	public Sound seahorse;
	public Sound seagull;
	public Sound poop;

	//only important 'event'
	void Awake()
	{
		_instance = this;
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.loop = true;
		audioSource.playOnAwake = false;
		isPreparedToPlay = true;
		fadeTime = -1;
		DontDestroyOnLoad(transform.gameObject);
	}

	void Update()
	{
		if(!isPreparedToPlay && fadeTime > 0)
		{
			if(Time.time > fadeTime)
			{
				PlayNextMusic();
			}
			else
			{
				float volume = (fadeTime - Time.time) / fadeDuration * current.volume;
				audioSource.volume = volume;
			}
		}
	}

	public void PlayNextMusic()
	{
		if(current.clip != next.clip)
		{
			current = next;
			audioSource.clip = current.clip;
			audioSource.volume = current.volume;
			audioSource.Play();
			isPreparedToPlay = false;
			fadeTime = -1;
		}
	}

	public void PlayMusic(Music music)
	{
		audioSource.loop = music.loop;
		next = music;
		if(isPreparedToPlay)
			PlayNextMusic();
		else
			fadeTime = Time.time + fadeDuration;
	}

	public void StopCurrentMusic()
	{
		audioSource.Stop();
		isPreparedToPlay = true;
	}
	
	public void PlaySound(Sound sound)
	{
		AudioSource.PlayClipAtPoint(sound.clip,Vector3.zero,sound.volume);
	}
	
	public static AudioHandler get()
	{
		return _instance;
	}

	//******* music calling functions *******
	public void PlayLogoMusic()
	{
		PlayMusic(song_logo);
	}
	public void PlayMenuMusic()
	{
		PlayMusic(song_menu);
	}

	public void PlayGameMusic()
	{
		PlayMusic(song_game);
	}

	public void PlayDeathMusic()
	{
		//PlayMusic(song_death);
	}

	//******* sound calling functions *******
	public void Title()
	{
		PlaySound(title);
	}

	public void GameStart()
	{
		PlaySound(start);
	}

	public void Death()
	{
		PlaySound(death);
	}

	public void HiScore()
	{
		PlaySound (hiScore);
	}

	public void Jump()
	{
		PlaySound(jump);
	}

	public void Turn()
	{
		PlaySound(turn);
	}

	public void Hit()
	{
		PlaySound(hit);
	}

	public void Shark()
	{
		PlaySound(shark);
	}

	public void Emerge()
	{
		PlaySound(emerge);
	}

	public void Ring()
	{
		PlaySound(ring);
	}
	
	public void Seahorse()
	{
		PlaySound(seahorse);
	}
	
	public void Seagull()
	{
		PlaySound(seagull);
	}
	
	public void Poop()
	{
		PlaySound(poop);
	}
}