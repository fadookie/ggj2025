using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class HeadController : MonoBehaviour
{
    private Renderer _renderer;
    [SerializeField] private Renderer _eyeRenderer;
    
    private void Start()
    {
        _renderer = gameObject.GetComponent<Renderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyMap.ShowHeadKey))
        {
            _renderer.enabled = !_renderer.enabled;
            _eyeRenderer.enabled = _renderer.enabled;
        }
    }
}