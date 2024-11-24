using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionRadius = 5f;
    public LayerMask damageableLayer;

    public FPS fpsScript;

    public void Explode()
    {
        Animator anim = gameObject.GetComponentInParent<Animator>();

        anim.SetBool("Accion",true);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, damageableLayer);
        
        foreach (Collider hit in hitColliders) 
        {
            if(hit.gameObject.tag == "Player")
            {
                fpsScript.ExplosionDamage();
            }

            if(hit.gameObject.tag == "Enemy")
            {
                Enemy enemyScript = hit.GetComponent<Enemy>();
                enemyScript.ExplosionDamage();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            Explode();
        }
    }
}
