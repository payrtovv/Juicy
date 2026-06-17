using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("Arrastra aquí la cámara del jugador (Player Camera).")]
    [SerializeField] private Camera playerCamera;

    [Header("Configuración del Raycast")]
    [Tooltip("Distancia máxima a la que el jugador puede interactuar con los bloques (estilo Minecraft).")]
    [SerializeField] private float interactionDistance = 4.5f;

    [Tooltip("Capa (Layer) en la que están los bloques interactuables para optimizar el Raycast.")]
    [SerializeField] private LayerMask interactableLayer;

    void Start()
    {
        // Si no asignaste la cámara en el inspector, intenta buscar la cámara principal automáticamente
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    void Update()
    {
        // Detectar el clic izquierdo del ratón
        if (Input.GetMouseButtonDown(0))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        if (playerCamera == null) return;

        // CORREGIDO: El rayo ahora sale desde la posición de la cámara y hacia donde mira la cámara
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        // Lanzamos el Raycast
        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            // Intentamos obtener el script InteractableBlock del objeto golpeado
            InteractableBlock targetBlock = hit.collider.GetComponent<InteractableBlock>();

            if (targetBlock != null)
            {
                // Si el objeto tiene el script, le mandamos la orden de recibir un golpe
                targetBlock.ReceiveHit();
            }
        }
    }
}