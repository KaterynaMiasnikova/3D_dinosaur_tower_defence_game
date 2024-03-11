using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class SettingsButton : MonoBehaviour, IPointerClickHandler
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

        gm.HomeMenuItems.SetActive(false);
        gm.HelpItems.SetActive(false);
        gm.SettingsItems.SetActive(true);
        gm.WinItems.SetActive(false);
        gm.GameOverItems.SetActive(false);
        gm.DuringGameItems.SetActive(false);

        gm.StopGame();
    }
}
