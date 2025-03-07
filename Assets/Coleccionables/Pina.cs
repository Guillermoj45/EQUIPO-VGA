using UnityEngine;

public class Pina : MonoBehaviour
{
    public int valor = 1; // Cuántas cerezas suma esta cereza

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) // Detecta por Layer
        {
            PinaController manager = Object.FindFirstObjectByType<PinaController>();
            if (manager != null)
            {
                manager.AgregarPina(valor);
            }
            Destroy(gameObject); // Destruir la cereza tras recogerla
        }
    }
}

