using UnityEngine;

public class MobilePedal : MonoBehaviour
{
    public static bool gasPressed = false;
    public static bool brakePressed = false;

    public void GasDown()
    {
        gasPressed = true;
    }

    public void GasUp()
    {
        gasPressed = false;
    }

    public void BrakeDown()
    {
        brakePressed = true;
    }

    public void BrakeUp()
    {
        brakePressed = false;
    }
}