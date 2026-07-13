using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [Header("UI Timer")]
    public TextMeshProUGUI timerText;

    [Header("Time Per Level")]
    public float track2Time = 90f;   // Level 1 = 1 menit 30 detik
    public float track3Time = 240f;  // Level 2 = 4 menit

    [Header("Time Over Panel")]
    public GameObject timeOverPanel;

    [Header("Gameplay UI")]
    public GameObject joystick;
    public GameObject gasButton;
    public GameObject brakeButton;
    public GameObject pauseButton;
    public GameObject speedText;
    public GameObject speedometerUI;

    [Header("Sound")]
    public EngineSound engineSound;

    [HideInInspector] public float timeLeft;
    public bool timerStarted = false;
    public bool timerStopped = false;

    private void Start()
    {
        SetTimeByScene();

        if (timeOverPanel != null)
            timeOverPanel.SetActive(false);

        UpdateTimerText();
    }

    private void Update()
    {
        // Start timer dari keyboard
        if (!timerStarted)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                StartTimer();
            }
        }

        if (!timerStarted || timerStopped)
            return;

        timeLeft -= Time.deltaTime;

        if (timeLeft < 0)
            timeLeft = 0;

        UpdateTimerText();

        if (timeLeft <= 0)
        {
            TimeOver();
        }
    }

    private void SetTimeByScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "track2")
            timeLeft = track2Time;
        else if (sceneName == "track3")
            timeLeft = track3Time;
        else
            timeLeft = 90f; // default
    }

    public void StartTimer()
    {
        if (!timerStarted)
            timerStarted = true;
    }

    public void StopTimer()
    {
        timerStopped = true;
    }

    public string GetFinalTime()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void UpdateTimerText()
    {
        if (timerText == null) return;

        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void TimeOver()
    {
        timerStopped = true;

        // Matikan suara mesin
        if (engineSound != null)
            engineSound.StopEngine();

        // Tampilkan panel Time Over
        if (timeOverPanel != null)
            timeOverPanel.SetActive(true);

        // Sembunyikan UI gameplay
        if (joystick != null) joystick.SetActive(false);
        if (gasButton != null) gasButton.SetActive(false);
        if (brakeButton != null) brakeButton.SetActive(false);
        if (pauseButton != null) pauseButton.SetActive(false);
        if (speedText != null) speedText.SetActive(false);
        if (speedometerUI != null) speedometerUI.SetActive(false);

        Time.timeScale = 0f;
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}