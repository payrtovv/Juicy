using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InterfazPico : MonoBehaviour
{
    [Header("Referencias del Sistema")]
    [SerializeField] private ControladorPico controladorPico;

    [Header("Componentes de UI")]
    [SerializeField] private TextMeshProUGUI textoNivelFuerza;
    [SerializeField] private TextMeshProUGUI textoCosto;
    [SerializeField] private TextMeshProUGUI textoMonedas;
    [SerializeField] private Button botonMejorar;

    private void Start()
    {
        controladorPico = ControladorPico.Instancia;

        // Vinculamos el botón por código para evitar arrastrarlo en el inspector
        if (botonMejorar != null)
        {
            botonMejorar.onClick.AddListener(ClickEnMejorar);
        }

        ActualizarInterfaz();
    }

    // Este método se ejecuta cada vez que el jugador presiona el botón
    private void ClickEnMejorar()
    {
        // Intentamos mejorar y, si tiene éxito, actualizamos los textos
        if (controladorPico.IntentarMejorarPico())
        {
            ActualizarInterfaz();
        }
    }

    // Centralizamos la actualización de la UI para mantenerla limpia
    public void ActualizarInterfaz()
    {
        if (controladorPico == null) return;

        textoNivelFuerza.text = $"Pico Nivel {controladorPico.nivelActual} (Fuerza: {controladorPico.fuerzaMinado})";
        textoCosto.text = $"Mejorar: ${controladorPico.CostoSiguienteMejora}";
        textoMonedas.text = $"Monedas: ${controladorPico.monedasActuales}";
    }

    // Opcional: Si ganas monedas desde otro script, llamas a esto para refrescar la pantalla
    private void OnEnable()
    {
        InvokeRepeating(nameof(ActualizarInterfaz), 0.5f, 0.5f); // Refresca las monedas continuamente
    }
}