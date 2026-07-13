using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishLevel : MonoBehaviour
{
    public GameObject finishPanel;
    public TextMeshProUGUI timeText;
    public Timer timer;

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

    private bool finished = false;

    private void OnTriggerEnter(Collider other)
    {
        if (finished) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("Finish Tersentuh");
            finished = true;

            if (timer != null)
            {
                timer.timerStopped = true;
                string hasil = timer.GetFinalTime();
                timeText.text = "Sisa Waktu : " + hasil;
            }
            else
            {
                Debug.Log("TIMER KOSONG");
            }

            // MATIKAN SUARA MESIN
            if (engineSound != null)
                engineSound.StopEngine();

            // Tampilkan panel finish
            if (finishPanel != null)
                finishPanel.SetActive(true);

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
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("track3");
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("track2");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}