using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum STATE { LOOKFOR, GOTO , ATTACK};
    public STATE currentState = STATE.LOOKFOR;
    public float gotoDistance;
    public Transform target;
    Vector3 startPosition;
    Animator animator;
    NavMeshAgent agent;
    float time;
    float attackDistance = 3f;
    // Start is called before the first frame update
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
                        LookFor();

                        break;
                    case STATE.GOTO:
                        Goto();
                        break;
                    case STATE.ATTACK:
                        Attack();
                        break;
                    default:
                        break;

                }
                yield return null;
            }
            else
                break;

        }

    }


    public void LookFor()
    {
        animator.SetBool("isIdle", true);
        agent.ResetPath();
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
            agent.SetDestination(target.position);
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
                GameObject tempBullet = PoolManager.instance.GetObjectsFromPool("Bullet");
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


    private float PlayerDistance()
    {
        return Vector3.Distance(target.transform.position, this.transform.position);
    }
}