using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Towers;
    public GameObject Castle;
    public Text HPText;
    public int waveCount;
    public int newWaveCount;
    public double CastleHp = 0;
    int currentEnemyHP;
    double currentSpeedMultiplier;
    public int money = 0;
    int enemies = 0;
    int deadEnemies = 0;
    bool allWaves = false;
    bool areSounds = true;
    bool gameOn = true;
    public Text moneyText;
    public BuildMenu towerMenu; // Reference to the tower menu UI panel
    public Texture2D cursorTexture; // Assign the custom cursor texture in the Inspector
    public CursorMode cursorMode; // Set cursor mode (Auto by default)
    public Vector2 hotSpot = Vector2.zero; // Set cursor hotspot
    public AudioClip mainMenuMusic, gameMusic, victoryMusic, gameOverMusic, purchase, buttonClick, dinosaurWalk, dinosaurAttack, dinosaurDead, towerCrossbowShot, towerCannonShot, towerFireShot, towerPoisonShot;
    public AudioSource audioSource, musicSource;
    public GameObject MainBackground, HomeMenuItems, HelpItems, SettingsItems, WinItems, GameOverItems, DuringGameItems, PauseItems, PlayItems;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true; // Ensure cursor visibility is enabled
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode); // Set custom cursor
        //Time.timeScale = 2;
        SetMoney(500);
        SetHP(100);
        StopGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (allWaves && enemies == deadEnemies)
        {
            PlayVictory();
            WinItems.SetActive(true);
            HomeMenuItems.SetActive(false);
            HelpItems.SetActive(false);
            SettingsItems.SetActive(false);
            GameOverItems.SetActive(false);
            DuringGameItems.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in the Editor
#else
            Application.Quit(); // Quits the application in a build
#endif
        }
    }

    public void StartGame()
    {
        ChangeGameOn(true);
        Time.timeScale = 1;
        //musicSource.Stop();
        AudioClip original = musicSource.clip;
        musicSource.clip = gameMusic;
        if(areSounds) //&& original != musicSource.clip)
            musicSource.Play();
    }
    public void StopGame()
    {
        ChangeGameOn(false);
        Time.timeScale = 0;
        //musicSource.Stop();
        AudioClip original = musicSource.clip;
        musicSource.clip = mainMenuMusic;
        if (areSounds) // && original != musicSource.clip)
           musicSource.Play();
    }

    public void ChangeGameOn(bool newGameOn)
    {
        gameOn = newGameOn;
    }
    public bool GetGameOn()
    {
        return gameOn;
    }
    
    public void AllWavesPassed()
    {
        allWaves = true;
    }
    public void AddEnemie()
    {
        enemies++;
    }
    public void AddDeadEnemie()
    {
        deadEnemies++;
        Debug.Log(enemies + " enemies!"+ deadEnemies + " dead!");
    }

    public void CallTowerMenu(BuildTower TowerPlace, string currentTowerTag)
    {
        towerMenu.gameObject.SetActive(true); // Activate the tower menu panel
        towerMenu.SetTower(TowerPlace, currentTowerTag);
    }
    public void FinishPurchase()
    {
        towerMenu.gameObject.SetActive(false); // Activate the tower menu panel
    }
    public void UpdateHP(double hp)
    {
        CastleHp += hp;
        HPText.text = "" + (int)CastleHp;
    }
    public void SetHP(double hp)
    {
        CastleHp = hp;
        HPText.text = "" + (int)CastleHp;
    }
    public int GetMoney()
    {
        return money;
    }
    public void UpdateMoney(int add)
    {
        money += add;
        moneyText.text = "" + money;
    }
    public void SetMoney(int add)
    {
        money = add;
        moneyText.text = "" + money;
    }

    public int GetEnemyHP()
    {
        return currentEnemyHP;
    }
    public void UpdateEnemyHP(int newHP)
    {
        currentEnemyHP = newHP;
    }

    public double GetSpeedMultiplier()
    {
        return currentSpeedMultiplier;
    }
    public void UpdateSpeedMultiplier(double newMultiplier)
    {
        currentSpeedMultiplier = newMultiplier;
    }

    public bool AreSounds()
    {
        return areSounds;
    }
    public void ChangeSounds()
    {
        areSounds = !areSounds;
        if (!areSounds)
        {
            audioSource.Stop();
            musicSource.Stop();
        }

    }

    public void PlayVictory()
    {
        if(areSounds)
        audioSource.PlayOneShot(victoryMusic);
    }
    public void PlayGameOver()
    {
        if (areSounds)
            audioSource.PlayOneShot(gameOverMusic);
    }
    public void PlayPurchase()
    {
        if (areSounds)
            audioSource.PlayOneShot(purchase);
    }
    public void PlayClick()
    {
        if (areSounds)
            audioSource.PlayOneShot(buttonClick);
    }
    public void PlayDinoAttack()
    {
        if (areSounds)
            audioSource.PlayOneShot(dinosaurAttack);
    }
    public void PlayDinoDead()
    {
        if (areSounds)
            audioSource.PlayOneShot(dinosaurDead);
    }
    public void PlayCrossbow()
    {
        if (areSounds)
            audioSource.PlayOneShot(towerCrossbowShot);
    }
    public void PlayCannon()
    {
        if (areSounds)
            audioSource.PlayOneShot(towerCannonShot);
    }
    public void PlayFire()
    {
        if (areSounds)
            audioSource.PlayOneShot(towerFireShot);
    }
    public void PlayPoison()
    {
        if (areSounds)
            audioSource.PlayOneShot(towerPoisonShot);
    }

    public void SpeedUpTheGame()
    {
        Time.timeScale += 0.5f;
    }
    public void SlowDownTheGame()
    {
        Time.timeScale = 1;
    }
}
