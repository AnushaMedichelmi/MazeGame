using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    SpriteRenderer sprite;
    int playerSpeed = 3;
    int score;
    Animator anim;
    [SerializeField]

    bool isGameOver;

    public GameObject trophyParticle;
    [SerializeField] private AudioSource coinEffect;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

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
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.Instance.GameOver();
            this.gameObject.SetActive(false);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cherry")
        {

            coinEffect.Play();
            GameManager.Instance.UpdateScore(10);

            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "End")
        {

            Instantiate(trophyParticle,collision.gameObject.transform.position,Quaternion.identity);
            anim.SetBool("isWon", true);
            GameManager.Instance.GameOver();
        }
    }

}