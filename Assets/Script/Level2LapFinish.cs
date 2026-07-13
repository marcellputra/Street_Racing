using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Level2LapFinish : MonoBehaviour
{
    [Header("Checkpoint")]
    public bool checkpointPassed = false;

    [Header("Finish UI")]
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

    public void SetCheckpointPassed()
    {
        checkpointPassed = true;
        Debug.Log("Checkpoint Level 2 sudah dilewati");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (finished) return;
        if (!other.CompareTag("Player")) return;

        // Hanya boleh finish kalau checkpoint SUDAH dilewati
        if (!checkpointPassed)
        {
            Debug.Log("Belum lewat checkpoint, belum boleh finish");
            return;
        }

        Debug.Log("Finish Level 2");

        finished = true;

        if (timer != null)
        {
            timer.timerStopped = true;
            string hasil = timer.GetFinalTime();
            timeText.text = "Sisa Waktu : " + hasil;
        }

        // Matikan suara mesin
        if (engineSound != null)
            engineSound.StopEngine();

        // Tampilkan finish panel
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

        Time.timeScale = 0f;
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("track4"); // kalau nanti ada level 3
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("track3"); // ulang level 2
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