using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private GameObject cameras;


public void ChangeCameras(int camPosition){

    for (int i = 0; i< cameras.gameObject.transform.childCount; i++ ){
        
        cameras.gameObject.transform.GetChild(i).gameObject.SetActive(false);
    }
    
    cameras.gameObject.transform.GetChild(camPosition).gameObject.SetActive(true);
    
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
