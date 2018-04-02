using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject menuPanel;
    public GameObject resultsPanel;
    public Text failText, timeText, purchasedUnitsText, healthUsesText, moneySpentText;
    public Text moneyText;
    public Text scoreText;
    public Material selectedMat;
    public Material normalMat;
    public int unitCost = 500;
    public int healthCost = 200;
    private HomeBase HB;
    public int time, purchasedUnits, healthUses, moneySpent;
    private bool menuActive = true;
    private int currentMoney = 2000;
    private int currentScore = 0;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

	// Use this for initialization
	void Start () {

        HB = FindObjectOfType<HomeBase>();
        resultsPanel.SetActive(false);
        StartCoroutine(Timer());
	}
	
	// Update is called once per frame
	void Update () {

        UpdateUI();
	}

    private void UpdateUI()
    {
        timeText.text = time + " seconds";
        purchasedUnitsText.text = purchasedUnits.ToString();
        healthUsesText.text = healthUses.ToString();
        moneySpentText.text = moneySpent.ToString();
        moneyText.text = "$" + currentMoney;
        scoreText.text = currentScore.ToString();
    }

    public void ToggleBaseMenu()
    {
        if (menuActive) menuPanel.SetActive(false);
        else menuPanel.SetActive(true);

        menuActive = !menuActive;
    }

    public void BuyUnit()
    {
        if (currentMoney >= unitCost)
        {
            HB.SpawnUnit();
            purchasedUnits++;
            currentMoney -= unitCost;
            moneySpent += unitCost;
        }
    }

    public void BuyHealth()
    {
        if (currentMoney >= healthCost)
        {
            HB.IncreaseHealth();
            healthUses++;
            currentMoney -= healthCost;
            moneySpent += healthCost;
        }
    }  

    public void IncreaseScore()
    {
        currentScore += 10;
    }

    public void IncreaseMoney()
    {
        currentMoney += 1000;
    }

    public void IncreaseDifficulty()
    {

    }

    public void DecreaseDifficulty()
    {

    }

    public void GameOver(bool fail)
    {
        if (fail) failText.text = "Artefact Failed";
        else failText.text = "Artefact Passed";
        resultsPanel.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        time++;
        StartCoroutine(Timer());
    }

}
