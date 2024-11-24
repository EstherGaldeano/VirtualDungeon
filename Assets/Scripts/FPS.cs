using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class FPS : MonoBehaviour
{
    [SerializeField]
    private GameObject ammoUI;
    private int ammo;

    [SerializeField]
    private GameObject healthUI;

    [SerializeField]
    private GameObject crossbow;

    [SerializeField]
    private GameObject arrowAmmo;

    [SerializeField]
    private GameObject beerAmmo;

    [SerializeField]
    private GameObject beerMug;

    private GameObject arrowAmmoClone;

    [SerializeField]
    private GameObject creationPoint;

    [SerializeField]
    private float arrowForce;

    private float health;

    [SerializeField]
    private GameObject sounds;

    public Leaderboard leaderboard;
    private bool drink;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ammo = 100;
        health = 100.0f;
        drink = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (drink == false) //Si no tiene una cerveza en mano, dispara flechas
            {
                if (ammo > 0)
                {
                    ammo--;
                    ammoUI.gameObject.GetComponent<TMP_Text>().text = ammo.ToString() + "/100";
                    arrowAmmoClone = (GameObject)Instantiate(arrowAmmo, creationPoint.gameObject.transform.position, creationPoint.transform.rotation * Quaternion.Euler(270, 15, 0));
                    arrowAmmoClone.gameObject.GetComponent<Rigidbody>().linearVelocity = this.gameObject.transform.GetChild(0).gameObject.transform.forward * arrowForce;
                    Destroy(arrowAmmoClone.gameObject, 5.0f);
                }

                if (ammo == 0)
                {
                    sounds.gameObject.transform.GetChild(3).gameObject.GetComponent<AudioSource>().Play();
                }
            }
            else //Si tiene una cerveza, lanza jarra
            {
                beerAmmo = (GameObject)Instantiate(beerAmmo, creationPoint.gameObject.transform.position, creationPoint.transform.rotation * Quaternion.Euler(270, 15, 0));
                beerAmmo.gameObject.GetComponent<Rigidbody>().linearVelocity = this.gameObject.transform.GetChild(0).gameObject.transform.forward * arrowForce;
                crossbow.gameObject.SetActive(true);
                beerMug.gameObject.SetActive(false);
                drink = false;
                Destroy(beerAmmo.gameObject, 5.0f);
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PruebaEnemigo")
        {
            Debug.Log("enemigo tocado");
        }


        if (other.gameObject.tag == "BeerHealth")
        {
            
            /*
             * BACKPUP SCRIPT SOLO FLECHAS, NO CERVEZA
             * 
            //El objeto no se detruye si la salud est� al m�ximo
            if (health < 100)
            {
                health += 10;
                sounds.gameObject.transform.GetChild(2).gameObject.GetComponent<AudioSource>().Play();
                Destroy(other.gameObject);  
            }
            */


            //SCRIPT PARA LANZAR CERVEZA
            //El objeto no se detruye si la salud est� al m�ximo
            if (health < 100)
            {               
                health += 10;
                sounds.gameObject.transform.GetChild(2).gameObject.GetComponent<AudioSource>().Play();
                Destroy(other.gameObject);
                drink = true; //Activamos booleano para anular el disparo de la flecha 
                crossbow.gameObject.SetActive(false);
                beerMug.gameObject.SetActive(true);
            }
       

            //La salud no aumentar� m�s de 100
            if (health >= 100)
            {
                health = 100;
            }
         
            healthUI.gameObject.GetComponent<Image>().fillAmount = health / 100;
        }

        if (other.gameObject.tag == "Ammo")
        {
            //Si la munici�n est� al m�ximo, el objeto no se eliminar�
            if (ammo < 100)
            {
                ammo += 10;
                Destroy(other.gameObject);   
                sounds.gameObject.transform.GetChild(4).gameObject.GetComponent<AudioSource>().Play();  
            }

            //La munici�n no aumentar� m�s de 100
            if (ammo >= 100)
            {
                ammo = 100;
            }

            if (ammo == 0)
            {
                sounds.gameObject.transform.GetChild(3).gameObject.GetComponent<AudioSource>().Play();
            }
           
            ammoUI.gameObject.GetComponent<TMP_Text>().text = ammo.ToString() + "/100";
        }

        if (other.gameObject.tag == "ArmAttack" || other.gameObject.tag == "HeadAttack")
        {
            if (health > 0)
            {
                health -= 10;
                healthUI.gameObject.GetComponent<Image>().fillAmount = health / 100;
                sounds.gameObject.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
                Debug.Log(health);
            }

            if (health <= 0)
            {
                sounds.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().Play();
                this.gameObject.GetComponent<Animator>().SetTrigger("Accion");
                this.gameObject.GetComponent<CharacterController>().enabled = false;
                Invoke("GameOver", 3.0f);
            }
        }

    }

   

    public void GameOver()
    {
        leaderboard.SetWinLose(0);

        SceneManager.LoadScene("WinLose");
    }

}
