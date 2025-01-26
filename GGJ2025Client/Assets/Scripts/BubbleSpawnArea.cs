using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class BubbleSpawnArea : MonoBehaviour {
    private static BubbleSpawnArea _instance;
    public static BubbleSpawnArea Instance => _instance;
    
	[SerializeField] private BubbleController spawnedPrefab;
	[SerializeField] private float spawnDelay = 2f;

	private BoxCollider2D _spawnArea;
	private Vector2 _maxSpawnPos;

	// Use this for initialization
	private void Awake () {
        // Singleton setup
        if (_instance != null) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        _instance = this;
        
		_spawnArea = GetComponent<BoxCollider2D>();
		_spawnArea.enabled = false; //We don't need this to test for any collisions, just to show visual bounds info in the editor.
		var size = _spawnArea.size;
		_maxSpawnPos = new Vector2(size.x / 2, size.y / 2);
	}

	public void QueueBubbleSpawn()
	{
		Debug.Log($"QueueBubbleSpawn");
		StartCoroutine(QueueSpawnCo());
	}

	private IEnumerator QueueSpawnCo()
	{
		yield return new WaitForSeconds(spawnDelay);
		SpawnBubbleImmediate();
	}

	public void SpawnBubbleImmediate()
	{
		// Debug.LogWarning($"SpawnBubbleImmediate");
		var spawned = Instantiate(spawnedPrefab.gameObject, Vector3.zero, Quaternion.identity);
		spawned.transform.parent = transform;
		var pos = new Vector3(Random.Range(-_maxSpawnPos.x, _maxSpawnPos.x), Random.Range(-_maxSpawnPos.y, _maxSpawnPos.y), 0);
		spawned.transform.localPosition = pos;
	}
}
