using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace DataBaseIntegration
{
   public class Register : MonoBehaviour
   {
      public void RegisterUser()
      {
         StartCoroutine(RegisterCo());
      }

      private IEnumerator RegisterCo()
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
         }
         else
         {
            Debug.Log(www.downloadHandler.text);
            Debug.Log("Success uploading form!");
         }
      }
   }
}
