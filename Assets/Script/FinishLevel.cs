using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishLevel : MonoBehaviour
{
    public GameObject finishPanel;
    public TextMeshProUGUI timeText;
    public Timer timer;

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

                Debug.Log(hasil);

                timeText.text =
                    "Sisa Waktu : " + hasil;
            }
            else
            {
                Debug.Log("TIMER KOSONG");
            }

            finishPanel.SetActive(true);

            Time.timeScale = 0f;
        }
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("track3");
    }

    public void Restart()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
}