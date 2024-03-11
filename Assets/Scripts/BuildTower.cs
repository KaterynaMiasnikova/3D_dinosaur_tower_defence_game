using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTower : MonoBehaviour
{
    public GameObject cannon;
    public GameObject crossbow;
    public GameObject fire;
    public GameObject poison;
    public GameObject areaDemonstrator;
    public string currentTowerTag;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && gm.GetGameOn()) // Left-click check
        {
            // Find all GameObjects with a specific tag
            GameObject[] towerAreas = GameObject.FindGameObjectsWithTag("TowerArea");
            // Iterate through the array to access each object
            foreach (GameObject area in towerAreas)
            {
                area.SetActive(false);
            }

            areaDemonstrator.SetActive(true);
            gm.CallTowerMenu(this, currentTowerTag);
        }
    }

    public void BuildNewTower(string newTowerTag)
    {
        cannon.SetActive(false);
        crossbow.SetActive(false);
        fire.SetActive(false);
        poison.SetActive(false);
        areaDemonstrator.SetActive(false);

        currentTowerTag = newTowerTag;

        switch (newTowerTag)
        {
            case "Crossbow":
                crossbow.SetActive(true);
                // Find all GameObjects with a specific tag
                GameObject[] crossbows = GameObject.FindGameObjectsWithTag("Crossbow");
                // Iterate through the array to access each object
                foreach (GameObject crossbow in crossbows)
                {
                    Tower component = crossbow.GetComponent<Tower>();
                    if (component != null)
                    {
                        component.dmg = 100;
                    }
                }
                break;
            case "Cannon":
                cannon.SetActive(true);
                GameObject[] cannons = GameObject.FindGameObjectsWithTag("Cannon");
                // Iterate through the array to access each object
                foreach (GameObject cannon in cannons)
                {
                    Tower component = cannon.GetComponent<Tower>();
                    if (component != null)
                    {
                        component.dmg = 200;
                    }
                }
                break;
            case "Fire":
                fire.SetActive(true);
                GameObject[] fires = GameObject.FindGameObjectsWithTag("Fire");
                // Iterate through the array to access each object
                foreach (GameObject fire in fires)
                {
                    Tower component = fire.GetComponent<Tower>();
                    if (component != null)
                    {
                        component.dmg = 300;
                    }
                }
                break;
            case "Poison":
                poison.SetActive(true);
                GameObject[] poisons = GameObject.FindGameObjectsWithTag("Cannon");
                // Iterate through the array to access each object
                foreach (GameObject poison in poisons)
                {
                    Tower component = poison.GetComponent<Tower>();
                    if (component != null)
                    {
                        component.dmg = 400;
                    }
                }
                break;
            default:
                break;
        }
        gm.PlayPurchase();
    }
}