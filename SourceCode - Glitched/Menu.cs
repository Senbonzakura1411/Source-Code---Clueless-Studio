using DataBaseIntegration;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject panel1, panel2, errorPanel;

    private string _name, _lastName, _email;

    public void NameInputField(string s)
    {
        _name = s;
    }

    public void LastNameInputField(string s)
    {
        _lastName = s;
    }

    public void EmailInputField(string s)
    {
        _email = s;
    }

    public void OnOkClick()
    {
        errorPanel.SetActive(false);
    }

    public void OnPlayClick()
    {
        if (UserData.Player != null)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            panel1.SetActive(false);
            panel2.SetActive(true); 
        }
    }

    public void OnContinueClick()
    {
        if (!string.IsNullOrWhiteSpace(_name) && !string.IsNullOrWhiteSpace(_lastName) &&
            !string.IsNullOrWhiteSpace(_email) && IsValidEmail(_email))
        {
            UserData.Player = new User(_name, _lastName, _email, 0);
            GameObject.Find("UserDataBaseContainer").GetComponent<Register>().RegisterUser();
            if (!UserData.IsError)
            {
                SceneManager.LoadScene(1);
            }
        }
        else
        {
            errorPanel.SetActive(true);
        }
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}