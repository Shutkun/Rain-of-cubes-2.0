using TMPro;
using UnityEngine;

public class SpawnerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _counterCubs;
    //[SerializeField] private TextMeshProUGUI _counterBombs;
    [SerializeField] private CubeSpawner _counterCub;


    private void OnEnable()
    {
        _counterCub.CubsSpawned += ChangeTextCubs;
    }

    private void OnDisable()
    {
        _counterCub.CubsSpawned -= ChangeTextCubs;
    }

    private void ChangeTextCubs(int currentNumber, int totalCount)
    {
        _counterCubs.text = $"На сцене {currentNumber} кубов\nВсего заспавнено было {totalCount}";
    }
}
