using System.Collections.Generic;
using UnityEngine;

public class PiesDetector : MonoBehaviour
{
    // Lista de colliders en contacto con "pies"
    public List<Collider2D> collidingElements = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el elemento no está ya en la lista, lo agregamos
        if (!collidingElements.Contains(collision))
        {
            collidingElements.Add(collision);
            Debug.Log("Entró: " + collision.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Cuando el objeto sale, lo quitamos de la lista
        if (collidingElements.Contains(collision))
        {
            collidingElements.Remove(collision);
            Debug.Log("Salió: " + collision.gameObject.name);
        }
    }
}
