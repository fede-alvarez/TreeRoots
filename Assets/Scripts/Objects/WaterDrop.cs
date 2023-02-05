using UnityEngine;
using DG.Tweening;

public class WaterDrop : MonoBehaviour
{
    [SerializeField] private int _branchIndex;
    [SerializeField] private Transform _resourcesParent;
    [SerializeField] private FruitResource _fruitPrefab;
    
    private bool _hasWater = false;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!other.CompareTag("Player")) return;

        if (other.TryGetComponent(out PlayerController player))
        {
            _hasWater = player.HasWater;
        }

        Transform resource = _resourcesParent.GetChild(_branchIndex);
        if (resource == null) return;
        if (resource.TryGetComponent(out FruitTrigger fruit))
        {
            FruitResource t = Instantiate(_fruitPrefab, resource.position, Quaternion.identity);
            if (t == null) return;
            //resource.ha
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (!other.CompareTag("Player")) return;

    }
}
