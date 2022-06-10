using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region SINGLETON 
    
    //Singleton:Only one object or one instance is created out of a class 
    private static GameManager instance;  
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject container = new GameObject("GameManager");
                    instance = container.AddComponent<GameManager>();
                }

            }
            return instance;
        }
    }
    #endregion

    #region PRIVATE VARIABLES

    private int score;
    private Camera mainCamera;
    [SerializeField]      //To serialize a private field
    Text scoreText;
    [SerializeField]
    Button playAgain;
    [SerializeField]
    Button back;
    [SerializeField]
    Button nextLevel;
    [SerializeField]
    GameObject gameOverPanel;
    #endregion



    #region MONOBEHAVIOUR METHODS
    private void Start()
    {
        gameOverPanel.SetActive(false);     //Intially Gameoverpanel is inactive
        playAgain.onClick.AddListener(PlayAgain);   //calling the playagain method
        back.onClick.AddListener(Back);              //calling the back method
        nextLevel.onClick.AddListener(NextLevel);    //method nextlevel
        

    }
    private void Update()
    {

    }
    #endregion
    #region PUBLIC METHODS
    public void UpdateScore(int value)
    {
        score = score + value;
        scoreText.text = "Score:" + score;
    }
    public void GameOver()
    {
       
        //Start coroutine:delay execution of a function 
        StartCoroutine("WaitToLoad");   //dealy for sometime before gameover is displayed

    }

    #endregion
    public void PlayAgain()
    {
       
        //SceneManager.LoadScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);    //loading active scene or current scene
    }

    public void Back()
    {
        SceneManager.LoadScene(0);          //loading menu scene 
    }
   public void NextLevel()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   //loading the next scene
    }

        IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(3f);     //wait for 3 seconds,after 3 seconds
        gameOverPanel.SetActive(true);           //gameover panel is set to active
    }


}