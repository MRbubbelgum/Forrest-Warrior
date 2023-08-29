using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkeletonTargetTrigger : MonoBehaviour
{
    public Enemy enemyScript;
    public PlayerCombat playerCombatScript;
    public GameObject MainCharacter;
    [SerializeField] private string mainCharacter = "MainCharacter";
    

    // Start is called before the first frame update

    void Start()
    {
        MainCharacter = GameObject.Find(mainCharacter);
        playerCombatScript = MainCharacter.GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.layer == 3)
            {
                enemyScript.isEnemyChasing = true;
            }
            
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (playerCombatScript.currentHealth == 0)
        {
            enemyScript.isEnemyChasing = false;
            
        }
    }
}
