using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region PUBLIC VARIABLES
   

    [SerializeField]
    public static PoolManager instance;
    [SerializeField]
    public List<GameObject> pool = new List<GameObject>();  //List of GameObject
    [SerializeField]
    public List<PoolObject> poolItems = new List<PoolObject>(); //List of poolObjects
    #endregion
    #region PRIVATE VARIABLES
    #endregion
    #region MONOBEHAVIOUR METHODS
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        return;
    }
   
    void Start()
    {
        AddToPool();    //Adding gameObjects to pool

    }
    private void Update()
    {

    }
    #endregion
    #region PUBLIC METHODS
    public void AddToPool()
    {
        foreach (PoolObject item in poolItems)
        {
            for (int i = 0; i < item.amount; i++)
            {
                GameObject temp = Instantiate(item.prefab); //Instantiating gameObjects from pool
                temp.name=item.prefab.name;
                pool.Add(temp); //Addidng those gameObjects to pool
                temp.SetActive(false);  //GameObjects setting to false
            }
        }
    }
    public GameObject GetObjectsFromPool(string tagname)
    {
        foreach (GameObject item in pool)
        {
            if (item.gameObject.tag == tagname && !item.activeInHierarchy)  // Getting the item
                                                                            // from pool which is false in hierarchy
            {
                //item.SetActive(true);

                Debug.Log("Item = " + item);
                return item;
                // pool[i].gameObject.SetActive(true);
            }

        }
        return null;

    }
    [System.Serializable]
    public class PoolObject
    {
        public GameObject prefab;       // GameObject is sent into the class 
        public string name;
        public int amount;
    }
    #endregion
    #region PRIVATE METHODS
    #endregion
}