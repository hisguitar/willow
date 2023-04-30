using UnityEngine;

public class MusicBox : MonoBehaviour
{
    /*YOU HAVE TO CREATE NEW OBJECT IN HIERARCHY AND PUT THIS SCRIPT IN TO IT
     AND PUT THAT OBJECT TO BE PREFAB, SO YOU CAN USE IT IN EVERY SCENE*/
    private enum MusicName { Memories, GodIsGoodToMe, LoveIsPuuung }
    [SerializeField] private MusicName musicName; // Type name of Background Music

    // Update is called once per frame
    public void Start()
    {
        // Stop all music
        if (SoundManager.instance.GetComponent<AudioSource>() != null)
        { SoundManager.instance.GetComponent<AudioSource>().Stop(); }

        switch (musicName)
        {
            // Start new music
            case MusicName.Memories:
                SoundManager.instance.Play(SoundManager.SoundName.Memories);
                break;
            case MusicName.GodIsGoodToMe:
                SoundManager.instance.Play(SoundManager.SoundName.GodIsGoodToMe);
                break;
            case MusicName.LoveIsPuuung:
                SoundManager.instance.Play(SoundManager.SoundName.LoveIsPuuung);
                break;
        }
    }
}