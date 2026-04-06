using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TMP_Text counterText;
    public TMP_Text incomeText;
    public float counter = 0;
    float nextTimeCheck = 1f;
    float lastIncomeValue = 0;
    [SerializeField] StoreUpgrade[] storeUpgrades;
    [SerializeField] int upgradePerSecond = 5;

    private void Start()
    {
        UpdateUi();
    }

    public void ClickAction()
    {
        counter++;
        UpdateUi();
    }

    private void Update()
    {
       if(nextTimeCheck < Time.timeSinceLevelLoad)
        {
            IdleCalculate();
            nextTimeCheck = Time.timeSinceLevelLoad + (1f / upgradePerSecond);

        }
    }

    void IdleCalculate()
    {
        float sum = 0;
        foreach (var storeUpgrade in storeUpgrades)
        {
            sum += storeUpgrade.CalculateIncomePerSecond();
            storeUpgrade.UpdateUi();
        }
        lastIncomeValue = sum;
        counter += sum / upgradePerSecond;
        UpdateUi();
    }

    public bool PurchaseAction(int cost)
    {
        if(counter >= cost)
        {
            counter -= cost;
            UpdateUi();
            return true;
        }
        return false;
    }

    void UpdateUi()
    {
        counterText.text = Mathf.RoundToInt(counter).ToString();
        incomeText.text = lastIncomeValue.ToString() + " /s";
    }
}
