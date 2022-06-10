using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    #region PUBLIC VARIABLES
    SpriteRenderer sprite;         //Renders a sprite for 2d graphics
    public int playerSpeed = 3;      //variable for playerspeed
    public bool isGameOver;

    public GameObject trophyParticle;              //Declaring a particle effect for trophy

    public GameObject powerUpParticle;             //Declaring a particle effect for powerup

    #endregion
    #region PRIVATE VARIABLES
    int score;                       //declaring variable score
    Animator anim;                    //Declaring animator
    [SerializeField] private AudioSource coinEffect;    //Declaring audio source
    #endregion
    #region MONOBEHAVIOUR METHODS
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        transform.GetChild(0).gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;     //movement of player in horizantal direction
        float inputY = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;       //movement of player in vertical direction


        transform.Translate(inputX, inputY, 0);

        if (inputX < 0)
        {

            sprite.flipX = true;            //flips the sprite on the x-axis
        }
        if (inputX > 0)
        {

            sprite.flipX = false;
        }
        if (inputY > 0 || inputX > 0 || inputX < 0 || inputY < 0)
        {
            anim.SetBool("isIdle", false);
        }
        else if (inputY == 0 || inputX == 0)
        {
            anim.SetBool("isIdle", true);
        }

    }
   
    public void OnCollisionEnter2D(Collision2D collision)   //Detects Collisions
    {
        if (collision.gameObject.tag == "Enemy")          //if player collides with enemy
        {
            GameManager.Instance.GameOver();              //game over condition is called
            this.gameObject.SetActive(false);
            isGameOver=true;
        }
        if (collision.gameObject.tag == "Bullet")             //if player collides with bullet 
        {
            GameManager.Instance.GameOver();                 //gameover function is called
            this.gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            isGameOver = true;
        }
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cherry")       //if player hits the cherries
        {

            coinEffect.Play();                             //playing audio
            GameManager.Instance.UpdateScore(10);          //increasing score by 10

            collision.gameObject.SetActive(false);         //making the cherry inactive
        }
        if (collision.gameObject.tag == "End")             //if player hits the trophy
        {

            Instantiate(trophyParticle, collision.gameObject.transform.position, Quaternion.identity);    //particle effect is instantiated
            anim.SetBool("isWon", true);                                                                  //win condition
            isGameOver = true;
            GameManager.Instance.GameOver();                                                              //Gameover funtion is called
        }
        if (collision.gameObject.tag=="PowerUp")       //if player hits powerup
        {
           GameObject temp= Instantiate(powerUpParticle, collision.gameObject.transform.position, Quaternion.identity);  //particle effect is instantiated
            collision.gameObject.SetActive(false);         //Making the powerup inactive
            PowerUpCollected(temp);                             //Calling the powerupcollected method

        }

    }
    #endregion
    #region PUBLIC METHODS
    public void PowerUpCollected(GameObject temp)
    {
         temp.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);     //particle effect
        Physics2D.IgnoreLayerCollision(3, 6, true);           //ignoring collision with bullets using layer
       // Physics2D.IgnoreLayerCollision(3,)
        Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), GameObject.Find("Enemy").GetComponent<Collider2D>(), true);   //ignoring collision with enemy using Tag
        print("collisions ignored");
        StartCoroutine("TimeForPowerUp");      //To delay the time to ignore collisions

       // GameObject.Find("Bullet");

    }
    IEnumerator TimeForPowerUp()
    {
        yield return new WaitForSeconds(6);
        print("collisions detecting");
        Physics2D.IgnoreLayerCollision(3, 6, false);        //Detecting collisions after some time with bullet
        transform.GetChild(0).gameObject.SetActive(false); 
        Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), GameObject.Find("Enemy").GetComponent<Collider2D>(), false);   //Detecting collisions after some time with enemy


    }
    #endregion
    #region PRIVATE METHODS
    #endregion

}
