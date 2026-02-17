using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Keyboard Input")]
    //public KeyCode moveLeftKey = KeyCode.A;
    //public KeyCode moveRightKey = KeyCode.D;

    [Header("XR Input")]
    public InputActionReference moveLeftAction;   // X button Left Controller
    public InputActionReference moveRightAction;  // A button Right Controller


    [Header("Movement Settings")]
    public float sideSpeed = 5f;
    public float laneLimit = 4f;
    public float turnAngle = 30f;
    public float turnSmooth = 5f;

    [Header("Effects")]
    public ParticleSystem explosionEffect;

    bool m_IsControllable = true;

    public bool moveLeftState = false;
    public bool moveRightState = false;

    public bool gameOver = false;




    void Update()
    {
        if (!m_IsControllable || gameOver) return;

        if (moveLeftAction != null)
            moveLeftState = moveLeftAction.action.IsPressed();

        if (moveRightAction != null)
            moveRightState = moveRightAction.action.IsPressed();

        // Apply states
        if (moveLeftState)
        {
            ApplyMove(-1f);
        }
        else if (moveRightState)
        {
            ApplyMove(1f);
        }
        else
        {
            ApplyIdle();
        }

    }





    void ApplyMove(float direction)
    {
        Vector3 pos = transform.position;
        pos.x += direction * sideSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -laneLimit, laneLimit);
        transform.position = pos;

        float targetY = direction * turnAngle;
        Vector3 rotation = transform.eulerAngles;
        rotation.y = Mathf.LerpAngle(rotation.y, targetY, Time.deltaTime * turnSmooth);
        transform.eulerAngles = rotation;
    }

    void ApplyIdle()
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.y = Mathf.LerpAngle(rotation.y, 0f, Time.deltaTime * turnSmooth);
        transform.eulerAngles = rotation;
    }

    public void EnableControl()
    {
        m_IsControllable = true;
    }
    public WebSocketGameController wsController;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("crate") ||
            collision.gameObject.CompareTag("prop"))
        {
            gameOver = true;
            wsController.GameOver();

            explosionEffect.transform.position = transform.position;
            explosionEffect.Play();
        }
    }
}
