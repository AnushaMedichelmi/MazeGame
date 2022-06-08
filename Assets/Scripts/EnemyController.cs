using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum STATE { LOOKFOR, GOTO };
    public STATE currentState = STATE.LOOKFOR;
    public float gotoDistance;
    public Transform target;
    Vector3 startPosition;
    Animator animator;
    NavMeshAgent agent;
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
        if (PlayerDistance() < 3f)
        {
            agent.SetDestination(target.position);
            transform.eulerAngles = Vector3.zero;
            // transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 1*Time.deltaTime);
        }

        else
        {
            currentState = STATE.LOOKFOR;
        }
        print("This is GotoState");
    }


    private float PlayerDistance()
    {
        return Vector3.Distance(target.transform.position, this.transform.position);
    }
}