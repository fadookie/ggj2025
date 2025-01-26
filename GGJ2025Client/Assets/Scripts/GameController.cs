using System;
using UnityEngine;

public class GameController: MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance => instance;
    
    void Awake()
    {
        // Singleton setup;
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        instance = this;
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