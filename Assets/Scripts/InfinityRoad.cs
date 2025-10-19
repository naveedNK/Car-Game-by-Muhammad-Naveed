using UnityEngine;

public class InfinityRoad : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatLength;

    void Start()
    {
        // Get the starting position of the road
        startPos = transform.position;

        // Half the length of the road to determine when to reset
        repeatLength = GetComponent<BoxCollider>().size.x / 2f;
    }

    void Update()
    {
        // If the road has moved back half its length, reset to start position
        if (transform.position.x < startPos.x - repeatLength)
        {
            transform.position = startPos;
        }
    }
}
