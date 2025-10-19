using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    public float speed = 70f;
    private PlayerController playerControllerScript;
    private float lowerBound = -20f; // not essential for road but fine for other objects

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            // Move the road backward (along Z axis)
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }

        // Optional destroy condition for non-road objects
        if (transform.position.x < lowerBound && (gameObject.CompareTag("crate") || gameObject.CompareTag("prop")))
        {
            Destroy(gameObject);
        }
    }
}
