using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

	private static AudioManager instance;
	public Sound[] sounds;

	void Awake () {
		if (instance == null)
			instance = this;
		else {
			Destroy (gameObject);
			return;
		}
		
		DontDestroyOnLoad (gameObject);

		foreach (Sound s in sounds) {
			s.source = gameObject.AddComponent<AudioSource> ();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
			s.source.outputAudioMixerGroup = s.audioMixerGroup;
		}
	}

	void Start(){
	}

	void Update(){

	}
	
	public void Play(string name){
		Sound s = Array.Find (sounds, sound => sound.name == name);
		if (s == null) {
			Debug.LogError ("Sound: " + name + " not found!");
			return;
		}
		s.source.Play ();
	}

	public void Stop(string name){
		Sound s = Array.Find (sounds, sound => sound.name == name);
		if (s == null) {
			Debug.LogError ("Sound: " + name + " not found!");
			return;
		}
		s.source.Stop ();
	}

	public void StopAllSounds(){
		foreach (Sound s in sounds) {
			s.source.Stop ();
		}
	}

	//stop all sounds except the one with the name
	public void StopAllSoundsButThis(string name){
		foreach (Sound s in sounds) {
			if(s.name != name)
				s.source.Stop ();
		}
	}

	public bool IsPlaying(string name){
		Sound s = Array.Find (sounds, sound => sound.name == name);
		return s.source.isPlaying;
	}
}
