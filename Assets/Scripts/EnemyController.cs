using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    #region PUBLIC VARIABLES
    public enum STATE { LOOKFOR, GOTO , ATTACK};    //Different states
    public STATE currentState = STATE.LOOKFOR;          //Current state
    public float gotoDistance; 
    public Transform target;
    #endregion
    #region PRIVATE VARIABLES
    Vector3 startPosition;
    Animator animator;
    NavMeshAgent agent;
    float time;
    float attackDistance = 3f;
    #endregion
    // Start is called before the first frame update
    #region MONOBEHAVIOUR METHODS
    IEnumerator Start()
    {

        animator = GetComponent<Animator>();
        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();

        while (true)
        {
            if (target != null)
            {
                switch (currentState)
                {
                    case STATE.LOOKFOR:
                        LookFor();     //calling Lookfor method

                        break;
                    case STATE.GOTO:
                        Goto();         //calling goto method
                        break;
                    case STATE.ATTACK:
                        Attack();        //calling attack method
                        break;
                    default:
                        break;

                }
                
            }
            else
                break;
            yield return null;

        }

    }

    #endregion
    #region PUBLIC METHODS
    public void LookFor()           //LookFor Method
    {
        animator.SetBool("isIdle", true);      
        transform.eulerAngles = Vector3.zero;

        if (PlayerDistance() < gotoDistance)
        {
            currentState = STATE.GOTO;
        }

        /*   else
           {
               transform.position = startPosition;
           }*/
        print("This is LookForState");
    }
    public void Goto()
    {
        animator.SetBool("isIdle", false);
        if (PlayerDistance() > attackDistance)
        {
            agent.SetDestination(target.position);  //setting player as atarget and to follow it
            transform.eulerAngles = Vector3.zero;
            // transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 1*Time.deltaTime);
        }
        if(PlayerDistance() >gotoDistance)
        {
            currentState=STATE.LOOKFOR;
        }

        else if(PlayerDistance() < attackDistance)
        {
            currentState = STATE.ATTACK;
        }
        print("This is GotoState");
    }
    public void Attack()
    {
        print("This is AttackState");
        transform.eulerAngles = Vector3.zero;
        time = time + Time.deltaTime;
        if (target.GetComponent<PlayerMovement>().isGameOver!=true)
        {
            if (time > 0.5f)
            {
                // GameObject tempBullet = Instantiate(bullet,this.transform.position,Quaternion.identity);
                GameObject tempBullet = PoolManager.instance.GetObjectsFromPool("Bullet");          //Bullet is taken from pool
                tempBullet.SetActive(true);                                                         
                tempBullet.transform.position = this.transform.position;
                time = 0;

            }
        }
        if (PlayerDistance() > attackDistance)
        {
            currentState = STATE.GOTO;
        }
    }
    #endregion

    #region PRIVATE METHODS
    private float PlayerDistance()
    {
        return Vector3.Distance(target.transform.position, this.transform.position);    //Calculating Distance from enemy and player
    }
    #endregion
}