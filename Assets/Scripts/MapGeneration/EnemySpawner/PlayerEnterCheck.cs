using UnityEngine;
using Pathfinding;

public class PlayerEnterCheck : MonoBehaviour
{
    private EnemySpawner _eSpawner;
    private EnemySpawnPoint[] _eSpawnPoints;
    private bool _isSpawned;
    private RoomType roomType;
    
    private void Start()
    {
        _eSpawnPoints = GetComponentsInChildren<EnemySpawnPoint>();
        _eSpawner = GetComponentInChildren<EnemySpawner>();
        _isSpawned = false;
        roomType = GetComponentInParent<PhysicalRoom>().roomType;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) 
            return;
        
        if (_isSpawned) 
            return;
        
        ScanArea();
        var playerLevel = collision.GetComponent<PlayerStat>().Level;
        _eSpawner.SetPlayerLevel(playerLevel);

        int enemyLevel = _eSpawner.GetEnemyLevel();
        for (int i = 0; i < _eSpawnPoints.Length; i++)
            _eSpawnPoints[i].SpawnEnemy(_eSpawner.GetEnemy(roomType), enemyLevel);
        _isSpawned = true;
        DungeonMusic.Instance.TurnTheCombatMusic();
        Destroy(gameObject);
        
    }

    public void ScanArea()
    {
        var graph = AstarPath.active.data.gridGraph;
        graph.center = transform.position;
        graph.UpdateTransform();
        graph.Scan();
    }
}
