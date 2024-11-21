using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
                //Disparo de flechas. Revisar restriccion
                arrowAmmoClone = (GameObject)Instantiate(arrowAmmo, creationPoint.gameObject.transform.position, Quaternion.identity);
                arrowAmmoClone.gameObject.GetComponent<Rigidbody>().linearVelocity = this.gameObject.transform.GetChild(0).gameObject.transform.forward * arrowForce;
                Destroy(arrowAmmoClone.gameObject, 5.0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BeerHealth")
        {
            Destroy(other.gameObject);
            health += 10;
           
                if (health >= 100)
                {
                    health = 100;
                }

            healthUI.gameObject.GetComponent<Image>().fillAmount = health / 100;
        }

        if (other.gameObject.tag == "Ammo")
        {
            Destroy(other.gameObject);
            ammo += 10;
            if (ammo >= 100)
            {
                ammo = 100;
            }
            ammoUI.gameObject.GetComponent<TMP_Text>().text = ammo.ToString() + "/100";
        }

        //PRUEBA ENEMIGO. PILLAR EL CUBO E IR ESTAMPANDOLO AL FPSCONTROLLER
        if (other.gameObject.tag == "pruebaEnemigo")
        {
            if (health > 0)
            {
                health -= 10;
                healthUI.gameObject.GetComponent<Image>().fillAmount = health / 100;
                Debug.Log(health);
            }
            else
            {
                Debug.Log("You're dead");
            }
        }

        /*
         * Dejo el if de los ataques de enemigos hechos. Falta el tag.
         
        if (other.gameObject.tag == )
        {
            health = health - 10;
            healthUI.gameObject.GetComponent<Image>().fillAmount = health / 100;
        }
        */
    }

}
