using UnityEngine;
using UnityEngine.UI;

public class NeedleSpeedometer : MonoBehaviour
{
    public RectTransform needle;   // drag NeedleUi ke sini
    public Rigidbody carRb;        // drag Rigidbody mobil ke sini

    public float maxSpeed = 120f;      // angka maksimum di speedometer
    public float minNeedleAngle = 90f;   // posisi jarum saat 0 km/h
    public float maxNeedleAngle = -90f;  // posisi jarum saat max speed

    void Update()
    {
        if (needle == null || carRb == null) return;

        // kecepatan mobil dalam km/h
        float speed = carRb.linearVelocity.magnitude * 3.6f;

        // batasi supaya tidak lebih dari maxSpeed
        speed = Mathf.Clamp(speed, 0f, maxSpeed);

        // ubah speed menjadi sudut jarum
        float angle = Mathf.Lerp(minNeedleAngle, maxNeedleAngle, speed / maxSpeed);

        // putar jarum
        needle.localEulerAngles = new Vector3(0, 0, angle);
    }
}