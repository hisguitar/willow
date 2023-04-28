using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    /*YOU HAVE TO CREATE NEW OBJECT THEN ADD "BOX COLLIDER 2D" COMPONENT
     AND PUT THIS SCRIPT TO IT
     DON'T FOR GET TO CREATE OBJECT "DIRECTION POINT"*/
    public float speed; // Speed of the platform
    public int startingPoint; // Starting index (position of the platform)
    public Transform[] points; // An array of transform points (positions where the platform needs to move)

    private int _i; // Index of the array

    private void Start()
    { transform.position = points[startingPoint].position; } // Setting the position of the platform to the position of one of the points using index "startingPoint"

    private void Update()
    {
        // Checking the distance of the platform and the point
        if (Vector2.Distance(transform.position, points[_i].position) < 0.02f)
        {
            _i++; // Increase the index
            if (_i == points.Length) // Check if the platform was on the last point after the index increase
            { _i = 0; } // Reset the index
        }
        
        // Moving the platform to the point position with the index "i"
        transform.position = Vector2.MoveTowards(transform.position, points[_i].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    { col.transform.SetParent(transform); }

    private void OnCollisionExit2D(Collision2D col)
    { col.transform.SetParent(null); }
}