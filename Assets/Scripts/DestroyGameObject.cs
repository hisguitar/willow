using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    /*YOU HAVE TO PUT THIS SCRIPT TO ANY OBJECT THAT YOU WANT TO DESTROY IN 0.3 SECOND*/
    private void Awake()
    { Destroy(gameObject, 0.3f); }
}