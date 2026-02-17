using UnityEngine;
using NativeWebSocket;
using UnityEngine.SceneManagement;
using System;

public class WebSocketGameController : MonoBehaviour
{
    private WebSocket websocket;
    public string serverIP = "10.204.0.65"; // Your server IP
    public int serverPort = 8081;

    private bool isGameOver = false;

    async void Start()
    {
        websocket = new WebSocket($"ws://{serverIP}:{serverPort}/");

        websocket.OnOpen += async () =>
        {
            Debug.Log("Connected to WebSocket server");
            string UUID = SystemInfo.deviceUniqueIdentifier;
            await websocket.SendText("Device (Unity):" + SystemInfo.deviceName + " ... UUID:" + UUID);
        };

        websocket.OnMessage += (bytes) =>
        {
            string message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("Received: " + message);
            IncomingMessageParser(message);
        };

        websocket.OnClose += (code) =>
        {
            Debug.Log("WebSocket closed");
        };

        await websocket.Connect();
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        if (websocket != null)
            websocket.DispatchMessageQueue();
#endif
    }

    // Call this when the car crashes
    public async void GameOver()
    {
        if (websocket != null && websocket.State == WebSocketState.Open)
        {
            await websocket.SendText("RED_ON");
            Debug.Log("Sent: RED_ON");
        }

        isGameOver = true;
        Time.timeScale = 0f; // optional pause
    }

    async void RestartGame()
    {
        // Turn off LED if connected
        if (websocket != null && websocket.State == WebSocketState.Open)
        {
            await websocket.SendText("RED_OFF");
        }

        // Reset timescale before reloading scene
        Time.timeScale = 1f;

        // Reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



    void Awake()
    {
        DontDestroyOnLoad(gameObject); // WebSocket persists across scene reloads
    }


    // Incoming messages from ESP32
    void IncomingMessageParser(string msg)
    {
        string valueParsed = msg.Substring(msg.IndexOf(":") + 1);

        // ESP32 push button triggers game restart
        if (msg.Contains("RESTART") && isGameOver)
        {
            Debug.Log("ESP32 Button Pressed: Restarting Game");
            RestartGame();
        }
    }
}
