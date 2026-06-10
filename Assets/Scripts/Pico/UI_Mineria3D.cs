using UnityEngine;
using TMPro; // Obligatorio para usar TextMeshPro

public class UI_Mineria3D : MonoBehaviour
{
    [Header("Componentes de Texto (TextMeshPro)")]
    [SerializeField] private TextMeshProUGUI textoPico;
    [SerializeField] private TextMeshProUGUI textoMonedas;

    void Start()
    {
        // Forzamos una primera actualización al arrancar la escena
        ActualizarTextosUI();
    }

    void Update()
    {
        // Monitoreamos los datos del GameManager persistente
        ActualizarTextosUI();
    }

    private void ActualizarTextosUI()
    {
        // Verificamos que el GameManager exista en esta escena antes de leerlo
        if (ControladorPico.Instancia != null)
        {
            if (textoPico != null)
            {
                textoPico.text = $" Pico: Nivel {ControladorPico.Instancia.nivelActual} (Fuerza: {ControladorPico.Instancia.fuerzaMinado})";
            }

            if (textoMonedas != null)
            {
                textoMonedas.text = $" Monedas: {ControladorPico.Instancia.monedasActuales}";
            }
        }
    }
}