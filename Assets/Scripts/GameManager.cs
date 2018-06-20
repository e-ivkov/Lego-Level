using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private int currency = 0;
    public GameObject currencyText;
    private int numberOfTowers = 5;
    public GameObject nTowersText;
    public int towerCost;
    public GameObject towerCostText;

    public int Currency
    {
        get
        {
            return currency;
        }

        set
        {
            currency = value;
            currencyText.GetComponent<Text>().text = "Currency: " + value.ToString();
        }
    }

    public int NumberOfTowers
    {
        get
        {
            return numberOfTowers;
        }

        set
        {
            numberOfTowers = value;
            nTowersText.GetComponent<Text>().text = "Number of towers: " + value.ToString();
        }
    }

    public void AddTower(){
        if(currency >= 5){
            NumberOfTowers++;
            Currency -= 5;
        }
    }

    // Use this for initialization
    void Start()
    {
        currencyText.GetComponent<Text>().text = "Currency: " + currency.ToString();
        towerCostText.GetComponent<Text>().text = "Tower cost: " + towerCost.ToString();
        nTowersText.GetComponent<Text>().text = "Number of towers: " + numberOfTowers.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
