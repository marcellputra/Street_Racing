using UnityEngine;
using TMPro;

public class TimerLevel2 : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    // Drag mobil player ke sini
    public mobil carController;

    float elapsedTime;
    bool timerStarted = false;
    bool timerStopped = false;

    void Update()
    {
        // Timer mulai saat tekan W / panah atas
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            timerStarted = true;
        }

        // Timer berjalan
        if (timerStarted && !timerStopped)
        {
            elapsedTime += Time.deltaTime;

            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            // Jika lebih dari 40 detik
            if (elapsedTime >= 40f)
            {
                timerStopped = true;

                timerText.text = "TIME OVER";

                // Matikan mobil
                carController.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            timerStopped = true;
            timerText.text += " FINISH!";
        }
    }
}