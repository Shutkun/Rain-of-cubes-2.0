using TMPro;
using UnityEngine;

public class SpawnerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textField;
    [SerializeField] private string _name;
    [SerializeField] private MonoBehaviour _spawnerComponent;

    private ISpawnerWithStats _spawner;
    
    private void OnEnable()
    {
        if (_spawnerComponent.TryGetComponent<ISpawnerWithStats>(out ISpawnerWithStats stats))
        {
            _spawner = stats;
        }

        if (_spawner != null)
        {
            _spawner.Spawned += ChangeText;
        }
    }

    private void OnDisable()
    {
        if (_spawner != null)
            _spawner.Spawned -= ChangeText;
    }

    private void ChangeText(int current, int total, int createCount)
    {
        _textField.text = $"На сцене {current} {_name}\nВсего заспавнено было {total} \nВсего создано {createCount}";
    }
}

