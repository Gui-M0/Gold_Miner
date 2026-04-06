using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoreUpgrade : MonoBehaviour
{
    [Header("Components")]
    public TMP_Text priceText;
    public TMP_Text incomePerSecText;
    public Button button;
    public Image characterImage;
    public TMP_Text upgradeNameText;

    [Header("Managers")]
    public GameManager gameManager;

    [Header("Upgrade Settings")]
    public string upgradeName;
    public int startPrice = 15;
    public float upgradePriceMultiplier;
    public float goldPerUpgrade;
    int upgradeLevel = 0;
    

    private void Start()
    {
        UpdateUi();
    }

    public void ClickAction()
    {
        int price = CalculatePrice();
        bool purchaseSuccessful = gameManager.PurchaseAction(price);
        if(purchaseSuccessful)
        {
            upgradeLevel++;
            UpdateUi();
        }
    }

    public void UpdateUi()
    {
        priceText.text = CalculatePrice().ToString();
        incomePerSecText.text = upgradeLevel.ToString() + " x " + goldPerUpgrade + "/s";
        bool canAfford = gameManager.counter >= CalculatePrice();
        button.interactable = canAfford;

        bool isPurchased = upgradeLevel > 0;
        characterImage.color = isPurchased ? Color.white : Color.black;

        upgradeNameText.text = isPurchased ? upgradeName : "???";
    }


    int CalculatePrice()
    {
        int price = Mathf.RoundToInt (startPrice * Mathf.Pow(upgradePriceMultiplier, upgradeLevel));
        return price;

    }

    public float CalculateIncomePerSecond()
    {
        return goldPerUpgrade * upgradeLevel;
    }
}
