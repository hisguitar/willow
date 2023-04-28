using UnityEngine;

public class Projectile : MonoBehaviour
{
    /*YOU HAVE TO CREATE NEW OBJECT TO BE PROJECTILE
     AND PUT THIS SCRIPT TO IT
     THEN MAKE THIS OBJECT TO BE PREFAB, SO YOU CAN USE IT TO BE PROJECTILE*/
    [SerializeField] private float speed = 8.5f; // Projectile speed
    [SerializeField] private GameObject explosionEffect; // Projectile impact

    // Update is called once per frame
    private void Update()
    { transform.position += - transform.right * speed * Time.deltaTime; } // Projectile movement

    private void OnCollisionEnter2D() // When it collides with another object
    {
        SoundManager.instance.Play(SoundManager.SoundName.ShotEffect); // Play Sound: ShotEffect
        Instantiate(explosionEffect, transform.position, Quaternion.identity); // Shot impact
        
        FindObjectOfType<EnemyBehavior>().TakeDamage(10); // Any object used EnemyBehavior script takes 10 damage
        Destroy(this.gameObject); // Destroy this game object
    }

    private void OnBecameInvisible() // When it disappears from the screen
    { Destroy(this.gameObject); } // Destroy this game object
}