using System.Linq;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    private EnemyList _enemyList;
    private EnemySpawnPoint[] _spawnPoints;
    private readonly int[] _excludedInts = 
    {
        -3, 
        -2, 
        -1, 
        0
    };
    private int _playerLevel;
    
    [Range(0, 3)] public int levelRange;
    [Space]
    [Range(0f, 1f)] public float firstTierSpawnProbability;
    [Range(0f, 1f)] public float secondTierSpawnProbability;
    [Range(0f, 1f)] public float thirdTierSpawnProbability;
    [Space]
    public int NumToUnlockSecondTier = 2;
    public int NumToUnlockThirdTier = 6;

    private static int ClearedRooms = 0;
    private bool _blockSecondTier = true;
    private bool _blockThirdTier = true;

    private void Start()
    {
        _enemyList = GetComponentInChildren<EnemyList>();
        _spawnPoints = GetComponents<EnemySpawnPoint>();
    }


    public Transform GetEnemy(RoomType roomType)
    {
        if(roomType == RoomType.Boss)
        {
            return _enemyList.GenerateBoss();
        }

        ClearedRooms++;
        UpdateTiersBlocks();
        var sum = firstTierSpawnProbability + secondTierSpawnProbability + thirdTierSpawnProbability; 
        var rand = Random.Range(0f, sum);
        
        var firstInterval = firstTierSpawnProbability;
        var secondInterval = firstTierSpawnProbability + secondTierSpawnProbability;
        var thirdInterval = firstTierSpawnProbability + secondTierSpawnProbability + thirdTierSpawnProbability;
        
        if (0 <= rand && rand < firstInterval)
            return _enemyList.GenerateFirstTierEnemy();
        
        if (firstInterval <= rand && rand < secondInterval && !_blockSecondTier)
            return _enemyList.GenerateSecondTierEnemy();

        if (secondInterval <= rand && rand <= thirdInterval && !_blockThirdTier)
            return _enemyList.GenerateThirdTierEnemy();
        else
            return _enemyList.GenerateFirstTierEnemy();
    }

    public int GetEnemyLevel()
    {
        return RandomFromRangeWithExceptions(_playerLevel - levelRange, 
                                             levelRange * 2, 
                                                      _excludedInts);
    }

    public void SetPlayerLevel(int level)
    {
        _playerLevel = level;
    }

    private int RandomFromRangeWithExceptions(int rangeMin, int rangeMax, params int[] exclude)
    {
        var enumerable = Enumerable.Range(rangeMin, rangeMax).Where(i => !exclude.Contains(i));
        var range = enumerable as int[] ?? enumerable.ToArray();
        
        var index = Random.Range(0, range.Length);
        return range.ElementAt(index);
    }

    //It is made to make spawning enemies with proggression of the player:
    private void UpdateTiersBlocks()
    {
        if (ClearedRooms > NumToUnlockSecondTier)
            _blockSecondTier = false;
        if (ClearedRooms > NumToUnlockThirdTier)
            _blockThirdTier = false;
    }
}