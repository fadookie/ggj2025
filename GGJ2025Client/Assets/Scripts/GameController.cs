using System;
using UnityEngine;

public class GameController: MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance => _instance;

    [SerializeField] private Collider2D playArea;
    public Collider2D PlayArea => playArea;
    
    void Awake()
    {
        // Singleton setup
        if (_instance != null) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        _instance = this;
    }

    private void Start()
    {
        BubbleSpawnArea.Instance.SpawnBubbleImmediate();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyMap.FullscreenKey))
        {
            Debug.LogWarning($"GameControllerFullscreen toggle, current:{Screen.fullScreen}");
            Screen.fullScreen = !Screen.fullScreen; 
        }
    }
}