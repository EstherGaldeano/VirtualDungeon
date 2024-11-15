using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("CambiarEscena", 3.0f);   
    }

    public void CambiarEscena()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
