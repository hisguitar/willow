using UnityEngine;

public class ClearPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        { GameplayManager.instance.ClearGame(); }
    }
}
