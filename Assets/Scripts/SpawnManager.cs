using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    #region PUBLIC VARIABLES
    #endregion
    #region PRIVATE VARIABLES
    [SerializeField]                    //To serialize a private field
    GameObject[] itemsToPickFrom;      
    [SerializeField]
    int gridX;
    [SerializeField]
    int gridY;
    [SerializeField]
    float gridSpacingOffset = 3f;
    [SerializeField]
    Vector3 gridOrigin = Vector3.zero;
    [SerializeField]
    GameObject powerUp;
    //  bool isPowerUp = false;
    int count = 0;
    #endregion
    #region MONOBEHAVIOUR METHODS
    private void Start()
    {
        SpawnCherries();         
        SpawnPowerUp();          
    }
    #endregion
    #region PUBLIC METHODS
    #endregion
    #region PRIVATE METHODS
    void SpawnPowerUp()
    {
        while (count==0)         
        {
            Vector2 spawnPointPowerUp = new Vector2(Random.Range(-7, 7), Random.Range(-4, 4));    //spawning of powerup in a particular range
            RaycastHit2D hit = Physics2D.Raycast(spawnPointPowerUp, -transform.up, 2f);       //if 

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
            }
            else if (hit.collider == null)
            {
                Debug.Log("spawned powerup");
                GameObject tempPowerUp = Instantiate(powerUp, spawnPointPowerUp, Quaternion.identity);
               // isPowerUp = true;
               count++;
            }
        }

    }
    void SpawnCherries()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                Vector3 spawnPoint = new Vector3(x*gridSpacingOffset, y*gridSpacingOffset, 0) + gridOrigin;
                Debug.DrawRay(spawnPoint, Vector3.forward, Color.red, 30f);          //Draw a line from start to start+dir in world coordinates

                RaycastHit2D hit = Physics2D.Raycast(spawnPoint, -transform.up, 2f); 
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                }
                if (hit.collider == null)
                {
                    PickAndSpawn(spawnPoint);
                }



            }
        }
    }
    void PickAndSpawn(Vector3 spawnPosition)
    {
        // int randomIndex = Random.Range(0, itemsToPickFrom.Length);
        GameObject tempCherry = PoolManagerScript.Instance.Spawn("Cherries");
       // tempCherry.SetActive(true);
        tempCherry.transform.position= new Vector3( spawnPosition.x,spawnPosition.y,0f);
    }
    #endregion
}


