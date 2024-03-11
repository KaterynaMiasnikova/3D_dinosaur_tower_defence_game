using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonTowerSelection : MonoBehaviour, IPointerClickHandler
{
    public GameManager gm;
    //public Text price;
    public GameObject NotEnoughMoney;
    public ButtonTowerSell sellButton;
    public int priceNumber;
    public string towerType;
    // add callbacks in the inspector like for buttons
    public UnityEvent onClick;
    BuildTower towerPlace;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gm.GetMoney() < priceNumber)
        {
            NotEnoughMoney.gameObject.SetActive(true);
        } else
        {
            NotEnoughMoney.gameObject.SetActive(false);
        }
    }

    public void CheckTower(string currentTowerTag, BuildTower tower)
    {
        towerPlace = tower;
        if (currentTowerTag == towerType)
        {
            sellButton.gameObject.SetActive(true);
            sellButton.setSellButton(tower, priceNumber);
            gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gm.GetMoney() >= priceNumber && gm.GetGameOn())
        {
            gm.UpdateMoney(-priceNumber);
            gm.FinishPurchase();
            towerPlace.BuildNewTower(towerType);
        } 
    }
}