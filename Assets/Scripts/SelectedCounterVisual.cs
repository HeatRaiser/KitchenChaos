
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] selectedCounterVisuals;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChange += OnSelectedCounterChanged;
    }

    private void OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangeEventArgs e)
    {
        foreach (GameObject selectedVisual in selectedCounterVisuals)
        {
            selectedVisual.SetActive(baseCounter == e.selectedCounter);
        }
    }
}
