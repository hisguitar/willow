using UnityEngine;

public class MainMenu : MonoBehaviour
{
    /*YOU SHOULD CREATE NEW OBJECT IN MAIN MENU SCENE
     AND PUT THIS SCRIPT TO IT*/
    [SerializeField] private string sceneName;

    private void StartGame() // Start button
    {
        SoundManager.instance.Play(SoundManager.SoundName.Click); // Play Sound: Click!
        LoadingScreenController.Instance.LoadNextScene(sceneName); // Load: SampleScene!
    }

    private void QuitGame() // Quit game button
    {
        SoundManager.instance.Play(SoundManager.SoundName.Click); // Play Sound: Click!
        Application.Quit();
    }
}