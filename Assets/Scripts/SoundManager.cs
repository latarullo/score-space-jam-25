using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour {

    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    public static SoundManager Instance { get; private set; }
    private void Awake() {
        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    //[SerializeField] private AudioClipRefSO audioClipRefSO;

    private float volume;

    private void Start() {
        //DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        //DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        //CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        //Player.Instance.OnPickedSomething += Instance_OnPickedSomething;
        //BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        //TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    //private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e) {
    //    TrashCounter trashCounter = sender as TrashCounter;
    //    this.PlaySound(audioClipRefSO.objectDrop, trashCounter.transform.position);
    //}

    //private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e) {
    //    BaseCounter baseCounter = sender as BaseCounter;
    //    this.PlaySound(audioClipRefSO.objectDrop, baseCounter.transform.position);
    //}

    //private void Instance_OnPickedSomething(object sender, System.EventArgs e) {
    //    Player player = sender as Player;
    //    this.PlaySound(audioClipRefSO.objectPickup, player.transform.position);
    //}

    //private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e) {
    //    CuttingCounter cuttingCounter = sender as CuttingCounter;
    //    this.PlaySound(audioClipRefSO.chop, cuttingCounter.transform.position);
    //}

    //private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e) {
    //    this.PlaySound(audioClipRefSO.deliveryFail, DeliveryCounter.Instance.transform.position);
    //}

    //private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e) {
    //    this.PlaySound(audioClipRefSO.deliverySuccess, DeliveryCounter.Instance.transform.position);
    //}

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    //public void PlayFootstepsSound(Vector3 position, float volume) {
    //    this.PlaySound(audioClipRefSO.footstep, position, volume);
    //}

    public void ChangeVolume() {
        volume += .1f;
        if (volume > 1) {
            volume = 0;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }
    public float GetVolume() {
        return volume;
    }
}
