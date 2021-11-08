using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pathfinding;

public class SpawnManager : MonoBehaviour
{   
    [Header("Prefabs")]
    public GameObject enemySoldierPrefab;
    public GameObject enemyBeetlePrefab;
    public GameObject healthPickupPrefab;
    public GameObject armourPickupPrefab;

    [Header("Spawn Management")]
    public int enemyCount;
    public int maxHealthPickups = 1;
    public int maxArmourPickups = 1;
    private bool spawning;

    [Header("Wave Attributes")]
    public int waveNumber = 0;
    public GameObject waveText; //Text displayed between waves to indicate incoming wave number
    public TextMeshProUGUI hudWaveText; //Text displayed in HUD indicating the current wave number
    

    private GameManager gameManager;
    void Start()
    {   gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = gameManager.soldierCount + gameManager.beetleCount ;
        if(enemyCount == 0 && !GameManager.isGamePaused && !spawning)
        {   //Check If Won
            if(waveNumber == gameManager.maxWaveNumber)
            {
                gameManager.GameOver(won : true);
                return;
            }
            //else spawn more enemies
       
            spawning = true;
            waveNumber++;
            if(waveNumber % 2 == 0)
            {   
                //maxHealthPickups = 1;
                maxArmourPickups = 1;
                maxHealthPickups = (int) waveNumber/10 + 1;
                //maxArmourPickups = (int) waveNumber/10 + 1;
            }
            else
            {
                maxHealthPickups = 0;
                maxArmourPickups = 0;
            }
            
            
            StartCoroutine(SpawnEnemyWave(waveNumber));
            
            
        }
        
    }


    
    IEnumerator SpawnEnemyWave(int waveNumber)
    {   
        waveText.GetComponent<TextMeshProUGUI>().text = "Wave : " + waveNumber;
        hudWaveText.text = "Wave (" + waveNumber + "/12)";
        waveText.SetActive(true);
        yield return new WaitForSeconds(1f);
        waveText.SetActive(false);
        
        for (int i = 0; i < waveNumber; i++)
        {
            Instantiate(enemySoldierPrefab, PickRandomPosition(), enemySoldierPrefab.transform.rotation);
            Instantiate(enemyBeetlePrefab, PickRandomPosition(), enemyBeetlePrefab.transform.rotation);
        }

        //Spawn HealthPickups if less than max
        for(int i = gameManager.healthPickupCount; i < maxHealthPickups; i++)
        {
            Instantiate(healthPickupPrefab, PickRandomPosition(), healthPickupPrefab.transform.rotation);
        }
        
        //Spawn ArmourPickups if less than max
        for(int i = gameManager.armourPickupCount; i < maxArmourPickups; i++)
        {
            Instantiate(armourPickupPrefab, PickRandomPosition(), armourPickupPrefab.transform.rotation);
        }
        spawning = false;
    }

    public Vector3 PickRandomPosition () {
        while(true)
        {
            var grid = AstarPath.active.data.gridGraph;
            var randomNode = grid.nodes[Random.Range(0, grid.nodes.Length)];
            if (randomNode.Walkable)
            { return (Vector3) randomNode.position; }
        }
    }

    public void SpawnObjectRandomly(GameObject gameObjectPrefab)
    {
        Instantiate(gameObjectPrefab, PickRandomPosition(), gameObjectPrefab.transform.rotation);
    }
}
