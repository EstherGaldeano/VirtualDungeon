using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class FPSKeyFlow : MonoBehaviour
{
    public GameObject door2;

    public TMP_Text text1;
    public TMP_Text killsText;

    public GameObject key1;
    public GameObject key2;

    public GameObject key1Img;
    public GameObject key2Img;

    public Animator text1anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //text1.gameObject.SetActive(false);
        key1.gameObject.SetActive(false);
        key2.gameObject.SetActive(false);
        key1Img.SetActive(false);
        key2Img.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameFlow.updateKills();
        }
        if(GameFlow.kills >= 10 && !GameFlow.key1Obtained)
        {
            key1.gameObject.SetActive(true);
        }
        
        if(GameFlow.kills < 10)
        {
            killsText.text = GameFlow.kills.ToString() + "/10";
        }
        else
        {
            killsText.text = "10/10";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GrateDoor" && !GameFlow.key1Obtained)
        {
            text1.text = "Necesitas la llave 1 para abrir la puerta";
            //text1.gameObject.SetActive(true);
            text1anim.SetBool("Accion", true);
        }
        if (other.gameObject.tag == "Door2" && !GameFlow.key2Obtained)
        {
            text1.text = "Necesitas la llave 2 para abrir la puerta";
            //text1.gameObject.SetActive(true);
            text1anim.SetBool("Accion", true);
        }
        if (other.gameObject.tag == "Key1")
        {
            Destroy(other.gameObject);

            key2.gameObject.SetActive(true);

            GameFlow.key1Obtained = true;
            key1Img.SetActive(true);
        }
        if (other.gameObject.tag == "Key2")
        {
            Destroy(other.gameObject);

            GameFlow.key2Obtained = true;
            key2Img.SetActive(true);
        }
        if (other.gameObject.tag == "GrateDoor" && GameFlow.key1Obtained && !GameFlow.grateDoorOpened)
        {
            text1.text = "Tienes acceso";
            //text1.gameObject.SetActive(true);
            text1anim.SetBool("Accion", true);
            GameFlow.grateDoorOpened = true;
            Invoke("TextDisappear", 3f);

            Animator anim = other.GetComponent<Animator>();
            anim.SetTrigger("Accion");
        }
        if (other.gameObject.tag == "Door2" && GameFlow.key2Obtained && !GameFlow.door2Opened)
        {
            text1.text = "Tienes acceso";
            //text1.gameObject.SetActive(true);
            text1anim.SetBool("Accion", true);
            GameFlow.door2Opened = true;
            Invoke("TextDisappear", 3f);

            Animator anim = other.GetComponent<Animator>();
            anim.SetBool("Accion", true);
        }
        if (other.gameObject.tag == "Door2Close")
        {
            Animator anim = door2.GetComponent<Animator>();

            anim.SetBool("Accion", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "GrateDoor" && !GameFlow.key1Obtained)
        {
            TextDisappear();
        }
        if (other.gameObject.tag == "Door2" && !GameFlow.key2Obtained)
        {
            TextDisappear();
        }
    }

    private void TextDisappear()
    {
        text1anim.SetBool("Accion", false);
    }
}
