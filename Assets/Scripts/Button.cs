using UnityEngine;

public class Button : MonoBehaviour
{
    /*YOU HAVE TO CREATE NEW OBJECT IN MAIN MENU SCENE
     AND PUT THIS SCRIPT TO IT*/
    [SerializeField] private string sceneName;

    public void StartButton() // Start button
    {
        SoundManager.instance.Play(SoundManager.SoundName.Click); // Play Sound: Click!
        LoadingScreenController.Instance.LoadNextScene(sceneName); // Load: SampleScene!
    }

    public void QuitButton() // Quit game button
    {
        SoundManager.instance.Play(SoundManager.SoundName.Click); // Play Sound: Click!
        Application.Quit();
    }
}