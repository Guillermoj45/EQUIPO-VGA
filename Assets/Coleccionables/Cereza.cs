using UnityEngine;

public class Cereza : MonoBehaviour
{
    public int valor = 1; // Cuántas cerezas suma esta cereza

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) // Detecta por Layer
        {
            CerezasController manager = Object.FindFirstObjectByType<CerezasController>();
            if (manager != null)
            {
                manager.AgregarCereza(valor);
            }
            Destroy(gameObject); // Destruir la cereza tras recogerla
        }
    }
}
