using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

	static MusicPlayer instance;
	AudioManager audioManager;
	static string currentPlaying;
	[SerializeField]
	string MenuMusic = "MenuMusic";
	[SerializeField]
	string CreditsMusic = "CreditsMusic";
	[SerializeField]
	string SwampMusic = "SwampMusic";
	[SerializeField]
	string RockFirelyMusic = "RockFirelyMusic";
	[SerializeField]
	string CityMusic = "CityMusic";

	void Awake () {
		if (instance == null)
			instance = this;
		else {
			Destroy (gameObject);
			return;
		}

		DontDestroyOnLoad (gameObject);
	}

	void Start () {
		audioManager = GameObject.Find ("AudioManager").GetComponent<AudioManager> ();

		//audioManager.StopAllSounds ();
	}

	void Update(){
		if (SceneManager.GetActiveScene ().buildIndex == 0 && currentPlaying != MenuMusic) {
			audioManager.StopAllSoundsButThis (MenuMusic);
			audioManager.Play (MenuMusic);
			currentPlaying = MenuMusic;
		}
		else if (SceneManager.GetActiveScene ().buildIndex == 1 && currentPlaying != CreditsMusic) {
			audioManager.StopAllSoundsButThis (CreditsMusic);
			audioManager.Play (CreditsMusic);
			currentPlaying = CreditsMusic;
		}
		else if (SceneManager.GetActiveScene ().buildIndex == 2 && currentPlaying != CityMusic) {
			audioManager.StopAllSoundsButThis (CityMusic);
			audioManager.Play (CityMusic);
			currentPlaying = CityMusic;
		}
		else if (SceneManager.GetActiveScene ().buildIndex == 3 && currentPlaying != SwampMusic) {
			audioManager.StopAllSoundsButThis (SwampMusic);
			audioManager.Play (SwampMusic);
			currentPlaying = SwampMusic;
		}
		else if (SceneManager.GetActiveScene ().buildIndex == 4 && currentPlaying != RockFirelyMusic) {
			audioManager.StopAllSoundsButThis (RockFirelyMusic);
			audioManager.Play (RockFirelyMusic);
			currentPlaying = RockFirelyMusic;
		}
	}
}
