using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;

    void Start()
    {
        // Keep the initial offset between camera and player
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Follow only the player's Z position (vertical/top-down movement)
        transform.position = new Vector3(
            transform.position.x,            // keep camera X fixed
            transform.position.y,            // keep camera Y fixed
            player.position.z + offset.z     // follow player Z movement
        );
    }
}
