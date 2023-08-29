using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class skeletonAttackTrigger : MonoBehaviour
{
    public Enemy enemyScript;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            enemyScript.rb.velocity = new Vector2(0, 0);
            enemyScript.IsEnemyAttacking = true;
            enemyScript.GetComponent<SpriteRenderer>().color = new Color(215f, 255f, 236f, 1f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            enemyScript.IsEnemyAttacking = false;
        }
    }
}
