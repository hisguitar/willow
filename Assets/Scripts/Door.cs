using UnityEngine;

public class Door : MonoBehaviour
{
    /*YOU SHOULD CREATE NEW OBJECT "DOOR" THAT HAVE "BOX COLLIDER 2D" FOR DETECT PLAYER
     AND PUT THIS SCRIP TO IT*/
    [SerializeField] private string nextSceneName; // Type scene that you want to go on Inspector

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        { LoadingScreenController.Instance.LoadNextScene(nextSceneName); } // Use instance from LoadingScreenController to load next scene and keep values!
    }
}