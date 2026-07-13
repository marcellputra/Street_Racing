using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider engineSlider;

    private void Start()
    {
        // =========================
        // MUSIC SLIDER
        // =========================
        if (musicSlider != null)
        {
            float savedMusic = PlayerPrefs.GetFloat("MusicVolume", 1f);
            musicSlider.value = savedMusic;

            musicSlider.onValueChanged.RemoveAllListeners();
            musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        }

        // =========================
        // ENGINE SLIDER
        // =========================
        if (engineSlider != null)
        {
            float savedEngine = PlayerPrefs.GetFloat("EngineVolume", 1f);
            engineSlider.value = savedEngine;

            engineSlider.onValueChanged.RemoveAllListeners();
            engineSlider.onValueChanged.AddListener(ChangeEngineVolume);
        }
    }

    public void ChangeMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();

        if (MusicManager.Instance != null)
            MusicManager.Instance.SetVolume(value);
    }

    public void ChangeEngineVolume(float value)
    {
        PlayerPrefs.SetFloat("EngineVolume", value);
        PlayerPrefs.Save();

        // Kalau sedang ada engine aktif di scene ini, langsung update juga
        if (EngineSound.Instance != null)
            EngineSound.Instance.SetVolume(value);
    }
}