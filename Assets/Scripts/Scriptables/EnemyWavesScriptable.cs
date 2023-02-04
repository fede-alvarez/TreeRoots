using System.Collections.Generic;
using UnityEngine;

[
    CreateAssetMenu(
        fileName = "EnemyRates",
        menuName = "GGJ/Enemies Spawn Rates",
        order = 0)
]
public class EnemyWavesScriptable : ScriptableObject
{
    public List<Wave> Waves;
}

[System.Serializable]
public class Wave
{
    public string Name = "Round 1";

    public int[] EnemiesFromSpawnPoints = { 1, 1 };

    public float WaitForNextWave = 1f;
}
