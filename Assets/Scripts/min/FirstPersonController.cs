using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1.5f;

    [Header("Cámara y Rotación")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float lookSpeed = 2.0f;
    [SerializeField] private float lookXLimit = 85.0f;

    // Referencias internas
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    [HideInInspector] public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Bloquear y ocultar el cursor en la pantalla
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 1. Lógica de Rotación (Mirar alrededor)
        if (canMove && playerCamera != null)
        {
            // Rotación vertical (Cámara)
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.localRotation = Quaternion.Euler(rotationX, 0, 0);

            // Rotación horizontal (Cuerpo del personaje)
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        // 2. Lógica de Movimiento (Caminar e Ir al frente/lados)
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        // Calcular velocidad en base a los ejes (WASD / Flechas)
        float curSpeedX = canMove ? walkSpeed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? walkSpeed * Input.GetAxis("Horizontal") : 0;

        // CORREGIDO: .y en minúscula
        float movementDirectionY = moveDirection.y;

        // Vector de movimiento final en el plano horizontal
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // 3. Salto y Gravedad
        if (characterController.isGrounded)
        {
            // Resetear la gravedad acumulada si está en el suelo
            moveDirection.y = -0.5f;

            // Input de Salto (Espacio)
            if (canMove && Input.GetButtonDown("Jump"))
            {
                // Fórmula física estándar para calcular la fuerza de salto basada en la altura deseada
                moveDirection.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        else
        {
            // CORREGIDO: .y en minúscula
            moveDirection.y = movementDirectionY + (gravity * Time.deltaTime);
        }

        // 4. Mover el Character Controller
        characterController.Move(moveDirection * Time.deltaTime);
    }
}