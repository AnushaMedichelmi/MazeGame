using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverPanel;
    SpriteRenderer sprite;
    int playerSpeed = 3;
    [SerializeField]
    Text scoreText;
    int score;
    Animator anim;
    [SerializeField]
    Button playAgain;
    bool isGameOver;
    // Start is called before the first frame update
    void Start()
    {
       sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gameOverPanel.SetActive(false);
        playAgain.onClick.AddListener(PlayAgain);
        
    }

    // Update is called once per frame
    void Update()
    {
            float inputX = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
            float inputY = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;

       
            transform.Translate(inputX, inputY, 0);
        
        if (inputX < 0)
            {
            
            sprite.flipX = true;
            }
            if (inputX > 0)
            {
           
            sprite.flipX = false;
            }
            if(inputY > 0||inputX>0||inputX<0||inputY<0)
        {
            anim.SetBool("isIdle", false);
        }
            else if(inputY ==0||inputX==0)
        {
            anim.SetBool("isIdle", true);
        }

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Enemy")
        {
            gameOverPanel.SetActive(true);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Cherry")
        {
            score = score + 10;
            scoreText.text = "Score:" + score;
            collision.gameObject.SetActive(false);
        }
        if(collision.gameObject.tag=="End")
        {
            anim.SetBool("isWon", true);
            StartCoroutine("WaitToLoad");
            gameOverPanel.SetActive(true);
            
        }
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }
    IEnumerator  WaitToLoad()
    {
        yield return new WaitForSeconds(5f);
    }
}

