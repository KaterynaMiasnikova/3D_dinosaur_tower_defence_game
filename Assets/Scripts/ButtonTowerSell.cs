using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonTowerSell : MonoBehaviour, IPointerClickHandler
{
    public GameManager gm;
    public int priceNumber;
    BuildTower towerPlace;

    // add callbacks in the inspector like for buttons
    public UnityEvent onClick;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSellButton(BuildTower tower, int price)
    {
        towerPlace = tower;
        priceNumber = price;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gm.GetGameOn())
        {
            gm.UpdateMoney(priceNumber);
            gm.FinishPurchase();
            towerPlace.BuildNewTower(null);
        }
    }
}
