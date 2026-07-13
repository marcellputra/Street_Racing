using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource audioSource;

    [Header("Volume Musik")]
    [Range(0f, 1f)] public float menuVolumeMultiplier = 1f;
    [Range(0f, 1f)] public float gameplayVolumeMultiplier = 0.35f;

    private float baseVolume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();

            // Ambil volume utama dari PlayerPrefs
            baseVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);

            // Terapkan volume sesuai scene pertama
            ApplySceneVolume(SceneManager.GetActiveScene().name);

            // Dengarkan perpindahan scene
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplySceneVolume(scene.name);
    }

    private void ApplySceneVolume(string sceneName)
    {
        if (audioSource == null) return;

        // Main menu = volume normal
        if (sceneName == "MainMenu")
        {
            audioSource.volume = baseVolume * menuVolumeMultiplier;
        }
        // Gameplay = volume dikecilkan
        else if (sceneName == "track2" || sceneName == "track3" || sceneName == "track4")
        {
            audioSource.volume = baseVolume * gameplayVolumeMultiplier;
        }
        else
        {
            audioSource.volume = baseVolume;
        }
    }

    public void SetVolume(float volume)
    {
        baseVolume = volume;

        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();

        ApplySceneVolume(SceneManager.GetActiveScene().name);
    }

    public float GetVolume()
    {
        return baseVolume;
    }
}