using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause UI")]
    public GameObject pauseMenu;
    public GameObject pauseButton;

    [Header("Pause Setting Panel")]
    public GameObject pauseSettingPanel;

    [Header("Control UI")]
    public GameObject fixedJoystick;
    public GameObject gasButton;
    public GameObject remButton;

    [Header("HUD UI")]
    public GameObject timerText;
    public GameObject speedText;
    public GameObject speedometerUI;

    [Header("Sound")]
    public EngineSound engineSound;

    [Header("Mobil")]
    public mobil carController;

    private bool isPaused = false;

    void Start()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(false);

        if (pauseSettingPanel != null)
            pauseSettingPanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(false);

        if (pauseSettingPanel != null)
            pauseSettingPanel.SetActive(false);

        // tampilkan UI gameplay
        if (pauseButton != null) pauseButton.SetActive(true);
        if (fixedJoystick != null) fixedJoystick.SetActive(true);
        if (gasButton != null) gasButton.SetActive(true);
        if (remButton != null) remButton.SetActive(true);
        if (timerText != null) timerText.SetActive(true);
        if (speedText != null) speedText.SetActive(true);
        if (speedometerUI != null) speedometerUI.SetActive(true);

        Time.timeScale = 1f;
        isPaused = false;

        // lanjutkan suara engine
        if (engineSound != null)
            engineSound.ResumeEngine();
    }

    public void Pause()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(true);

        if (pauseSettingPanel != null)
            pauseSettingPanel.SetActive(false);

        // sembunyikan UI gameplay
        if (pauseButton != null) pauseButton.SetActive(false);
        if (fixedJoystick != null) fixedJoystick.SetActive(false);
        if (gasButton != null) gasButton.SetActive(false);
        if (remButton != null) remButton.SetActive(false);
        if (timerText != null) timerText.SetActive(false);
        if (speedText != null) speedText.SetActive(false);
        if (speedometerUI != null) speedometerUI.SetActive(false);

        // pause engine
        if (engineSound != null)
            engineSound.PauseEngine();

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void OpenPauseSetting()
    {
        if (pauseSettingPanel != null)
            pauseSettingPanel.SetActive(true);

        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }

    public void ClosePauseSetting()
    {
        if (pauseSettingPanel != null)
            pauseSettingPanel.SetActive(false);

        if (pauseMenu != null)
            pauseMenu.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }
}