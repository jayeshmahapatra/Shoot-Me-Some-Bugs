using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryEffect : MonoBehaviour
{
    // Start is called before the first frame update
    private ParticleSystem particle;
    public SpawnManager spawnManager;
    void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        spawnManager = GameObject.FindGameObjectWithTag("Respawn").GetComponent<SpawnManager>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale < 0.01f)
         {
             particle.Simulate(Time.unscaledDeltaTime, true, false);
         }
    }
}
