using UnityEngine;

public class mobil : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentBrakeForce;

    // Android Button
    private bool gasPressed = false;
    private bool brakePressed = false;

    // Rigidbody
    private Rigidbody rb;

    // Joystick
    public FixedJoystick joystick;

    // Timer
    public Timer timer;

    // Engine Sound
    public EngineSound engineSound;

    // Settings
    [SerializeField] private float motorForce = 1500f;
    [SerializeField] private float brakeForce = 3000f;
    [SerializeField] private float maxSteerAngle = 30f;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    // Wheel Mesh
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        float keyboardH = Input.GetAxis("Horizontal");
        horizontalInput = keyboardH;

        verticalInput = 0f;

        bool acceleratingNow = false;

        // =========================
        // Keyboard W = Maju
        // =========================
        if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1f;
            acceleratingNow = true;

            if (timer != null)
                timer.StartTimer();

            if (engineSound != null)
                engineSound.StartEngine();
        }

        // =========================
        // Keyboard S = Mundur
        // =========================
        if (Input.GetKey(KeyCode.S))
        {
            verticalInput = -1f;
        }

        // =========================
        // Joystick Steering
        // =========================
        if (joystick != null)
        {
            horizontalInput += joystick.Horizontal;
        }

        horizontalInput = Mathf.Clamp(horizontalInput, -1f, 1f);

        // =========================
        // Android Gas
        // =========================
        if (gasPressed)
        {
            verticalInput = 1f;
            acceleratingNow = true;

            if (timer != null)
                timer.StartTimer();

            if (engineSound != null)
                engineSound.StartEngine();
        }

        // kasih tahu engine apakah sedang digas atau tidak
        if (engineSound != null)
            engineSound.SetAccelerating(acceleratingNow);
    }

    private void HandleMotor()
    {
        // ==========================
        // Android REM
        // ==========================
        if (brakePressed)
        {
            float forwardSpeed = transform.InverseTransformDirection(rb.linearVelocity).z;

            // Kalau mobil masih maju -> rem
            if (forwardSpeed > 0.2f)
            {
                rearLeftWheelCollider.motorTorque = 0f;
                rearRightWheelCollider.motorTorque = 0f;
                currentBrakeForce = brakeForce;
            }
            else
            {
                // Kalau hampir berhenti -> mundur
                currentBrakeForce = 0f;
                rearLeftWheelCollider.motorTorque = -motorForce;
                rearRightWheelCollider.motorTorque = -motorForce;
            }
        }
        // ==========================
        // Keyboard Space = REM
        // ==========================
        else if (Input.GetKey(KeyCode.Space))
        {
            rearLeftWheelCollider.motorTorque = 0f;
            rearRightWheelCollider.motorTorque = 0f;
            currentBrakeForce = brakeForce;
        }
        // ==========================
        // Jalan Normal
        // ==========================
        else
        {
            currentBrakeForce = 0f;

            rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
            rearRightWheelCollider.motorTorque = verticalInput * motorForce;
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
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform, true);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform, false);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform, true);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform, false);
    }

    private void UpdateSingleWheel(
        WheelCollider wheelCollider,
        Transform wheelTransform,
        bool isLeftWheel)
    {
        Vector3 pos;
        Quaternion rot;

        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;

        if (isLeftWheel)
            wheelTransform.rotation = rot * Quaternion.Euler(0, 180, 0);
        else
            wheelTransform.rotation = rot;
    }

    // =============================
    // Android Button
    // =============================
    public void GasDown()
    {
        gasPressed = true;

        if (timer != null)
            timer.StartTimer();

        if (engineSound != null)
        {
            engineSound.StartEngine();
            engineSound.SetAccelerating(true);
        }
    }

    public void GasUp()
    {
        gasPressed = false;

        if (engineSound != null)
            engineSound.SetAccelerating(false);
    }

    public void BrakeDown()
    {
        brakePressed = true;

        if (engineSound != null)
            engineSound.SetAccelerating(false);
    }

    public void BrakeUp()
    {
        brakePressed = false;
    }

    // dipakai PauseMenu
    public bool IsAccelerating()
    {
        if (gasPressed) return true;
        if (Input.GetKey(KeyCode.W)) return true;
        return false;
    }
}