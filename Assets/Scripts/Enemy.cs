using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private float distance;
    private int enemyLife = 3;
    private int enemyAttackDamage = 1;
    private bool attackCooldown;
    private int randomAttack;
    private Vector3 playerPosition;
    private bool blockEnemy;


    void Start(){
        blockEnemy = false;
        attackCooldown = false;
        player =  (GameObject)GameObject.FindGameObjectWithTag("Player");
    }

    void Update(){

        if (gameObject.tag == "Boss"){
            enemyLife = enemyLife * 3;
            enemyAttackDamage = enemyAttackDamage * 3;
        }  

        if (blockEnemy == false){
            distance = Vector3.Distance(this.gameObject.transform.position, player.gameObject.transform.position);

        
            if(distance < 3.0f){
                //ANIMATION ATTACK
                playerPosition = new Vector3 (player.gameObject.transform.position.x, this.gameObject.transform.position.y, player.gameObject.transform.position.z);
                this.gameObject.transform.LookAt(playerPosition);
                if(attackCooldown == false){
                    attackCooldown = true;
                    this.gameObject.GetComponent<NavMeshAgent>().speed = 0.0f; 
                    randomAttack = Random.Range(0,2);

                    if (randomAttack == 0){
                        this.gameObject.GetComponent<Animator>().SetFloat("walking", 0.0f);
                        this.gameObject.GetComponent<Animator>().SetTrigger("attackSlice");  

                    } else {
                        this.gameObject.GetComponent<Animator>().SetFloat("walking", 0.0f);
                        this.gameObject.GetComponent<Animator>().SetTrigger("attackHit");  
                    }

                    Invoke("UnblockAttack", 3.0f);  
                
                } 
            }

            else if (distance < 5.0f){
                //ANIMATION RUN + FOLLOW
                this.gameObject.GetComponent<NavMeshAgent>().speed = 5.0f;
                this.gameObject.GetComponent<Animator>().SetFloat("walking", 1.0f);
                this.gameObject.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        
            } else {
                    this.gameObject.GetComponent<NavMeshAgent>().speed = 0.0f; 
                    
            }
       } 
        
    }

    private void UnblockAttack(){
        attackCooldown = false;
    }

    private void OnCollisionEnter(Collision other){
        if (other.gameObject.tag=="Arrow"){
            
            blockEnemy = true;
            this.gameObject.GetComponent<NavMeshAgent>().speed = 0.0f;
            this.gameObject.GetComponent<Animator>().SetTrigger("death");
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            GameFlow.updateKills();
            Destroy(this.gameObject,5.0f);
        }
    }

  
}
