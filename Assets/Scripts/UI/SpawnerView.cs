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
        _spawner = _spawnerComponent.GetComponent<ISpawnerWithStats>();
        if (_spawner != null)
            _spawner.Spawned += ChangeText;
    }

    private void OnDisable()
    {
        if (_spawner != null)
            _spawner.Spawned -= ChangeText;
    }

    private void ChangeText(int current, int total)
    {
        _textField.text = $"На сцене {current} {_name}\nВсего заспавнено было {total}";
    }
}

