using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeOverManager : MonoBehaviour
{
    [Header("Panel")]
    public GameObject timeOverPanel;

    [Header("UI Gameplay")]
    public GameObject joystick;
    public GameObject gasButton;
    public GameObject brakeButton;
    public GameObject timerUI;
    public GameObject speedUI;
    public GameObject pauseButton;
    public GameObject speedometerUI;

    [Header("Sound")]
    public EngineSound engineSound;

    private bool isGameOver = false;

    void Start()
    {
        if (timeOverPanel != null)
            timeOverPanel.SetActive(false);
    }

    public void ShowTimeOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        // Matikan suara mesin
        if (engineSound != null)
            engineSound.StopEngine();

        // Tampilkan panel time over
        if (timeOverPanel != null)
            timeOverPanel.SetActive(true);

        // Sembunyikan UI gameplay
        if (joystick != null) joystick.SetActive(false);
        if (gasButton != null) gasButton.SetActive(false);
        if (brakeButton != null) brakeButton.SetActive(false);
        if (timerUI != null) timerUI.SetActive(false);
        if (speedUI != null) speedUI.SetActive(false);
        if (pauseButton != null) pauseButton.SetActive(false);
        if (speedometerUI != null) speedometerUI.SetActive(false);

        // Pause game
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