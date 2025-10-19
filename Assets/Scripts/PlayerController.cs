using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float sideSpeed = 5f;       // Speed of left/right movement
    public float laneLimit = 4f;       // Boundary on both sides
    public float turnAngle = 30f;      // Maximum rotation when turning
    public float turnSmooth = 5f;      // Smoothness of turning
    public ParticleSystem explosionEffect;
    public bool gameOver;

    void Update()
    {
        if (gameOver) return;

        // Get horizontal input (-1 for left, +1 for right)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Move car left or right smoothly
        Vector3 pos = transform.position;
        pos.x += horizontalInput * sideSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -laneLimit, laneLimit);
        transform.position = pos;

        // Tilt car left/right for visual effect
        float targetY = horizontalInput * turnAngle;
        Vector3 rotation = transform.eulerAngles;
        rotation.y = Mathf.LerpAngle(rotation.y, targetY, Time.deltaTime * turnSmooth);
        transform.eulerAngles = rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("crate") || collision.gameObject.CompareTag("prop"))
        {
            gameOver = true;
            explosionEffect.transform.position = transform.position;
            explosionEffect.Play();
        }
    }
}
