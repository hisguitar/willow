using UnityEngine;

public class ManaPotion : MonoBehaviour
{
    /*YOU HAVE TO CREATE NEW OBJECT THEN ADD "BOX COLLIDER 2D" COMPONENT FOR DETECT PLAYER
     AND PUT THIS SCRIPT TO IT*/
    [SerializeField] private int mana;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameplayManager.instance.IncreaseMana(mana); // Increase mana
            SoundManager.instance.Play(SoundManager.SoundName.CollectEffect); // Play Sound: CollectEffect

            Destroy(gameObject); // Destroy this Game Object!
        }
    }
}