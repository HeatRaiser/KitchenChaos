
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject selectedCounterVisual;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChange += OnSelectedCounterChanged;
    }

    private void OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangeEventArgs e)
    {
        if (clearCounter == e.selectedCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        selectedCounterVisual.SetActive(true);
    }

    private void Hide()
    {
        selectedCounterVisual.SetActive(false);
    }
}
