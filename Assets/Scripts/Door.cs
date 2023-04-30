using UnityEngine;

public class Door : MonoBehaviour
{
    /*YOU HAVE TO CREATE NEW OBJECT CALLED "DOOR" THEN ADD "BOX COLLIDER 2D" COMPONENT FOR DETECT PLAYER
     AND PUT THIS SCRIP TO IT*/
    private enum NextSceneName { SampleScene, SecondScene, ThirdScene }
    [SerializeField] private NextSceneName nextSceneName; // Type scene that you want to go on Inspector

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            switch (nextSceneName)
            {
                case NextSceneName.SampleScene:
                    LoadingScreenController.Instance.LoadNextScene("SampleScene");
                    break;
                case NextSceneName.SecondScene:
                    LoadingScreenController.Instance.LoadNextScene("SecondScene");
                    break;
                case NextSceneName.ThirdScene:
                    LoadingScreenController.Instance.LoadNextScene("ThirdScene");
                    break;
            }
        } // Use instance from LoadingScreenController to load next scene and keep values!
    }
}