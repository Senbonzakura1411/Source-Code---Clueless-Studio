using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{

    [SerializeField] TextMeshProUGUI moneyText, viewersText, subscribersText, pointText;
    [SerializeField] TextMeshProUGUI toysCountText, foodCountText, housesCountText, litterCountText;
    [SerializeField] GameObject subUIPrefab, donationUIPrefab;
    [SerializeField] GameObject canvas;
    [SerializeField] Image happyCat;
    GameObject subTemp, donationTemp;

    [HideInInspector] public bool donationAlive, subscriberAlive;

    float elapsed;

    void Update()
    {
        moneyText.text = "Money = $" + ResourcesManager.Instance.money;
        subscribersText.text = "Subscribers = " + ResourcesManager.Instance.subscribers;
        pointText.text = "Points = " + PointSystem.Instance.points;
        toysCountText.text = "Toys owned = " + PlaceableObjectManager.Instance.toys.Count;
        foodCountText.text = "Food bowls owned = " + PlaceableObjectManager.Instance.food.Count;
        housesCountText.text = "Beds owned = " + PlaceableObjectManager.Instance.houses.Count;
        litterCountText.text = "Litter boxes owned = " + PlaceableObjectManager.Instance.litter.Count;
        happyCat.fillAmount = PointSystem.Instance.happiness / 1000f;


        elapsed += Time.deltaTime;
        if (elapsed >= 1f)
        {
            elapsed = elapsed % 1f;
            viewersText.text = "Viewers = " + ResourcesManager.Instance.viewers;
        }



    }

    public void NewSubscriber()
    {
        subscriberAlive = true;
        subTemp = Instantiate(subUIPrefab, new Vector3(Screen.width / 2, Screen.height / 4, 0), Quaternion.identity, canvas.transform);
        StartCoroutine(DestroySubscriberMessage());
    }

    public void NewDonation(int value)
    {
        donationAlive = true;
        donationTemp = Instantiate(donationUIPrefab, new Vector3(Screen.width / 2, Screen.height / 3, 0), Quaternion.identity, canvas.transform);
        donationTemp.GetComponent<TextMeshProUGUI>().text = " Random User Donated: " + value;
        StartCoroutine(DestroyDonationMessage());
    }

    IEnumerator DestroyDonationMessage()
    {
        yield return new WaitForSeconds(1f);
        Destroy(donationTemp.gameObject);
        donationAlive = false;
    }
    IEnumerator DestroySubscriberMessage()
    {
        yield return new WaitForSeconds(1f);
        Destroy(subTemp.gameObject);
        subscriberAlive = false;
    }
}
