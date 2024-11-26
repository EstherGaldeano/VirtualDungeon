using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private float distance;
    private int enemyLife = 3;
    public int enemyAttackDamage = 1;
    private bool attackCooldown;
    private int randomAttack;
    private Vector3 playerPosition;
    private bool blockEnemy;

    private float attackDistance = 2.0f;
    private float followDistance = 10.0f;

    public CountdownTimer countdownTimer;

    void Start(){
        blockEnemy = false;
        attackCooldown = false;
        player =  (GameObject)GameObject.FindGameObjectWithTag("Player");

        if (gameObject.tag == "Boss")
        {
            enemyLife = enemyLife * 3;
            enemyAttackDamage = enemyAttackDamage * 3;
            attackDistance = 3.0f;
            followDistance = 10.0f;
        }

        if (gameObject.tag == "EnemyJail")
        {
            followDistance = 10.0f;
        }
    }

    void Update()
    {
        playerPosition = new Vector3(player.gameObject.transform.position.x, this.gameObject.transform.position.y, player.gameObject.transform.position.z);
        distance = Vector3.Distance(this.gameObject.transform.position, player.gameObject.transform.position);
        
        if(blockEnemy == false)
        {
            if (distance < attackDistance)
            {
                //ANIMATION ATTACK
                this.gameObject.transform.LookAt(playerPosition);
                if (attackCooldown == false)
                {
                    attackCooldown = true;
                    this.gameObject.GetComponent<NavMeshAgent>().speed = 0.0f;
                    randomAttack = Random.Range(0, 2);

                    if (randomAttack == 0)
                    {
                        this.gameObject.GetComponent<Animator>().SetFloat("walking", 0.0f);
                        this.gameObject.GetComponent<Animator>().SetTrigger("attackSlice");
                    }
                    else
                    {
                        this.gameObject.GetComponent<Animator>().SetFloat("walking", 0.0f);
                        this.gameObject.GetComponent<Animator>().SetTrigger("attackHit");
                    }

                    Invoke("UnblockAttack", 1.5f);
                }
            }

            if (distance < followDistance && distance >= attackDistance)
            {
                //ANIMATION RUN + FOLLOW
                this.gameObject.transform.LookAt(playerPosition);
                this.gameObject.GetComponent<NavMeshAgent>().speed = 5.0f;
                this.gameObject.GetComponent<Animator>().SetFloat("walking", 1.0f);
                this.gameObject.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
            }

            if (distance >= followDistance)
            {
                this.gameObject.GetComponent<NavMeshAgent>().speed = 0.0f;
                this.gameObject.GetComponent<Animator>().SetFloat("walking", 0.0f);
            }
        }
    }
    

    private void UnblockAttack()
    {
        attackCooldown = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag=="Arrow")
        {
            EnemyLoseLife();
        }
    }

    private void EnemyLoseLife()
    {
        enemyLife--;

        CheckEnemyLife();
    }

    public void ExplosionDamage()
    {
        enemyLife -= 3;

        CheckEnemyLife();
    }

    private void CheckEnemyLife()
    {
        if (enemyLife <= 0)
        {
            EnemyDeath();
        }
    }

    private void EnemyDeath()
    {
        blockEnemy = true;
        this.gameObject.GetComponent<NavMeshAgent>().speed = 0.0f;
        this.gameObject.GetComponent<Animator>().SetTrigger("death");
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;

        foreach (Transform child in transform)
        {
            Collider collider = child.GetComponent<Collider>();
            if(collider != null)
            {
                collider.enabled = false;
            }
        }

        GameFlow.updateKills();
        Destroy(this.gameObject, 5.0f);

        if(gameObject.tag == "Boss")
        {
            countdownTimer.YouWin();
        }
    }    
}
