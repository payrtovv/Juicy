using UnityEngine;

public class CoinCanvasManager : MonoBehaviour
{
    public static CoinCanvasManager Instancia { get; private set; }

    [Header("Referencias")]
    [Tooltip("Arrastra aquí el Prefab del texto flotante.")]
    [SerializeField] private GameObject floatingTextPrefab;

    [Tooltip("Área (RectTransform) donde pueden aparecer los textos. Si se deja vacío, se usará todo el Canvas.")]
    [SerializeField] private RectTransform spawnArea;

    void Awake()
    {
        // Configurar Singleton simple
        if (Instancia == null)
        {
            Instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (spawnArea == null)
        {
            spawnArea = GetComponent<RectTransform>();
        }
    }

    public void SpawnCoinText(int amount)
    {
        if (floatingTextPrefab == null) return;

        // 1. Instanciar el prefab como hijo del Canvas/Área de spawn
        GameObject newTextGo = Instantiate(floatingTextPrefab, spawnArea);

        // 2. Calcular una posición aleatoria dentro del RectTransform
        Vector2 randomPosition = GetRandomPositionInRect(spawnArea);

        // 3. Asignar la posición local en la UI
        RectTransform textRect = newTextGo.GetComponent<RectTransform>();
        if (textRect != null)
        {
            textRect.anchoredPosition = randomPosition;
        }

        // 4. Inicializar los valores del texto (monedas)
        FloatingText floatingTextScript = newTextGo.GetComponent<FloatingText>();
        if (floatingTextScript != null)
        {
            floatingTextScript.Setup(amount);
        }
    }

    private Vector2 GetRandomPositionInRect(RectTransform rectTransform)
    {
        // Obtiene las esquinas locales del RectTransform para saber su tamaño real
        Vector3[] corners = new Vector3[4];
        rectTransform.GetLocalCorners(corners);

        // corners[0] es la esquina inferior izquierda, corners[2] es la superior derecha
        float minX = corners[0].x + 50f; // Margen de seguridad para que no quede cortado en los bordes
        float maxX = corners[2].x - 50f;
        float minY = corners[0].y + 50f;
        float maxY = corners[2].y - 50f;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        return new Vector2(randomX, randomY);
    }
}