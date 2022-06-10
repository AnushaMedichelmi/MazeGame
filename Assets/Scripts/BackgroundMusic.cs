using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    #region PUBLIC VARIABLES
    #endregion
    #region PRIVATE VARIABLES
    private static BackgroundMusic backgroundMusic;
    #endregion
    #region MONOBEHAVIOUR METHODS
    void Awake()
    {
        if(backgroundMusic == null)
        {
            backgroundMusic = this;
            DontDestroyOnLoad(backgroundMusic);  // Do not destroy the target object when loading a new scene
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    #region PUBLIC METHODS
    #endregion
    #region PRIVATE METHODS
    #endregion
}
