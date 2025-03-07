using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class CerezasController : MonoBehaviour
{
    public int cantidadCerezas = 0; // Contador de cerezas
    public TMP_Text textoCerezas; // Referencia al UI Text

    private void Start()
    {
        ActualizarTexto();
    }

    public void AgregarCereza(int cantidad)
    {
        cantidadCerezas += cantidad;
        PlayerPrefs.SetInt("cerezas", cantidadCerezas); // Guardar
        PlayerPrefs.Save();
        ActualizarTexto();
    }

    private void ActualizarTexto()
    {
        if (textoCerezas != null)
            textoCerezas.text = cantidadCerezas.ToString(); // Muestra solo el número
    }
}
