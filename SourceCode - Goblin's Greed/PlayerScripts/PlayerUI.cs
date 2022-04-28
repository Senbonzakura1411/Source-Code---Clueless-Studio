using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts
{
   public class PlayerUI : MonoBehaviour
   {
        [SerializeField] private TextMeshProUGUI timerText;

        public Image hasteIcon, copyIcon, throwIcon;

        public Image mochila;

        public TextMeshProUGUI infoTxt;


        private void Update()
      {
         timerText.text = GameManager.Instance.InGameTimer;
      }
   }
}
