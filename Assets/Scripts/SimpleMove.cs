using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    public float speed = 5f;
    public float crouchSpeed = 2.5f;

    public float mouseSensitivity = 200f;
    public Transform cameraTransform;

    public float gravity = -20f;

    private float xRotation = 0f;
    private CharacterController controller;
    private float yVelocity;

    // 🔥 CROUCH
    public float standHeight = 1.8f;
    public float crouchHeight = 1.0f;

    public float cameraStandY = 1.6f;
    public float cameraCrouchY = 1.0f;

    public float crouchSmooth = 8f;

    private float currentCameraY;
    private bool isCrouching;

    //Step Sounds
    public AudioClip footstepSound;
    public float stepInterval = 0.5f;

    private float stepTimer = 0f;
    private AudioSource audioSource;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        currentCameraY = cameraTransform.localPosition.y;
        cameraStandY = currentCameraY;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (GameManager.instance.isGameEnded) return;
        
        HandleMouse();
        HandleCrouch();
        HandleMovement();
    }

    // 🔥 МЫШКА
    void HandleMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    // 🔥 CROUCH
    void HandleCrouch()
    {
        isCrouching = Input.GetKey(KeyCode.LeftControl);

        float targetHeight = isCrouching ? crouchHeight : standHeight;
        controller.height = Mathf.Lerp(controller.height, targetHeight, Time.deltaTime * crouchSmooth);


        float targetCamY = isCrouching ? cameraCrouchY : cameraStandY;
        currentCameraY = Mathf.Lerp(currentCameraY, targetCamY, Time.deltaTime * crouchSmooth);

        Vector3 camPos = cameraTransform.localPosition;
        camPos.y = currentCameraY;
        cameraTransform.localPosition = camPos;
    }

    // 🔥 ДВИЖЕНИЕ
    void HandleMovement()
    {
        float currentSpeed = isCrouching ? crouchSpeed : speed;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (controller.isGrounded && yVelocity < 0)
        {
            yVelocity = -2f;
        }

        yVelocity += gravity * Time.deltaTime;

        Vector3 velocity = move * currentSpeed;
        velocity.y = yVelocity;

        controller.Move(velocity * Time.deltaTime);

        // 🔊 шаги
        if (controller.isGrounded && move.magnitude > 0.1f)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = footstepSound;
                    audioSource.Play();

                }

                float interval = isCrouching ? stepInterval * 1.5f : stepInterval;
                stepTimer = interval;
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            stepTimer = 0f;
        }
    }
}