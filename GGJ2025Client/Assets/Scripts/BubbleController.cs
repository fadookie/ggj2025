using System;
using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MeshRenderer))]
public class BubbleController: MonoBehaviour
{
    [SerializeField] private ParticleSystem burstParticle;
    private Collider2D[] _leftArmHostileColliders;
    private Collider2D[] _rightArmHostileColliders;

    private bool _isLeftArmHostileTouching = false;
    private bool _isRightArmHostileTouching = false;
    
    // private Rigidbody2D _rigidbody2D;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        // _rigidbody2D = GetComponent<Rigidbody2D>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _leftArmHostileColliders = ArmController.LeftArmControllerInstance.HostileTargets.ToArray();
        _rightArmHostileColliders = ArmController.RightArmControllerInstance.HostileTargets.ToArray();
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
        // Debug.Log($"{nameof(BubbleController)}#{nameof(OnCollisionEnter2D)} other:{collision.gameObject.name} impulse:{impulse}");
        if (_leftArmHostileColliders.Contains(collision.collider))
        {
            _isLeftArmHostileTouching = true;
        }
        if (_rightArmHostileColliders.Contains(collision.collider))
        {
            _isRightArmHostileTouching = true;
        }

        if (_isLeftArmHostileTouching && _isRightArmHostileTouching)
        {
            Debug.LogWarning("BOTH ARMS ARE TOUCHING!");
            StartCoroutine(BubblePop());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log($"{nameof(BubbleController)}#{nameof(OnCollisionExit2D)} other:{collision.gameObject.name}");
        if (_leftArmHostileColliders.Contains(collision.collider))
        {
            _isLeftArmHostileTouching = false;
        }
        if (_rightArmHostileColliders.Contains(collision.collider))
        {
            _isRightArmHostileTouching = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
		// Debug.LogWarning($"OnTriggerExit2D other:{other.gameObject.name}");
        if (other == GameController.Instance.PlayArea && Application.isPlaying)
        {
            StartCoroutine(DieCo());
        }
    }

    private IEnumerator BubblePop()
    {
        _meshRenderer.enabled = false;
        burstParticle.Play();
        AudioManager.Instance.PlaySound(AudioManager.Sound.BubblePop);
        yield return new WaitUntil(() => !burstParticle.isPlaying);
        Die();
    }

    private IEnumerator DieCo()
    {
		// Debug.LogWarning($"DieCo");
        yield return new WaitForSeconds(2f);
        Die();
    }
    
    private void Die()
    {
		Debug.LogWarning($"Die");
        BubbleSpawnArea.Instance.QueueBubbleSpawn();
        Destroy(this.gameObject);
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