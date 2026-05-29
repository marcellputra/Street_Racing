using UnityEngine;
using TMPro;
using System.Collections;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public GameObject car;

    void Start()
    {
        car.GetComponent<mobil>().enabled = false;
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        countdownText.text = "5";
        yield return new WaitForSeconds(1);

        countdownText.text = "4";
        yield return new WaitForSeconds(1);

        countdownText.text = "3";
        yield return new WaitForSeconds(1);

        countdownText.text = "2";
        yield return new WaitForSeconds(1);

        countdownText.text = "1";
        yield return new WaitForSeconds(1);

        countdownText.text = "GO!";
        car.GetComponent<mobil>().enabled = true;

        yield return new WaitForSeconds(1);

        countdownText.gameObject.SetActive(false);
    }
}