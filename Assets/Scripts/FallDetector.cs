using UnityEngine;

public class FallDetector : MonoBehaviour
{
    /*YOU SHOULD PUT THIS SCRIPT TO CHARACTER THAT'S ON THE ISLAND.*/
    private Vector3 _respawnPoint;
    public GameObject fallDetector;
    public bool enemy;

    // Start is called before the first frame update
    private void Start()
    { _respawnPoint = transform.position; }

    // Update is called once per frame
    private void Update()
    { fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y); }
    
    // Fall Detector Method
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("FallDetector")) return;
        switch (enemy)
        {
            case true: // FOR ENEMY
            { FindObjectOfType<EnemyBehavior>().TakeDamage(20); break; }
            case false: // FOR PLAYER
            { GameplayManager.instance.TakeDamage(40); break; }
        }
        transform.position = _respawnPoint; // Respawn this object
    }
}