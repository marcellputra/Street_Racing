using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    float elapsedTime;
    bool timerStarted = false;
    bool timerStopped = false;

    void Update()
    {
        // Timer mulai saat tekan W atau Panah Atas
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
        }
    }

    // Saat menyentuh garis finish
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            timerStopped = true;
            timerText.text += " FINISH!";
        }
    }
}