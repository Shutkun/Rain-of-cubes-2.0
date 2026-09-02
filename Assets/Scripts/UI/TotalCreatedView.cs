using TMPro;
using UnityEngine;

public class TotalCreatedView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textField;

    private void OnEnable()
    {
        SpawnStatistics.TotalCreatedChanged += ChangeText;
        ChangeText(SpawnStatistics.TotalCreated);
    }

    private void OnDisable()
    {
        SpawnStatistics.TotalCreatedChanged -= ChangeText;
    }

    private void ChangeText(int total)
    {
        _textField.text = $"Всего создано объектов: {total}";
    }
}
