using UnityEngine;

public class Rotacion : MonoBehaviour
{

    [SerializeField]
    private float velocityX;

    [SerializeField]
    private float velocityY;

    [SerializeField]
    private float velocityZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(velocityX, velocityY, velocityZ);  
    }
}
