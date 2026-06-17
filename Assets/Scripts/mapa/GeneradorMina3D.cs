using UnityEngine;

public class GeneradorMina3D : MonoBehaviour
{
    // Creamos una estructura para agrupar el bloque y su probabilidad
    [System.Serializable]
    public struct DatosBloque
    {
        public string nombreDelBloque; // Solo para identificarlo visualmente en el inspector
        public GameObject prefabBloque;
        [Range(0f, 100f)] public float probabilidad; // Porcentaje de aparición (ej: 5, 10, 60...)
    }

    [Header("Configuración de los 5 Bloques")]
    [Tooltip("Agrega aquí tus 5 tipos de bloques configurando sus prefabs y probabilidades.")]
    [SerializeField] private DatosBloque[] listaBloques = new DatosBloque[5];

    [Header("Dimensiones de la Mina")]
    [SerializeField] private int anchoX = 10;
    [SerializeField] private int altoY = 5;
    [SerializeField] private int profundidadZ = 10;

    void Start()
    {
        GenerarMapa();
    }

    void GenerarMapa()
    {
        Vector3 posicionInicial = transform.position;

        for (int y = 0; y < altoY; y++)
        {
            for (int x = 0; x < anchoX; x++)
            {
                for (int z = 0; z < profundidadZ; z++)
                {
                    Vector3 posicionBloque = new Vector3(
                        posicionInicial.x + x,
                        posicionInicial.y + y,
                        posicionInicial.z + z
                    );

                    GameObject prefabAEstructurar = SeleccionarBloqueAleatorio();

                    // Validación por si acaso olvidaste arrastrar algún prefab en la lista
                    if (prefabAEstructurar != null)
                    {
                        GameObject nuevoBloque = Instantiate(prefabAEstructurar, posicionBloque, Quaternion.identity);
                        nuevoBloque.transform.parent = this.transform;
                    }
                }
            }
        }

        Debug.Log($"¡Mina generada con éxito! Total de bloques: {anchoX * altoY * profundidadZ}");
    }

    private GameObject SeleccionarBloqueAleatorio()
    {
        // 1. Calculamos la suma total de todas las probabilidades configuradas
        float sumaTotalProbabilidades = 0f;
        foreach (DatosBloque bloque in listaBloques)
        {
            sumaTotalProbabilidades += bloque.probabilidad;
        }

        // 2. Tiramos un dado flotante entre 0 y el total de la suma
        float dado = Random.Range(0f, sumaTotalProbabilidades);

        // 3. Algoritmo de peso acumulado: buscamos en qué rango cayó el dado
        float acumulado = 0f;
        foreach (DatosBloque bloque in listaBloques)
        {
            acumulado += bloque.probabilidad;
            if (dado <= acumulado)
            {
                return bloque.prefabBloque;
            }
        }

        // Retorno de emergencia por si la lista está vacía
        return null;
    }
}