using UnityEngine;

public class GeneradorMina3D : MonoBehaviour
{
    [Header("Prefabs de los Bloques")]
    [SerializeField] private GameObject prefabPiedra;
    [SerializeField] private GameObject prefabCarbon;

    [Header("Dimensiones de la Mina")]
    [SerializeField] private int anchoX = 10;
    [SerializeField] private int altoY = 5;
    [SerializeField] private int profundidadZ = 10;

    [Header("Probabilidades (0 a 100)")]
    [Range(0, 100)]
    [SerializeField] private int probabilidadCarbon = 15; // 15% de probabilidad de carbón

    void Start()
    {
        GenerarMapa();
    }

    void GenerarMapa()
    {
        // Guardamos la posición inicial del generador como punto de partida
        Vector3 posicionInicial = transform.position;

        // Bucle 1: Controla la altura (Eje Y)
        for (int y = 0; y < altoY; y++)
        {
            // Bucle 2: Controla el ancho (Eje X)
            for (int x = 0; x < anchoX; x++)
            {
                // Bucle 3: Controla la profundidad (Eje Z)
                for (int z = 0; z < profundidadZ; z++)
                {
                    // 1. Calcular la posición exacta de este bloque matemático
                    Vector3 posicionBloque = new Vector3(
                        posicionInicial.x + x,
                        posicionInicial.y + y,
                        posicionInicial.z + z
                    );

                    // 2. Decidir qué tipo de bloque aparecerá usando probabilidad
                    GameObject prefabAEstructurar = SeleccionarBloqueAleatorio();

                    // 3. Crear el bloque físicamente en el mundo de Unity
                    // Quaternion.identity significa que nace sin ninguna rotación (recto)
                    GameObject nuevoBloque = Instantiate(prefabAEstructurar, posicionBloque, Quaternion.identity);

                    // Opcional: Meter los bloques dentro del generador para no saturar la jerarquía
                    nuevoBloque.transform.parent = this.transform;
                }
            }
        }

        Debug.Log($"¡Mina generada con éxito! Total de bloques: {anchoX * altoY * profundidadZ}");
    }

    private GameObject SeleccionarBloqueAleatorio()
    {
        // Tiramos un dado entre 1 y 100
        int dado = Random.Range(1, 101);

        // Si el dado es menor o igual a 15, ponemos Carbón
        if (dado <= probabilidadCarbon)
        {
            return prefabCarbon;
        }

        // De lo contrario, ponemos Piedra por defecto
        return prefabPiedra;
    }
}