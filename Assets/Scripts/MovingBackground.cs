using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    /*YOU HAVE TO PUT THIS SCRIPT TO EVERY BACKGROUND THAT YOU WANT TO MAKE PARALLAX EFFECT*/
    [SerializeField] private float parallaxEffect; // Parallax effect speed
    private float _startPosition;
    private GameObject _camera;

    // Start is called before the first frame update
    private void Start()
    {
        _camera = GameObject.Find("CM Follow Player Camera");
        _startPosition = transform.position.x;
    }

    // Update is called once per frame
    private void Update()
    {
        float distance = (_camera.transform.position.x * parallaxEffect);
        transform.position = new Vector3(_startPosition + distance, transform.position.y, transform.position.z);
    }
}