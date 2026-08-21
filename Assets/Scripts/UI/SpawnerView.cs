using TMPro;
using UnityEngine;

public class SpawnerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _counterCubs;
    [SerializeField] private TextMeshProUGUI _counterBombs;
    [SerializeField] private CubeSpawner _counterCub;
    [SerializeField] private BombSpawner _counterBomb;


    private void OnEnable()
    {
        _counterCub.CubsSpawned += ChangeTextCubs;
        _counterBomb.BombSpawned += ChangeTextBombs;
    }

    private void OnDisable()
    {
        _counterCub.CubsSpawned -= ChangeTextCubs;
        _counterBomb.BombSpawned -= ChangeTextBombs;
    }

    private void ChangeTextCubs(int currentNumber, int totalCount)
    {
        ChangeText(_counterCubs, currentNumber, totalCount, "кубов");
    }

    private void ChangeTextBombs(int currentNumber, int totalCount)
    {
        ChangeText(_counterBombs, currentNumber, totalCount, "бомб");
    }

    private void ChangeText(TextMeshProUGUI field, int currentNumber, int totalCount, string name)
    {
        field.text = $"На сцене {currentNumber} {name}\nВсего заспавнено было {totalCount}";
    }
}
