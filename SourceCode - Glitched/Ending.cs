using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    [SerializeField] private GameObject panel1, panel2;
    
    public void UpdateUser()
    {
        UserData.Player.SetIsCompleted(1);
        StartCoroutine(UpdateCo());
    }

    private IEnumerator UpdateCo()
    {
        var form = new WWWForm();
        form.AddField("name", UserData.Player.GetName());
        form.AddField("lastName", UserData.Player.GetLastName());
        form.AddField("email", UserData.Player.GetEmail());
        form.AddField("isCompleted", UserData.Player.GetIsCompleted());
        //var www = UnityWebRequest.Post("http://localhost:8888/SqlConnect/register.php", form);
        var www = UnityWebRequest.Post("https://gg-gamefest.sv/Assets/register.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
            panel2.SetActive(true);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            Debug.Log("Success uploading form!");
            panel2.SetActive(true);
        }
    }

    public void OnPanelOk()
    {
        SceneManager.LoadScene(0);
    }
    
    public void OnErrorPanelOk()
    {
        panel1.SetActive(false);
    }
}