using UnityEngine;


public class MoveBackward : MonoBehaviour
{
    public float roadSpeed = 100f;
    private PlayerController roadControllerScript;
    private float lowerBoundValue = -20f; // not essential for road but fine for other objects


    void Start()
    {
        roadControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!roadControllerScript.gameOver)
        {
            // Move the road backward (along Z axis)
            transform.Translate(Vector3.right * roadSpeed * Time.deltaTime);
        }

        // Optional destroy condition for non-road objects
        if (transform.position.x < lowerBoundValue && (gameObject.CompareTag("crate") || gameObject.CompareTag("prop")))
        {
            Destroy(gameObject);
        }
    }
}
