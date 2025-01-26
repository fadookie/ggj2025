using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BubbleController: MonoBehaviour
{
    [SerializeField] private Collider2D playArea;
    
    private Vector3 _startingPosition;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _startingPosition = transform.position;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var impulse = ComputeTotalImpulse(collision);
        Debug.Log($"{nameof(BubbleController)}#{nameof(OnCollisionEnter2D)} impulse:{impulse}");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == playArea && Application.isPlaying)
        {
            StartCoroutine(ResetToStartPosition());
        }
    }

    private IEnumerator ResetToStartPosition()
    {
        yield return new WaitForSeconds(2f);
        transform.position = _startingPosition;
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.angularVelocity = 0f;
    }

    private static Vector2 ComputeTotalImpulse(Collision2D collision) {
        var impulse = Vector2.zero;

        var contactCount = collision.contactCount;
        for(var i = 0; i < contactCount; i++) {
            var contact = collision.GetContact(i);
            impulse += contact.normal * contact.normalImpulse;
            impulse.x += contact.tangentImpulse * contact.normal.y;
            impulse.y -= contact.tangentImpulse * contact.normal.x;
        }

        return impulse;
    }
}