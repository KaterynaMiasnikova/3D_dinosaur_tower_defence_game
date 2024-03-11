using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class StartGameButton : MonoBehaviour, IPointerClickHandler
{
    // add callbacks in the inspector like for buttons
    public UnityEvent onClick;
    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        //Debug.Log(name + " Button Clicked!", this);
        // invoke your event
        onClick.Invoke();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // Iterate through the array to access each object
        if (enemies != null)
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
        foreach (GameObject tower in gm.Towers)
        {
            tower.SetActive(false);
        }
        BuildTower[] buildTowers = FindObjectsOfType<BuildTower>();
        foreach (BuildTower buildTower in buildTowers)
        {
            buildTower.currentTowerTag = null;
        }
        gm.waveCount = 0;
        gm.newWaveCount = 1;
        gm.SetHP(100);
        gm.SetMoney(500);
        gm.Castle.SetActive(true);
        gm.MainBackground.SetActive(false);
        gm.HomeMenuItems.SetActive(false);
        gm.HelpItems.SetActive(false);
        gm.SettingsItems.SetActive(false);
        gm.WinItems.SetActive(false);
        gm.GameOverItems.SetActive(false);
        gm.DuringGameItems.SetActive(true);

        gm.StartGame();
    }
}