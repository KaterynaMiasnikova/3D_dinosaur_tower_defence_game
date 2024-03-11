using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHP : MonoBehaviour
{
    public GameObject Castle;
    public double CastleHp;
    GameManager gm;
    public void Dmg_2(int DMG_2count)
    {
        gm.CastleHp -= DMG_2count;
    }

    public void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();

        gm.CastleHp = 0;
        gm.UpdateHP(100);
    }

    private void Update()
    {
        if (gm.CastleHp <= 0)
        {
            gameObject.tag = "Castle_Destroyed"; // send it to TowerTrigger to stop the shooting
            Castle.SetActive(false);
            Time.timeScale = 0;
            gm.PlayGameOver();
            gm.ChangeGameOn(false);
            gm.GameOverItems.SetActive(true);
            gm.HomeMenuItems.SetActive(false);
            gm.HelpItems.SetActive(false);
            gm.SettingsItems.SetActive(false);
            gm.WinItems.SetActive(false);
            gm.DuringGameItems.SetActive(false);
        }
        CastleHp = gm.CastleHp;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("enemyBug"))
        {
            gm.UpdateHP(-0.1);
        }
    }

    
}