using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /* YOU HAVE TO CREATE NEW OBJECT TO BE PROJECTILE
     * AND PUT THIS SCRIPT TO IT
     * THEN MAKE THIS OBJECT TO BE PREFAB, SO YOU CAN USE IT TO BE PROJECTILE */
     
    [SerializeField] private float damage; // Damage of the projectile
    [SerializeField] private float initialSpeed = 8f; // Initial speed of the projectile
    [SerializeField] private Rigidbody2D rb; // Rigidbody of projectile
    [SerializeField] private GameObject impactEffect; // Projectile impact
    
    private void Start()
    {
        // Initial velocity of the projectile
        rb.velocity = -transform.right * initialSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if enemy/player collide to this object or not
        EnemyBehavior enemy = collision.gameObject.GetComponent<EnemyBehavior>();
        if (enemy != null)
        {
            // Any object using EnemyBehavior script will takes damage
            enemy.TakeDamage(damage);
        }


        /* Determine the direction of the projectile
         * Create a rotation to face the direction */
        Vector2 direction = -transform.right;
        Quaternion rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 0f);
        
        /* Play Bullet impact sound
         * Instantiate the impactEffect with the correct orientation */
        SoundManager.instance.Play(SoundManager.SoundName.ShotEffect); // Play Sound: ShotEffect
        Instantiate(impactEffect, transform.position, rotation);
        Destroy(this.gameObject);
    }
    
    // When projectile disappears from the screen
    private void OnBecameInvisible()
    { Destroy(this.gameObject); }
}