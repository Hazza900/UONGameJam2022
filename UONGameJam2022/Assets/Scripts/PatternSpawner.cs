using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternSpawner : MonoBehaviour
{

    [SerializeField]
    private float[][] _patterns = {{1,2,4,3,5}, {6, 5, 3, 5, 6}, {5, 4, 2, 1, 6}};

    [SerializeField]
    private float _spawnDelay = 0.7f;
    /*


    idea?    always insert 0 - layout at the start of pattern?
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


    // not sure how spawning works for now, but I thought we'd have 3 spawners,
    // so I'll have it in a way that it returns array of 3 bools, one for each lane to determine if an obstacle needs to be spawned
    // maybe have a reference to 3 spawners that can be called individually and call them from here

    public static PatternSpawner Instance = null;

    private void Awake(){
        if(Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartSpawn(){
        StartCoroutine(Spawn());
    }

    public void AdjustSpawnDelay(float value){
        // can be subtraction as default, but this kinda makes more sense, just need to pass negative value for it to decrease spawn delay
        _spawnDelay += value;
    }


    private IEnumerator Spawn(){
        var wait = new WaitForSeconds(_spawnDelay);
        var pattern = _patterns[Random.Range(0, _patterns.length)];

        int index = 0;
        while(index < pattern.length){
            yield return wait;
            // counting first sapwner as most left one
            // 3rd spawner would be right most one

            //  FirstSpawner.Spawn(pattern[index] & 4 > 0); // returns true if needs to spawn, false if not
            //  SecondSpawner.Spawn(pattern[index] & 2 > 0); // returns true if needs to spawn, false if not
            //  ThirdSpawner.Spawn(pattern[index] & 1 > 0); // returns true if needs to spawn, false if not
            
            index++;

        }
    

    }







}
