using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPos;

    void OnEnable()
    {
        originalPos = transform.localPosition;
    }

    public void Shake(float amount, float speed)
    {
        // Calculamos una posición aleatoria pequeña
        float x = Random.Range(-1f, 1f) * amount;
        float y = Random.Range(-1f, 1f) * amount;

        // Aplicamos la vibración a la posición original
        transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
    }

    public void ResetCamera()
    {
        transform.localPosition = originalPos;
    }
}