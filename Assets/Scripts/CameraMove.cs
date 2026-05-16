using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -5);
    public float sensitivity = 3f;

    // --- NUEVAS VARIABLES PARA VIBRACIÓN ---
    private Vector3 shakeOffset = Vector3.zero;
    public bool isShaking = false;
    public float shakeIntensity = 0.001f;

    float mouseX, mouseY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        // Leer movimiento del mouse
        mouseX += Input.GetAxis("Mouse X") * sensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * sensitivity;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        // Rotar la cámara y el personaje
        transform.eulerAngles = new Vector3(mouseY, mouseX, 0);
        target.rotation = Quaternion.Euler(0, mouseX, 0);

        // --- LÓGICA DE VIBRACIÓN ---
        if (isShaking)
        {
            shakeOffset = new Vector3(
                Random.Range(-0.2f, 0.2f) * shakeIntensity,
                Random.Range(-0.2f, 0.2f) * shakeIntensity,
                0
            );
        }
        else
        {
            shakeOffset = Vector3.zero;
        }

        // Posicionar la cámara + Sumar la vibración
        transform.position = (target.position + transform.rotation * offset) + shakeOffset;
    }
}