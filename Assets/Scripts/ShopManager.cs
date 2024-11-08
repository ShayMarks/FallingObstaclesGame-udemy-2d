using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // הוסף שורה זו אם היא לא קיימת

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI totalCoinsText;

    public Image[] skinImages;
    public TextMeshProUGUI[] priceTexts;
    public Button[] buyButtons;
    public Button[] selectButtons;
    public int[] skinPrices;

    private int totalCoins;

    void Start()
    {
        // טען את כמות המטבעות הכוללת
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        UpdateTotalCoinsText();

        // עדכן את מצב החנות
        UpdateShop();
    }

    void UpdateTotalCoinsText()
    {
        totalCoinsText.text = "Total Coins: " + totalCoins.ToString();
    }

    void UpdateShop()
    {
        for (int i = 0; i < skinPrices.Length; i++)
        {
            bool isPurchased = PlayerPrefs.GetInt("SkinPurchased" + i, 0) == 1;

            if (isPurchased)
            {
                buyButtons[i].gameObject.SetActive(false);
                selectButtons[i].gameObject.SetActive(true);
            }
            else
            {
                buyButtons[i].gameObject.SetActive(true);
                selectButtons[i].gameObject.SetActive(false);
                priceTexts[i].text = skinPrices[i].ToString();
            }

            // עדכון מצב הכפתורים בהתאם לסקין הנבחר
            int selectedSkin = PlayerPrefs.GetInt("SelectedSkin", 0);
            if (selectedSkin == i)
            {
                selectButtons[i].interactable = false;
                selectButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "Selected";
            }
            else
            {
                selectButtons[i].interactable = true;
                selectButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "Use Skin";
            }
        }
    }

    public void BuySkin(int index)
    {
        if (totalCoins >= skinPrices[index])
        {
            totalCoins -= skinPrices[index];
            PlayerPrefs.SetInt("TotalCoins", totalCoins);
            PlayerPrefs.SetInt("SkinPurchased" + index, 1);

            UpdateTotalCoinsText();
            UpdateShop();
        }
        else
        {
            Debug.Log("Not enough coins to purchase this skin.");
        }
    }

    public void SelectSkin(int index)
    {
        PlayerPrefs.SetInt("SelectedSkin", index);
        UpdateShop();
    }

    public void GoToMainMenu()
    {
        // טעינת סצנת MainMenu
        SceneManager.LoadScene("MainMenu");
    }
}
