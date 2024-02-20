using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    /*YOU HAVE TO CREATE NEW OBJECT IN MAIN MENU SCENE
     AND PUT THIS SCRIPT TO IT*/
    public void ChangeScene(string sceneName)
    {
        SoundManager.instance.Play(SoundManager.SoundName.Click);
        LoadingScreenController.Instance.LoadNextScene(sceneName);
    }

    public void ChangeSceneInMenu(string sceneName)
    {
        SoundManager.instance.Play(SoundManager.SoundName.Click);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        SoundManager.instance.Play(SoundManager.SoundName.Click);
        Application.Quit();
    }
}