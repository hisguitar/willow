using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenController : MonoBehaviour
{
    /*YOU SHOULD CREATE NEW OBJECT IN GAMEPLAY MANAGER SCENE
     AND PUT THIS SCRIPT TO IT*/
    public static LoadingScreenController Instance { get; private set; }

    [SerializeField] private GameObject loadingScreenObject;

    private void Awake() //before Start() methods
    { Instance = this; }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        loadingScreenObject.SetActive(true);

        var unloadOp = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        while (!unloadOp.isDone)
        {
            //You can update loading progress here.
            yield return null;
        }

        var loadOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!loadOp.isDone)
        {
            //You can update loading progress here.
            yield return null;
        }
        yield return new WaitForSeconds(3f); //loading time.

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName)); // Active: SampleScene
        loadingScreenObject.SetActive(false); // Inactive: Loading Screen object
    }

    public void LoadNextScene(string screenName)
    { StartCoroutine(LoadSceneCoroutine(screenName)); }
}