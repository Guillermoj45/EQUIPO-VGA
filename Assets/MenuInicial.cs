using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Jugar(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Escena actual: " + SceneManager.GetActiveScene().buildIndex);
    }

    public void Salir(){
        Application.Quit();
    }
}
