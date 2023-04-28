using UnityEngine;
using UnityEngine.SceneManagement;

public class Initializer : MonoBehaviour
{
    /*YOU HAVE TO CREATE NEW OBJECT
     AND PUT THIS SCRIPT TO IT
     THEN MAKE THIS OBJECT TO BE PREFAB, SO YOU MUST USE IT IN EVERY SCENE*/
    private const string LoadingScreenName = "GameplayManager";

    // Awake is called before the first frame update
    private void Awake()
    {
        // Get a reference to the loading screen scene
        var loadingScreenScene = SceneManager.GetSceneByName(LoadingScreenName); // load the loading screen scene
        
        // Check if the scene is not loaded yet
        if (!loadingScreenScene.isLoaded)
        { SceneManager.LoadScene(LoadingScreenName, LoadSceneMode.Additive); } // load the loading screen scene again!
    }
}