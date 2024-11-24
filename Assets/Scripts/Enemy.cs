using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private float distance;
    private int enemyLife = 3;
    public int enemyAttackDamage;
    private int BaseEnemyAttackDamage = 1;
    private bool attackCooldown;
    private int randomAttack;
    private Vector3 playerPosition;
    private bool blockEnemy;

    public CountdownTimer countdownTimer;

    void Start(){
        blockEnemy = false;
        attackCooldown = false;
        player =  (GameObject)GameObject.FindGameObjectWithTag("Player");

        if (gameObject.tag == "Boss")
        {
            enemyLife = enemyLife * 3;
            BaseEnemyAttackDamage = BaseEnemyAttackDamage * 3;
        }
    }

    void Update()
    {
        playerPosition = new Vector3(player.gameObject.transform.position.x, this.gameObject.transform.position.y, player.gameObject.transform.position.z);
        distance = Vector3.Distance(this.gameObject.transform.position, player.gameObject.transform.position);
        
        if(blockEnemy == false)
        {
            if (distance < 2f)
            {
                //ANIMATION ATTACK
                this.gameObject.transform.LookAt(playerPosition);
                if (attackCooldown == false)
                {
                    enemyAttackDamage = BaseEnemyAttackDamage;

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

                    enemyAttackDamage = 0;

                    Invoke("UnblockAttack", 1.5f);
                }
            }

            if (distance < 5.0f && distance >= 2f)
            {
                //ANIMATION RUN + FOLLOW
                this.gameObject.transform.LookAt(playerPosition);
                this.gameObject.GetComponent<NavMeshAgent>().speed = 5.0f;
                this.gameObject.GetComponent<Animator>().SetFloat("walking", 1.0f);
                this.gameObject.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
            }

            if (distance >= 5)
            {
                this.gameObject.GetComponent<NavMeshAgent>().speed = 0.0f;
                this.gameObject.GetComponent<Animator>().SetFloat("walking", 0.0f);
            }
        }
    }
    

    private void UnblockAttack(){
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

        if(enemyLife <= 0)
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
        GameFlow.updateKills();
        Destroy(this.gameObject, 5.0f);

        countdownTimer.YouWin();        
    }

    
}
