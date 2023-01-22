using UnityEngine;

public class ThornTrap : MonoBehaviour
{
    /*YOU SHOULD CREATE NEW OBJECT THAT HAVE "BOX COLLIDER 2D"
     AND PUT THIS SCRIPT TO IT*/
    
    [SerializeField] private GameObject bleedingEffect; // Bleeding object
    [SerializeField] private int damage; // Trap damage

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SoundManager.instance.Play(SoundManager.SoundName.StabbedEffect); // Bleeding sound effect
            Instantiate(bleedingEffect, GameObject.Find("Player Collider").transform.position, Quaternion.identity); // Bleeding effect
            GameplayManager.instance.TakeDamage(damage); // Player will take damage
        }
    }
}