using DataBaseIntegration;
using UnityEngine;

public class UserData : MonoBehaviour
{
    [SerializeField] private GameObject errorPanel;
     
    public static User Player;
    public static bool IsError;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ErrorMessageActivation()
    {
        IsError = true;
        errorPanel.SetActive(true);
    }
    public void OnOkClick()
    {
        errorPanel.SetActive(false);
        IsError = false;
    }
}