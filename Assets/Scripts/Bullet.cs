using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region PUBLIC VARIABLES
    #endregion
    #region PRIVATE VARIABLES
    
    float moveSpeed = 7f;   //Speed variable
    Rigidbody2D rb;           //Rigidbody reference
    PlayerMovement target;  //target variable
    Vector2 moveDirection;  //vector to move direction variable that will represent direction to target
    float time=0;
    #endregion
    #region MONOBEHAVIOUR METHODS
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();               //we assign rigidbody by the variable
        Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), GameObject.Find("Enemy").GetComponent<Collider2D>(), true);
        Physics2D.IgnoreLayerCollision(6, 6);
        target = GameObject.FindObjectOfType<PlayerMovement>();   //we assign target variable finding our playermovement 
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;  //we calculate direction to target by subtraction target position and bullet postion resulting vector is normalized and multiplied by movespeed
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);    //setting velocity to bullets to move in particular direction
     //   transform.Translate(new Vector2(moveDirection.x, moveDirection.y));
        StartCoroutine(BackToPool());

    }
    void Update()
    {
      /*  time=time+ Time.deltaTime;
        if(time>1f)
        {

            this.gameObject.SetActive(false);
            time=0;
        }*/
    }
    #endregion
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))   //if bullet collides with any gameobject then we check if its player 
        {
            Debug.Log("Hit");
            this.gameObject.SetActive(false);//then we send message 
                                            
        }
    }
    IEnumerator BackToPool()
    {
        Debug.Log("InCoroutine");
        yield return new WaitForSeconds(1f);
        PoolManagerScript.Instance.Recycle("Bullet", this.gameObject);
    }
}
