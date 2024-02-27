using UnityEngine;

public class Door : MonoBehaviour
{
    /*YOU HAVE TO CREATE NEW OBJECT CALLED "DOOR" THEN ADD "BOX COLLIDER 2D" COMPONENT FOR DETECT PLAYER
     AND PUT THIS SCRIP TO IT*/
    private enum NextSceneName { StringstarFields, MagicCliffs, TaigaForest }
    [SerializeField] private NextSceneName nextSceneName; // Type scene that you want to go on Inspector

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            switch (nextSceneName)
            {
                case NextSceneName.StringstarFields:
                    LoadingScreenController.Instance.LoadNextScene("StringstarFields");
                    break;
                case NextSceneName.MagicCliffs:
                    LoadingScreenController.Instance.LoadNextScene("MagicCliffs");
                    break;
                case NextSceneName.TaigaForest:
                    LoadingScreenController.Instance.LoadNextScene("TaigaForest");
                    break;
            }
        } // Use instance from LoadingScreenController to load next scene and keep values!
    }
}