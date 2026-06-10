using UnityEngine;

public class ControladorPico : MonoBehaviour
{
    // Instancia estática para que cualquier script pueda acceder a él fácilmente
    public static ControladorPico Instancia { get; private set; }

    [Header("Atributos del Pico")]
    public int nivelActual = 1;
    public int fuerzaMinado = 1;

    [Header("Economía")]
    public int monedasActuales = 0;
    public int costoBaseMejora = 20;

    public int CostoSiguienteMejora => nivelActual * costoBaseMejora;

    private void Awake()
    {
        // LOGICA DE PERSISTENCIA (Singleton)
        // Si ya existe un ControladorPico en la nueva escena, destruye el duplicado
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        // Si es el primero, se convierte en la instancia oficial y no se destruye al cambiar de escena
        Instancia = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool IntentarMejorarPico()
    {
        int costo = CostoSiguienteMejora;

        if (monedasActuales >= costo)
        {
            monedasActuales -= costo;
            nivelActual++;
            fuerzaMinado = nivelActual;

            Debug.Log($"¡Pico mejorado! Nivel: {nivelActual} | Nueva Fuerza: {fuerzaMinado}");
            return true;
        }

        return false;
    }

    [ContextMenu("Sumar 100 Monedas")]
    public void SumarMonedasPrueba()
    {
        monedasActuales += 100;
    }
}