using UnityEngine;

public class mobil : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentBrakeForce;
    private bool isBraking;

    // Joystick Android
    public FixedJoystick joystick;

    // Settings
    [SerializeField] private float motorForce = 1500f;
    [SerializeField] private float brakeForce = 3000f;
    [SerializeField] private float maxSteerAngle = 30f;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    // Wheel Transforms
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        // Input Keyboard
        float keyboardH = Input.GetAxis("Horizontal");
        float keyboardV = Input.GetAxis("Vertical");

        // Input Joystick
        float joystickH = 0f;
        float joystickV = 0f;

        if (joystick != null)
        {
            joystickH = joystick.Horizontal;
            joystickV = joystick.Vertical;
        }

        // Gabungkan Keyboard + Joystick
        horizontalInput = Mathf.Clamp(
            keyboardH + joystickH,
            -1f,
            1f
        );

        verticalInput = Mathf.Clamp(
            keyboardV + joystickV,
            -1f,
            1f
        );

        // Rem (untuk PC)
        isBraking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        if (isBraking)
        {
            rearLeftWheelCollider.motorTorque = 0f;
            rearRightWheelCollider.motorTorque = 0f;

            currentBrakeForce = brakeForce;
        }
        else
        {
            rearLeftWheelCollider.motorTorque =
                verticalInput * motorForce;

            rearRightWheelCollider.motorTorque =
                verticalInput * motorForce;

            currentBrakeForce = 0f;
        }

        ApplyBraking();
    }

    private void ApplyBraking()
    {
        frontLeftWheelCollider.brakeTorque = currentBrakeForce;
        frontRightWheelCollider.brakeTorque = currentBrakeForce;
        rearLeftWheelCollider.brakeTorque = currentBrakeForce;
        rearRightWheelCollider.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;

        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(
            frontLeftWheelCollider,
            frontLeftWheelTransform,
            true
        );

        UpdateSingleWheel(
            frontRightWheelCollider,
            frontRightWheelTransform,
            false
        );

        UpdateSingleWheel(
            rearLeftWheelCollider,
            rearLeftWheelTransform,
            true
        );

        UpdateSingleWheel(
            rearRightWheelCollider,
            rearRightWheelTransform,
            false
        );
    }

    private void UpdateSingleWheel(
        WheelCollider wheelCollider,
        Transform wheelTransform,
        bool isLeftWheel
    )
    {
        Vector3 pos;
        Quaternion rot;

        wheelCollider.GetWorldPose(out pos, out rot);

        wheelTransform.position = pos;

        if (isLeftWheel)
        {
            wheelTransform.rotation =
                rot * Quaternion.Euler(0, 180, 0);
        }
        else
        {
            wheelTransform.rotation = rot;
        }
    }
}