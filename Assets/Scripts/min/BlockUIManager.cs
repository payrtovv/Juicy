using UnityEngine;
using TMPro; // Necesario para TextMeshPro
using UnityEngine.UI; // Por si quieres usar un Slider/Barra de vida en el futuro

public class BlockUIManager : MonoBehaviour
{
    public static BlockUIManager Instancia;

    [Header("Referencias UI")]
    [Tooltip("El panel o contenedor de la vida para ocultarlo cuando no hay combates/minería.")]
    [SerializeField] private GameObject panelVidaHUD;
    [SerializeField] private TextMeshProUGUI textoVida;

    private void Awake()
    {
        // Configuración del Singleton
        if (Instancia == null)
        {
            Instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Ocultamos la UI al inicio del juego
        OcultarVida();
    }

    /// <summary>
    /// Actualiza y muestra la UI con los datos del bloque golpeado.
    /// </summary>
    public void MostrarVida(string nombreBloque, int vidaActual, int vidaMax)
    {
        if (panelVidaHUD != null) panelVidaHUD.SetActive(true);

        // Prevenimos que se muestren números negativos en pantalla
        int vidaMostrar = Mathf.Max(0, vidaActual);

        if (textoVida != null)
        {
            textoVida.text = $"{nombreBloque}: {vidaMostrar} / {vidaMax}";
        }
    }

    /// <summary>
    /// Oculta el panel de vida.
    /// </summary>
    public void OcultarVida()
    {
        if (panelVidaHUD != null) panelVidaHUD.SetActive(false);
    }
}