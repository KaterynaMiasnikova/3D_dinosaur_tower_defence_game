using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
    public List<ButtonTowerSelection> buttons;
    public List<ButtonTowerSell> sellButtons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTower(BuildTower tower, string currentTowerTag)
    {
        foreach (ButtonTowerSell sell in sellButtons)
        {
            sell.gameObject.SetActive(false);
        }
        foreach (ButtonTowerSelection button in buttons)
        {
            button.gameObject.SetActive(true);
            button.CheckTower(currentTowerTag, tower);
        }
    }
}