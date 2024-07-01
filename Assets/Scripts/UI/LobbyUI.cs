using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button createLobbyButton;
    [SerializeField] private Button quickJoinButton;
    [SerializeField] private Button joinCodeButton;
    [SerializeField] private TMP_InputField joinCodeInputField;
    [SerializeField] private CreateLobbyUI createLobbyUI;


    private void Awake()
    {
        quickJoinButton.onClick.AddListener(KitchenGameLobby.Instance.QuickJoin);
        
        createLobbyButton.onClick.AddListener(() => 
            createLobbyUI.Show());
        
        mainMenuButton.onClick.AddListener(() => Loader.LoadNetwork(Loader.Scene.MainMenu));
        
        joinCodeButton.onClick.AddListener(() => KitchenGameLobby.Instance.JoinWithCode(joinCodeInputField.text));
    }
}
