using UnityEngine;

public class TriggerAreaCheck : MonoBehaviour
{
    /*YOU SHOULD CREATE NEW OBJECT TO BE CHILDREN OF MONSTER THAT HAVE "BOX COLLIDER 2D" FOR DETECT PLAYER
     AND PUT THIS SCRIPT TO IT*/
    private EnemyBehavior _enemy; // Link with EnemyBehavior script

    // Awake is called before the first frame update
    private void Awake()
    { _enemy = GetComponentInParent<EnemyBehavior>(); } // Return component to do something in EnemyBehavior script

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player")) // If any game object with tag "Player" come in side
        {
            gameObject.SetActive(false); // Set this game object to be false
            _enemy.target = col.transform; // Give position of that object to _enemy.target
            _enemy.inRange = true; // Set inRange of EnemyBehavior script to be true
            _enemy.dangerZone.SetActive(true); // Set active dangerZone of EnemyBehavior script
        }
    }
}