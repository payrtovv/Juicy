using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    // Cambiamos a public o nos aseguramos de que se asigne en código si se olvida en el inspector
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float duration = 1.0f;

    private float timer;
    private Vector3 initialScale;

    private void Awake()
    {
        // Por si acaso te olvidaste de arrastrarlo en el Inspector, lo busca en sí mismo
        if (textMesh == null)
        {
            textMesh = GetComponent<TextMeshProUGUI>();
        }
    }

    public void Setup(int amount)
    {
        if (textMesh != null)
        {
            textMesh.text = "+" + amount.ToString();

            // FUERZA a TextMeshPro a actualizar la malla de texto en este preciso instante
            textMesh.ForceMeshUpdate();
        }
        else
        {
            Debug.LogError("¡No se encontró el componente TextMeshProUGUI en el Prefab!", gameObject);
        }

        initialScale = transform.localScale;
        timer = duration;
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        timer -= Time.deltaTime;
        float progress = timer / duration;
        transform.localScale = initialScale * progress;

        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}