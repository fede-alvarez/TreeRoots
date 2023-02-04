using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyRates", menuName = "GGJ/Enemies Spawn Rates", order = 0)]
public class EnemyWavesScriptable : ScriptableObject 
{
    public List<EnemySpawnRate> _spawnRates;
}

[System.Serializable]
public class EnemySpawnRate 
{
    public string Round = "Round 1";
    public int EnemiesFromLeft = 1;
    public int EnemiesFromRight = 1;
    public float WaitForNextWave = 1f;
}