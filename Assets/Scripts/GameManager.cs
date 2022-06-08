using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region SINGLETON
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
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Button playAgain;
    [SerializeField]
    GameObject gameOverPanel;
    #endregion



    #region MONOBEHAVIOUR METHODS
    private void Start()
    {
        gameOverPanel.SetActive(false);
        playAgain.onClick.AddListener(PlayAgain);
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
        StartCoroutine("WaitToLoad");

    }

    #endregion
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }
    IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(5f);
        gameOverPanel.SetActive(true);
    }
}