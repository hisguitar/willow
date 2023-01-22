using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*YOU SHOULD CREATE NEW OBJECT THAT HAVE "RIGIDBODY 2D, SPRITE RENDERER, ANIMATOR"
     AND PUT THIS SCRIPT TO IT
     THEN YOU HAVE TO CREATE CHILDREN OBJECT..
     - PLAYER COLLIDER THAT HAVE "BOX COLLIDER 2D"
     - LIGHT 2D FOR BEAUTIFUL CHARACTER!*/
    [Header("Movement Variables")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    private Rigidbody2D _rb;
    
    [Header("Other Variables (Animations, Projectile)")]
    public Animator animator; // Link animator with this code
    private static readonly int Speed = Animator.StringToHash("Speed"); // Speed parameter
    [SerializeField] private Transform launchOffset; // Spawn point of projectile
    [SerializeField] private Projectile projectilePrefab; // Knife prefab

    // Awake, Update and FixedUpdate Methods ---------------------------------------------------------------------------
    // Awake is called before start
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Combat();
    }

    private void FixedUpdate()
    {
        Move();
    }
    
    // Move Method -----------------------------------------------------------------------------------------------------
    // ReSharper disable Unity.PerformanceAnalysis
    private void Move()
    {
        var movement = Input.GetAxis("Horizontal");
        
        // Setting float to show walking animation
        animator.SetFloat(Speed, Mathf.Abs(movement));

        // Character walk
        transform.position += new Vector3(movement, 0, 0) * (Time.deltaTime * movementSpeed);
        
        // Character rotation
        if (!Mathf.Approximately(0, movement))
        { transform.rotation = movement > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity; }
        
        // Character jump
        if (Input.GetButton("Jump"))
        {
            if (Mathf.Abs(_rb.velocity.y) < 0.001f)
            {
                _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                SoundManager.instance.Play(SoundManager.SoundName.JumpEffect); //Play Sound: JumpEffect
            }
        }
    }

    // Combat Method ---------------------------------------------------------------------------------------------------
    // ReSharper disable Unity.PerformanceAnalysis
    private void Combat()
    {
        // NORMAL ATTACK
        if (!Input.GetKeyDown(KeyCode.J)) return;
        // If current mana < 10
        if (GameplayManager.instance.currentMana < 10)
        {
            // Play Sound: Alert
            SoundManager.instance.Play(SoundManager.SoundName.Alert);
            // Display alert on the screen 1 second
            StartCoroutine(FindObjectOfType<GameplayManager>().SendAlert("Not enough mana", 1));
            // Debug on console: Not enough mana
            Debug.Log("Not enough mana");
        }
        // If reduces 10 mana
        else if (GameplayManager.instance.ReduceMana(10))
        {
            SoundManager.instance.Play(SoundManager.SoundName.ThrowEffect); // Play Sound: ThrowEffect
            Instantiate(projectilePrefab, launchOffset.position, transform.rotation); // Throw a knife
        }
    }
}