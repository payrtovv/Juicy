using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Configuración del Raycast")]
    [Tooltip("Distancia máxima a la que el jugador puede interactuar con los bloques (estilo Minecraft).")]
    [SerializeField] private float interactionDistance = 4.5f;

    [Tooltip("Capa (Layer) en la que están los bloques interactuables para optimizar el Raycast.")]
    [SerializeField] private LayerMask interactableLayer;

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
        // El rayo sale desde el centro exacto de la pantalla (donde estará la mira) hacia adelante
        Ray ray = new Ray(transform.position, transform.forward);
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