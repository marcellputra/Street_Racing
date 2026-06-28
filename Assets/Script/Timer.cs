using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    public float timeLeft = 20f;

    public bool timerStarted = false;
    public bool timerStopped = false;

    void Update()
    {
        if (!timerStarted)
        {
            if (Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.UpArrow))
            {
                timerStarted = true;
            }
        }

        if (timerStarted && !timerStopped)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft < 0)
                timeLeft = 0;

            int minutes = Mathf.FloorToInt(timeLeft / 60);
            int seconds = Mathf.FloorToInt(timeLeft % 60);

            timerText.text =
                string.Format("{0:00}:{1:00}",
                            minutes,
                            seconds);

            if (timeLeft <= 0)
            {
                timerStopped = true;

                SceneManager.LoadScene(
                    SceneManager.GetActiveScene().name
                );
            }
        }
    }

    public string GetFinalTime()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}