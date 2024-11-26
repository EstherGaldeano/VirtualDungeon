using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    

    [SerializeField]
    private GameObject ButtonFPS;

    [SerializeField]
    private GameObject ButtonSalir;

    [SerializeField]
    private GameObject ButtonCredits;

    [SerializeField]
    private GameObject Credits;

    [SerializeField]
    private GameObject Atras;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Credits.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Creditos()
    {
        this.ButtonFPS.SetActive(false);
        this.ButtonSalir.SetActive(false);
        this.ButtonCredits.SetActive(false);
        this.Credits.SetActive(true);
        this.Atras.SetActive(true);
    }

    public void BackMP()
    {
       
        this.ButtonFPS.SetActive(true);
        this.ButtonSalir.SetActive(true);
        this.ButtonCredits.SetActive(true);
        this.Credits.SetActive(false);
        this.Atras.SetActive(false);
    }

    public void CambiarEscena(string nombreEscenaDestino)
    {
        SceneManager.LoadScene(nombreEscenaDestino);
    }

    public void Salir()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }

    }

}
