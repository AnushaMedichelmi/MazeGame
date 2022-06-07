using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[SerializeField]
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);   //To load scene1
    }

    public void PlayGameTwo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);     //To load scene 2
    }

    public void PlayGameThree()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);   //To load scene 3
    }



    public void QuitGame()
    {
        Application.Quit();
    }
}
