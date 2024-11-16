using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private GameObject cameras;

    [SerializeField]
    private GameObject iluminacionLobby;
    [SerializeField]
    private GameObject iluminacionTabern;
    [SerializeField]
    private GameObject iluminacionBedroom;
    [SerializeField]
    private GameObject iluminacionDungeon;

    [SerializeField]
    private GameObject sliderIntensidadLuz;

    private int i;


    public void CambiarIntensidad()
    {
        for (i = 0; i < iluminacionLobby.gameObject.transform.childCount; i++)
        {
            iluminacionLobby.gameObject.transform.GetChild(i).gameObject.GetComponent<Light>().intensity = sliderIntensidadLuz.gameObject.GetComponent<Slider>().value;
        }
        for (i = 0; i < iluminacionTabern.gameObject.transform.childCount; i++)
        {
            iluminacionTabern.gameObject.transform.GetChild(i).gameObject.GetComponent<Light>().intensity = sliderIntensidadLuz.gameObject.GetComponent<Slider>().value;
        }
        for (i = 0; i < iluminacionBedroom.gameObject.transform.childCount; i++)
        {
            iluminacionBedroom.gameObject.transform.GetChild(i).gameObject.GetComponent<Light>().intensity = sliderIntensidadLuz.gameObject.GetComponent<Slider>().value;
        }
        for (i = 0; i < iluminacionDungeon.gameObject.transform.childCount; i++)
        {
            iluminacionDungeon.gameObject.transform.GetChild(i).gameObject.GetComponent<Light>().intensity = sliderIntensidadLuz.gameObject.GetComponent<Slider>().value;
        }

    }

    public void ChangeCameras(int camPosition){

    for (int i = 0; i< cameras.gameObject.transform.childCount; i++ ){
        
        cameras.gameObject.transform.GetChild(i).gameObject.SetActive(false);
    }
    
    cameras.gameObject.transform.GetChild(camPosition).gameObject.SetActive(true);
    
    }

    public void ApagarLuces()
    {
        for (i=0; i< iluminacionLobby.gameObject.transform.childCount; i++)
        {
            iluminacionLobby.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (i = 0; i < iluminacionTabern.gameObject.transform.childCount; i++)
        {
            iluminacionTabern.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (i = 0; i < iluminacionBedroom.gameObject.transform.childCount; i++)
        {
            iluminacionBedroom.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (i = 0; i < iluminacionDungeon.gameObject.transform.childCount; i++)
        {
            iluminacionDungeon.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void EncenderLuces()
    {
        for (i = 0; i < iluminacionLobby.gameObject.transform.childCount; i++)
        {
            iluminacionLobby.gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
        for (i = 0; i < iluminacionTabern.gameObject.transform.childCount; i++)
        {
            iluminacionTabern.gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
        for (i = 0; i < iluminacionBedroom.gameObject.transform.childCount; i++)
        {
            iluminacionBedroom.gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
        for (i = 0; i < iluminacionDungeon.gameObject.transform.childCount; i++)
        {
            iluminacionDungeon.gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (canvas.gameObject.activeSelf){
                canvas.gameObject.SetActive(false);
            }else {
                canvas.gameObject.SetActive(true);
            }
       } 
    }
}
