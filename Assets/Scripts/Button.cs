using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    /*YOU HAVE TO CREATE NEW OBJECT IN MAIN MENU SCENE
     AND PUT THIS SCRIPT TO IT*/
    private enum SceneName { SampleScene, SecondScene, ThirdScene}
    [SerializeField] private SceneName sceneName;

    public void EnterScene()
    {
        SoundManager.instance.Play(SoundManager.SoundName.Click);
        switch (sceneName)
        {
            case SceneName.SampleScene:
                LoadingScreenController.Instance.LoadNextScene("SampleScene");
                break;
            case SceneName.SecondScene:
                LoadingScreenController.Instance.LoadNextScene("SecondScene");
                break;
            case SceneName.ThirdScene:
                LoadingScreenController.Instance.LoadNextScene("ThirdScene");
                break;
        }
    }

    public void BackToMainMenu()
    {
        SoundManager.instance.Play(SoundManager.SoundName.Click);
        //LoadingScreenController.Instance.LoadNextScene("MainMenu");
        SceneManager.LoadScene("MainMenu");
    }

    public void EndCredit()
    {
        SoundManager.instance.Play(SoundManager.SoundName.Click);
        //LoadingScreenController.Instance.LoadNextScene("EndCredit");
        SceneManager.LoadScene("EndCredit");
    }
    
    // Quit game button
    public void QuitGame()
    {
        SoundManager.instance.Play(SoundManager.SoundName.Click);
        Application.Quit();
    }
}