using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    /*YOU HAVE TO CREATE NEW OBJECT IN GAMEPLAY MANAGER SCENE
     AND PUT THIS SCRIPT TO IT
     AND CREATE EVERYTHING THAT YOU WANT IT TO WORK IN BACKGROUND*/
    private static GameplayManager _instance; // First variable of instance
    public static GameplayManager instance => _instance; // Second variable of instance

    [Header("Health Variables")]
    [SerializeField] private Image currentHealthBar;
    public float currentHealth;
    private const float MaxHealth = 100;
    private const float MinHealth = 0;
    private static readonly int Death = Animator.StringToHash("Death");

    [Header("Mana Variables")]
    [SerializeField] private Image currentManaBar;
    public float currentMana;
    public float manaRegeneration = 5;
    private const float MaxMana = 100;
    private const float MinMana = 0;

    [Header("Alert Variables")]
    [SerializeField] private TMP_Text alertText; // Text that you want to alert
    [SerializeField] private TMP_Text currentLevelText;
    public int currentLevel = 1;
    public bool levelUp = false;

    [Header("End Pages Variables")]
    public GameObject youSurvive;
    public GameObject youDead;

    // Awake is called before the first frame update
    private void Awake()
    { _instance = this; } // Call instance this.

    // Update is called once per frame
    private void Update()
    {
        // Level conditions
        if (currentLevel == 3 && levelUp == true)
        {
            levelUp = false;
            manaRegeneration = 15;
            StartCoroutine(SendAlert("Your mana regeneration is increased by x3!", 3));
        }
        currentLevelText.text = $"Level: {currentLevel}";

        // Health conditions
        if (currentHealth > MinHealth)
        {
            // Update health and mana bar
            currentHealthBar.fillAmount = currentHealth / MaxHealth; // Fill health bar
            currentManaBar.fillAmount = currentMana / MaxMana; // Fill mana bar
            // Health & Mana regeneration
            currentMana += manaRegeneration * Time.deltaTime;
            currentHealth += 1 * Time.deltaTime;
        }
        if (currentHealth > MaxHealth) // Health limit
        { currentHealth = MaxHealth; }
        else if (currentHealth < MinHealth) // Death state
        {
            currentHealth = MinHealth;
            FindObjectOfType<PlayerController>().animator.SetTrigger(Death);
            FindObjectOfType<PlayerController>().enabled = false; // Freeze movement
            youDead.SetActive(true); // Set active "You're Dead" object
        }

        // Mana conditions
        if (currentMana > MaxMana) // Mana limit (Maximum)
        { currentMana = MaxMana; }
        else if (currentMana < MinMana) // Mana limit (Minimum)
        { currentMana = MinMana; }
    }

    // Send alert text on the screen -----------------------------------------------------------------------------------
    public IEnumerator SendAlert(string text, int time)
    {
        alertText.text = text; // Show alert text that you want (CODING TEXT ON CONDITIONS YOU WANT!)
        yield return new WaitForSeconds(time); // Time's depend on you (CODING TEXT ON CONDITIONS YOU WANT!)
        alertText.text = ""; // Set text box to be "" / null
    }

    // TakeDamage Method -----------------------------------------------------------------------------------------------
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        CameraShake.Instance.ShakeCamera(5f, 0.1f);
    }
    
    // Reduce, Increase Mana Method ------------------------------------------------------------------------------------
    public bool ReduceMana(float spellCost) // Reduces mana when you attack or use skills.
    {
        if (currentMana >= spellCost)
        {
            currentMana -= spellCost;
            return true;
        }
        return false;
    }
    public void IncreaseMana(float mana) // Increase mana when you collect items.
    {
        StartCoroutine(SendAlert($"You have gained {mana} mana", 1));
        currentMana += mana;
    }
    
    // Clear Game Method -----------------------------------------------------------------------------------------------
    public void ClearGame()
    {
        youSurvive.SetActive(true); // Set active "You Survived" object
    }
    
    // Restart, Return to MainMenu Button Methods ----------------------------------------------------------------------
    public void Restart() 
    { SceneManager.LoadScene("SampleScene");} // Load "SampleScene"

    public void MainMenu()
    { SceneManager.LoadScene("MainMenu"); } // Load "MainMenu"
}