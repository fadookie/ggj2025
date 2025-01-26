using System;
using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MeshRenderer))]
public class BubbleController: MonoBehaviour
{
    [SerializeField] private ParticleSystem burstParticle;
    [SerializeField] private Collider2D playArea;
    [SerializeField] private Collider2D[] leftArmHostileColliders;
    [SerializeField] private Collider2D[] rightArmHostileColliders;

    private bool isLeftArmHostileTouching = false;
    private bool isRightArmHostileTouching = false;
    
    private Vector3 _startingPosition;
    private Rigidbody2D _rigidbody2D;
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _startingPosition = transform.position;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyMap.DebugPopKey))
        {
            // TODO: Make sure this doesn't count for score if we allow the user to use this key
            Debug.LogWarning("DEBUG BubblePop");
            StartCoroutine(BubblePop());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var impulse = ComputeTotalImpulse(collision);
        Debug.Log($"{nameof(BubbleController)}#{nameof(OnCollisionEnter2D)} other:{collision.gameObject.name} impulse:{impulse}");
        if (leftArmHostileColliders.Contains(collision.collider))
        {
            isLeftArmHostileTouching = true;
        }
        if (rightArmHostileColliders.Contains(collision.collider))
        {
            isRightArmHostileTouching = true;
        }

        if (isLeftArmHostileTouching && isRightArmHostileTouching)
        {
            Debug.LogWarning("BOTH ARMS ARE TOUCHING!");
            StartCoroutine(BubblePop());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log($"{nameof(BubbleController)}#{nameof(OnCollisionExit2D)} other:{collision.gameObject.name}");
        if (leftArmHostileColliders.Contains(collision.collider))
        {
            isLeftArmHostileTouching = false;
        }
        if (rightArmHostileColliders.Contains(collision.collider))
        {
            isRightArmHostileTouching = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == playArea && Application.isPlaying)
        {
            StartCoroutine(ResetToStartPositionCo());
        }
    }

    private IEnumerator BubblePop()
    {
        _meshRenderer.enabled = false;
        burstParticle.Play();
        AudioManager.Instance.PlaySound(AudioManager.Sound.BubblePop);
        yield return new WaitForSeconds(2f);
        ResetToStartPosition();
        _meshRenderer.enabled = true;
    }

    private IEnumerator ResetToStartPositionCo()
    {
        yield return new WaitForSeconds(2f);
        ResetToStartPosition();
    }
    
    private void ResetToStartPosition()
    {
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