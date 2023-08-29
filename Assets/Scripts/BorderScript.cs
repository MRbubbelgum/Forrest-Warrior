using Cainos.LucidEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BorderScript : MonoBehaviour
{

    public GameObject enemyBorder;
    public GameObject EnemyHealthBar;
    
    
    [SerializeField] private string enemyHealthBar = "EnemyHealthBar";
    



    [ContextMenu("RemoveBorder")]
    public void removeBorder()
    {
        Debug.Log("Border försvinner");
        enemyBorder.GetComponent<Image>().enabled = false;
    }

    [ContextMenu("BorderInvisible")]
    public void borderInvisible()
    {
        enemyBorder.GetComponent<Image>().enabled = false;
    }

    [ContextMenu("BorderVisible")]
    public void borderVisible()
    {
        enemyBorder.GetComponent<Image>().enabled = true;
    }



    // Start is called before the first frame update
    void Start()
    {
       
       EnemyHealthBar = GameObject.Find(enemyHealthBar);
        enemyBorder = GameObject.FindWithTag("EnemypBorder");
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
