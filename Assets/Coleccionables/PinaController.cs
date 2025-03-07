using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class PinaController : MonoBehaviour
{
    public int cantidadPina = 0; // Contador de cerezas
    public TMP_Text textoCerezas; // Referencia al UI Text

    private void Start()
    {
        ActualizarTexto();
    }

    public void AgregarPina(int cantidad)
    {
        cantidadPina += cantidad;
        PlayerPrefs.SetInt("pinas", cantidadPina); // Guardar
        PlayerPrefs.Save();
        ActualizarTexto();
    }

    private void ActualizarTexto()
    {
        if (textoCerezas != null)
            textoCerezas.text = cantidadPina.ToString(); // Muestra solo el número
    }
}
