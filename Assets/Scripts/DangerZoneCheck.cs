using UnityEngine;

public class DangerZoneCheck : MonoBehaviour
{
    /*YOU SHOULD CREATE NEW OBJECT "DANGER ZONE" THAT HAVE "BOX COLLIDER 2D" TO DETECT PLAYER
     AND PUT THIS SCRIPT TO IT*/
    public string motionAttack; // Type name of motion attack (YOU CAN CHECK NAME OF IT ON THE ANIMATOR PAGE)
    
    private bool _inRange; // Variable True/False : In range / Out of range
    private EnemyBehavior _enemy; // Link with EnemyBehavior script
    private Animator _animator; // Link with animator of parent object

    // Awake, Update Method --------------------------------------------------------------------------------------------
    // Awake is called before the first frame update
    private void Awake()
    {
        _enemy = GetComponentInParent<EnemyBehavior>(); // Return component to do something in EnemyBehavior script
        _animator = GetComponentInParent<Animator>(); // Return component to do something in animator
    }

    // Update is called once per frame
    private void Update()
    {
        if (_inRange && !_animator.GetCurrentAnimatorStateInfo(0).IsName(motionAttack))
        { _enemy.Flip(); }
    }

    // On Trigger 2D (Enter & Exit) Method -----------------------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player")) // If any game object with tag "Player" COME IN SIDE!
        { _inRange = true; }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player")) // If any game object with tag "Player" GET OUT SIDE!
        {
            _inRange = false; // _inRange of danger zone (SET TO BE INACTIVE)
            gameObject.SetActive(false);
            _enemy.triggerArea.SetActive(true);
            _enemy.inRange = false;
            _enemy.SelectTarget();
        }
    }
}