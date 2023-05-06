using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{
    /*YOU HAVE TO CREATE NEW OBJECT CALLED "MONSTER" THEN ADD "RIGIDBODY 2D, SPRITE RENDERER, ANIMATOR" COMPONENT
     AND PUT THIS SCRIPT TO IT
     THEN YOU HAVE TO CREATE CHILDREN OBJECT..
     - MONSTER COLLIDER THAT HAVE "BOX COLLIDER 2D"
     - OBSTACLE CHECK THAT HAVE "BOX COLLIDER 2D" FOR DETECT OBSTACLE
     - TRIGGER AREA THAT HAVE "BOX COLLIDER 2D" FOR DETECT PLAYER 
     - DANGER ZONE THAT HAVE "BOX COLLIDER 2D" FOR DETECT PLAYER IN HIGHER RANGE
     - COMBAT POPUPS THAT HAVE "CANVAS, CANVAS SCALER, GRAPHIC RAYCASTER"
       AND HAVE CHILDREN OBJECT IS "ENEMY NAME, HEALTH BAR, CURRENT HEALTH BAR"
     - HIT BOX THAT HAVE "BOX COLLIDER 2D" TO DETECT PLAYER AND DEAL DAMAGE
     - LIGHT 2D FOR BEAUTIFUL MONSTER!*/
    #region Public Variables
    [Header("Status Variables")]
    public Image currentHealthBar;
    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;
    public float jumpForce;
    [Header("Combat Variables")]
    [SerializeField] private GameObject bleedingEffect;
    public GameObject combatPopups; // Interface of enemy (Health bar, Enemy name)
    public float enemyPower;
    public float attackDistance; // Minimum distance to attack
    public float timer; // Timer is cooldown between attacks
    public string motionAttack; // Type name of motion attack (YOU CAN CHECK NAME OF IT ON THE ANIMATOR PAGE.)
    [Header("Zone Variables")]
    public Transform leftLimit; // Scope of patrol (Left point)
    public Transform rightLimit; // Scope of patrol (Right point)
    [HideInInspector] public Transform target; // Target is everything that enemy move to. (Like Player, Patrol point)
    [HideInInspector] public bool inRange; // Check if player is in range or not
    public GameObject triggerArea; // Find player in this area
    public GameObject dangerZone; // Combat area
    #endregion

    #region Private Variables
    private Animator _animator; // Link with animator node of enemy
    private float _distance; // Store the distance between enemy and player
    private bool _attackMode;
    private bool _cooling; // Check if enemy is cooling after attack
    private float _intTimer; // _intTimer is current time of cooldown
    private static readonly int Attack1 = Animator.StringToHash("Attack");
    private static readonly int CanWalk = Animator.StringToHash("canWalk");
    #endregion

    // Awake, Update Methods -------------------------------------------------------------------------------------------
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        SelectTarget();
        _intTimer = timer; // Store the initial value of timer
        _animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    private void Update()
    {
        // Health conditions
        if (currentHealth <= 0) // If enemy's dead
        {
            GameplayManager.instance.levelUp = true;
            GameplayManager.instance.currentLevel++;
            combatPopups.SetActive(false); // Set combatPopups to be false
            Destroy(gameObject); // Destroy this game object
        }
        
        // If attack mode = false, let the enemy walk around and patrol
        if (!_attackMode)
        { Move(); }

        if (!InsideOfLimits() && !inRange && !_animator.GetCurrentAnimatorStateInfo(0).IsName(motionAttack))
        { SelectTarget(); }

        // Use enemy logic when inRange = true
        if (inRange)
        { EnemyLogic(); }
    }
    
    // Enemy Logic (Attack or not) Method ------------------------------------------------------------------------------
    private void EnemyLogic()
    {
        _distance = Vector2.Distance(transform.position, target.position);
        
        if (_distance > attackDistance) // If distance between enemy and player > attack distance
        { StopAttack();}
        else if (attackDistance >= _distance && _cooling == false) // If distance between enemy and player <= attack distance & cooldown is false
        { Attack(); }

        if (_cooling) // If cooling is true
        {
            Cooldown(); // It's mean enemy need to wait for cooldown
            _animator.SetBool(Attack1, false); // Set attack animation to be false!
        }
    }
    
    // Move Method -----------------------------------------------------------------------------------------------------
    private void Move()
    {
        _animator.SetBool(CanWalk, true); // Play walk animation of enemy
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName(motionAttack)) // If the attack animation of enemy is not playing
        {
            var position = transform.position;
            
            // Change position of enemy to target position (chase player)
            Vector2 targetPosition = new Vector2(target.position.x, position.y);
            // Change the position of the enemy to move forward
            position = Vector2.MoveTowards(position, targetPosition, moveSpeed * Time.deltaTime);
            transform.position = position;
        }
    }
    
    // check scope of patrol Method ------------------------------------------------------------------------------------
    private bool InsideOfLimits()
    {
        var position = transform.position;
        return position.x > leftLimit.position.x && position.x < rightLimit.position.x;
    }
    
    // Check where enemy should go while patrolling -------------------------------------------------------------------
    public void SelectTarget() // Set to be public because i need to call in other script (DangerZoneCheck Script)
    {
        var position = transform.position;
        float distanceToLeft = Vector2.Distance(position, leftLimit.position);
        float distanceToRight = Vector2.Distance(position, rightLimit.position);

        if (distanceToLeft > distanceToRight) // If distance to left > distance to right, walk to the left.
        { target = leftLimit; }
        else // In other cases, walk to the right
        { target = rightLimit; }
        
        Flip(); // Flip when target is behind (target is not player, it's mean where enemy should go)
    }
    
    // Flip method -----------------------------------------------------------------------------------------------------
    public void Flip() // Set to be public because i need to call in other script (DangerZoneCheck Script)
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        { rotation.y = 180f; }
        else
        { rotation.y = 0f; }
        transform.eulerAngles = rotation;
    }
    
    // Jump over obstacles ---------------------------------------------------------------------------------------------
    public void CrossOver() // // Set to be public because i need to call in other script (DangerZoneCheck Script)
    {
        // Play jump animation
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce); 
    }
    
    // Attack Method ---------------------------------------------------------------------------------------------------
    private void Attack()
    {
        timer = _intTimer; // Reset timer when player enter attack range
        _attackMode = true; // To check if enemy can attack or not
        
        _animator.SetBool(CanWalk, false); // Stop walk animation
        _animator.SetBool(Attack1, true); // Play attack animation
    }
    
    public void DamagePlayer()
    {
        if (_attackMode != true) return;
        Instantiate(bleedingEffect, GameObject.Find("Player Collider").transform.position, Quaternion.identity); // Bleeding effect
        GameplayManager.instance.TakeDamage(enemyPower);
    }
    
    // Start cooldown Method -------------------------------------------------------------------------------------------
    public void TriggerCooling()
    { _cooling = true; }
    
    // Cooldown Method -------------------------------------------------------------------------------------------------
    private void Cooldown()
    {
        timer -= Time.deltaTime; // Countdown time of cooldown

        if (!(timer <= 0) || !_cooling || !_attackMode) return;
        _cooling = false;
        timer = _intTimer;
    }

    // Stop Attack Method ----------------------------------------------------------------------------------------------
    private void StopAttack()
    {
        _cooling = false;
        _attackMode = false;
        _animator.SetBool(Attack1, false);
    }

    // TakeDamage Method ------------------------------------------------------------------------------------------------
    public void TakeDamage(float damage)
    {
        combatPopups.SetActive(true); // Set combatPopups to be false when enemy's dead
        currentHealth -= damage;
        currentHealthBar.fillAmount = currentHealth / maxHealth;
    }
    
    // Slash effect Method ---------------------------------------------------------------------------------------------
    public void SlashEffect()
    { SoundManager.instance.Play(SoundManager.SoundName.SlashEffect); } // Play Sound: SlashEffect
}