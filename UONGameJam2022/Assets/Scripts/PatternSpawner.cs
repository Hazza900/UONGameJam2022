using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternSpawner : MonoBehaviour
{


    [SerializeField]
    private Transform _spawnLeft;

    [SerializeField]
    private Transform _spawnCenter;

    [SerializeField]
    private Transform _spawnRight;


    [SerializeField]
    private GameObject _obstacle;

    [SerializeField]
    private bool _spawnUp = false;


    [SerializeField]
    private bool _spawnDown = true;

    [SerializeField]
    private float _spawnDelay = 0.5f;

    private float _obstacleSpeedDiff = 0;

    [SerializeField]
    private Pattern[] _patterns = new Pattern[]{
        new Pattern(new int[] {1, 2, 4, 3, 5}),
        new Pattern(new int[] {6, 5, 3, 5, 6}),
        new Pattern(new int[] {5, 4, 2, 1, 6})
    };




    /*

    pattern explanation
    ***** 0 - empty
    ***** x - obstacle

    0 - 0 0 0
    1 - 0 0 x
    2 - 0 x 0
    3 - 0 x x
    4 - x 0 0 
    5 - x 0 x
    6 - x x 0
    */


    public static PatternSpawner Instance = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        StartSpawn();
    }

    public void StartSpawn()
    {
        StartCoroutine(SpawnLoop());
    }

    public void AdjustSpawnDelay(float value)
    {
        // can be subtraction as default, but this kinda makes more sense, just need to pass negative value for it to decrease spawn delay
        _spawnDelay += value;
    }

    public void SetSpawn(bool spawnUp, bool spawnDown)
    {
        _spawnUp = spawnUp;
        _spawnDown = spawnDown;
    }


    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            var wait = new WaitForSeconds(_spawnDelay);

            var pattern = _patterns[Random.Range(0, _patterns.Length)].data;

            int index = 0;
            while (index < pattern.Length)
            {
                yield return wait;
                // counting first sapwner as most left one
                // 3rd spawner would be right most one

                if ((pattern[index] & 4) > 0) Spawn(_spawnLeft);
                if ((pattern[index] & 2) > 0) Spawn(_spawnCenter);
                if ((pattern[index] & 1) > 0) Spawn(_spawnRight);


                index++;

            }
            // have one cycle of rest random?
            if (Random.Range(0, 3) == 2)
            {
                yield return wait;
            }

            _spawnDelay += 0.02f;
            _obstacleSpeedDiff += 1f;
        }
    }

    private void Spawn(Transform spawnerPosition)
    {
        if (_spawnDown)
        {
            var obstacle = Instantiate(_obstacle, transform);
            obstacle.GetComponent<Obstacle>().AdjustSpeed(_obstacleSpeedDiff);
            obstacle.transform.position = spawnerPosition.position;

        }
        if (_spawnUp)
        {
            var obstacle = Instantiate(_obstacle, transform);
            obstacle.GetComponent<Obstacle>().AdjustSpeed(_obstacleSpeedDiff);
            obstacle.GetComponent<Obstacle>().SetDirectionUp();
            obstacle.transform.position = spawnerPosition.position;
        }

    }


}

[System.Serializable]
public class Pattern
{
    public int[] data;

    public Pattern(int[] input)
    {
        data = input;
    }
}
