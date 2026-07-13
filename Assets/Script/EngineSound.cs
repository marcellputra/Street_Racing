using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EngineSound : MonoBehaviour
{
    public static EngineSound Instance;

    [Header("Referensi Mobil")]
    public Rigidbody carRb;

    [Header("Pitch Settings")]
    public float idlePitch = 0.8f;
    public float maxPitch = 1.6f;
    public float maxSpeed = 120f;

    [Header("Volume")]
    [Range(0f, 1f)] public float engineVolume = 1f;

    [Header("Respons")]
    public float pitchSmooth = 3f;

    private AudioSource audioSource;
    private bool engineStarted = false;
    private bool isAccelerating = false;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        // Ambil volume engine dari PlayerPrefs
        float savedVolume = PlayerPrefs.GetFloat("EngineVolume", engineVolume);
        audioSource.volume = savedVolume;

        audioSource.pitch = idlePitch;
    }

    private void Update()
    {
        if (!engineStarted || audioSource == null)
            return;

        float targetPitch = idlePitch;

        // Kalau sedang gas, pitch naik sesuai kecepatan mobil
        if (isAccelerating && carRb != null)
        {
            float speed = carRb.linearVelocity.magnitude * 3.6f; // m/s -> km/h
            speed = Mathf.Clamp(speed, 0f, maxSpeed);

            float t = speed / maxSpeed;
            targetPitch = Mathf.Lerp(idlePitch, maxPitch, t);
        }
        else
        {
            // Kalau gas dilepas, pitch turun ke idle
            targetPitch = idlePitch;
        }

        audioSource.pitch = Mathf.Lerp(
            audioSource.pitch,
            targetPitch,
            Time.unscaledDeltaTime * pitchSmooth
        );
    }

    public void StartEngine()
    {
        engineStarted = true;

        // Pastikan volume selalu mengikuti setting terakhir
        audioSource.volume = PlayerPrefs.GetFloat("EngineVolume", engineVolume);

        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    public void SetAccelerating(bool accelerating)
    {
        isAccelerating = accelerating;
    }

    public bool IsAccelerating()
    {
        return isAccelerating;
    }

    public void StopEngine()
    {
        engineStarted = false;
        isAccelerating = false;

        if (audioSource.isPlaying)
            audioSource.Stop();

        audioSource.pitch = idlePitch;
    }

    public void PauseEngine()
    {
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Pause();
    }

    public void ResumeEngine()
    {
        if (audioSource != null && engineStarted)
            audioSource.UnPause();
    }

    public void SetVolume(float volume)
    {
        if (audioSource == null) return;

        audioSource.volume = volume;
        PlayerPrefs.SetFloat("EngineVolume", volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return PlayerPrefs.GetFloat("EngineVolume", engineVolume);
    }
}