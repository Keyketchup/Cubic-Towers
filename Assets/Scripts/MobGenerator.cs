using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobGenerator : MonoBehaviour
{
    public static MobGenerator singleton { get; private set; } = null;

    public float spawnDelay = 0.8f;
    public int starterMobs = 3;
    public int mobMultiplier = 2;
    public GameObject basicMobPrefab;

    private int currentWave = 0;
    private int numberOfMobs = 0;
    bool startedGen = false;

    void GenerateMob()
    {

        currentWave++;
        int mobCount = starterMobs + currentWave * mobMultiplier;
        StartCoroutine(SpawnMob(mobCount));

    }

    bool CheckWave()
    {
        return numberOfMobs <= 0;
    }

    public void whenMobDead()
    {
        numberOfMobs--;
        Debug.Log("Mob Dead");
    }

    private IEnumerator SpawnMob(int amount)
    {
        WaitForSeconds delay = new WaitForSeconds(spawnDelay);
        for (var i = 0; i < amount; i++)
        {
            yield return delay;
            numberOfMobs++;
            GameObject newMob = Instantiate(basicMobPrefab, MobGenerator.singleton.transform);
            newMob.transform.position = MapGenerator.singleton.startTile.transform.position;
        }
        startedGen = true;
        yield break;
    }

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public int getNumberOfMobs()
    {
        return numberOfMobs;
    }

    private void Start()
    {
        GenerateMob();
    }

    private void Update()
    {
        if(CheckWave())
        {
            if (startedGen)
            {
                startedGen = false;
                GenerateMob();
            }
        }
    }

}