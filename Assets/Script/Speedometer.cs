using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
    public Rigidbody carRb;
    public TMP_Text speedText;

    void Update()
    {
        float speed = carRb.linearVelocity.magnitude * 3.6f;

        speedText.text = Mathf.Round(speed) + " KM/H";
    }
}