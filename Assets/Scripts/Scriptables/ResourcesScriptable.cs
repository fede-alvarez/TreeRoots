using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "FruitSpawnRates", menuName = "GGJ/Fruit Spawn Rates", order = 0)]
public class ResourcesScriptable : ScriptableObject 
{
    public List<FruitSpawnRate> _spawnRates;
}

public class FruitSpawnRate 
{
    
}