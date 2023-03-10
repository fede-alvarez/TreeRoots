using UnityEngine;
using DG.Tweening;

public class WaterDrop : MonoBehaviour
{
    [SerializeField] private int _branchIndex;
    [SerializeField] private Transform _resourcesParent;
    [SerializeField] private FruitResource _fruitPrefab;

    private PlayerController _player;
    
    private bool _hasWater = false;

    private void Update() 
    {
        if (_player == null || !_player.HasWater) return;
        if (_player.InteractionPressed)
        {
            CreateResource();
            _player.HasWater = false;
            //_resourcesParent.GetChild(_branchIndex).gameObject.SetActive(true);
            gameObject.SetActive(false);
            AudioManager.GetInstance.PlaySound(AudioManager.AudioList.ActivateRoot);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!other.CompareTag("Player") && _player.HasWater) return;

        if (other.TryGetComponent(out PlayerController player))
        {
            _player = player;
            _hasWater = player.HasWater;
        }
    }

    private void CreateResource()
    {
        Transform resource = _resourcesParent.GetChild(_branchIndex);
        if (resource == null) return;
        if (resource.TryGetComponent(out FruitTrigger fruit))
        {
            AudioManager.GetInstance.PlaySound(AudioManager.AudioList.Spawn);

            FruitResource f = Instantiate(_fruitPrefab, resource.position, Quaternion.identity);
            if (f == null) return;
            fruit.SetFruit(f);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (!other.CompareTag("Player")) return;

    }
}
