using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FPS : MonoBehaviour
{
    [SerializeField]
    private GameObject ammoUI;
    private int ammo;

    [SerializeField]
    private GameObject healthUI;

    [SerializeField]
    private GameObject arrowAmmo;

    private GameObject arrowAmmoClone;

    [SerializeField]
    private GameObject creationPoint;

    [SerializeField]
    private float arrowForce;

    private float health;

    [SerializeField]
    private GameObject sounds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ammo = 100;
        health = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PruebaEnemigo")
        {
            Debug.Log("enemigo tocado");
        }


        if (other.gameObject.tag == "BeerHealth")
        {

            //El objeto no se detruye si la salud está al máximo
            if (health < 100)
            {
                health += 10;
                sounds.gameObject.transform.GetChild(2).gameObject.GetComponent<AudioSource>().Play();
                Destroy(other.gameObject);
            }

            //La salud no aumentará más de 100
            if (health >= 100)
            {
                health = 100;
            }
         
            healthUI.gameObject.GetComponent<Image>().fillAmount = health / 100;
        }

        if (other.gameObject.tag == "Ammo")
        {
            //Si la munición está al máximo, el objeto no se eliminará
            if (ammo < 100)
            {
                ammo += 10;
                Destroy(other.gameObject);   
                sounds.gameObject.transform.GetChild(4).gameObject.GetComponent<AudioSource>().Play();  
            }

            //La munición no aumentará más de 100
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

        

    }

    public void OnCollisionEnter(Collision other)
    {

      
            //Daño enemigo. Tag Enemy solo para pruebas
            if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "ArmAttack" || other.gameObject.tag == "HeadAttack")
            {
                if (health > 0)
                {
                    health -= 10;
                    healthUI.gameObject.GetComponent<Image>().fillAmount = health / 100;
                    sounds.gameObject.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
                    Debug.Log(health);
                }

                if (health == 0)
                {
                    sounds.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().Play();
                    this.gameObject.GetComponent<Animator>().SetTrigger("Accion");
                    Invoke("GameOver", 3.0f);
                }
            }
        }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

}
