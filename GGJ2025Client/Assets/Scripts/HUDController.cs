using System;
using UnityEngine;
using UniRx;

public class HUDController: MonoBehaviour
{
    private static HUDController _instance;
    public static HUDController Instance => _instance;

    [SerializeField] private UnityEngine.UI.Text scoreLabel;
    
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
        GameController.Instance.Score
            .TakeUntilDestroy(this)
            .SubscribeToText(scoreLabel, score => $"Score: {score}")
            .AddTo(this);
    }
}