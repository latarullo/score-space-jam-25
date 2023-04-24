using UnityEngine;

public class MusicManager : MonoBehaviour {

    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;

    private float volume = .3f;

    private void Awake() {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        volume = Mathf.Round(PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 0.3f));
        audioSource.volume = volume;
    }

    public void ChangeVolume() {
        volume += .1f;
        if (volume > 1.1) {
            volume = 0;
        } else if ( volume > 1) {
            volume = 1;
        }

        audioSource.volume = volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }
    public float GetVolume() {
        return volume;
    }
}