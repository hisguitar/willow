using UnityEngine;

public class ObstacleCheck : MonoBehaviour
{
    /*YOU HAVE TO CREATE NEW OBJECT TO BE CHILDREN OF MONSTER OR.. THEN ADD "COLLIDER" COMPONENT FOR DETECT OBSTACLE
     AND PUT THIS SCRIPT TO IT*/
    private EnemyBehavior _enemy; // Link with EnemyBehavior script
    
    // Awake is called before the first frame update
    private void Awake()
    { _enemy = GetComponentInParent<EnemyBehavior>(); } // Return component to do something in EnemyBehavior script
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ground")) // If this game object collider touching any game object with tag "Ground"
        { _enemy.CrossOver(); } // Cross that obstacle (MUST BE TAG "Ground")
    }
}