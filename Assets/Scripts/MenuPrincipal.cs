using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField]
    private GameObject ButtonFixedCam;

    [SerializeField]
    private GameObject ButtonFPSCam;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Creditos()
    {
        this.ButtonFixedCam.SetActive(false);
        this.ButtonFPSCam.SetActive(false);
        this.ButtonSalir.SetActive(false);
        this.ButtonCredits.SetActive(false);
        this.Credits.SetActive(true);
        this.Atras.SetActive(true);
    }

    public void BackMP()
    {
        this.ButtonFixedCam.SetActive(true);
        this.ButtonFPSCam.SetActive(true);
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
