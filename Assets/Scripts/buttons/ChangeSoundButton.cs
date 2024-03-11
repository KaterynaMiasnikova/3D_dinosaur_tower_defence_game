using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ChangeSoundButton : MonoBehaviour, IPointerClickHandler
{
    // add callbacks in the inspector like for buttons
    public UnityEvent onClick;
    public GameObject musicOnIcon, musicOffIcon;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        //gm = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Output to console the clicked GameObject's name and the following message.
        Debug.Log(name + " SoundChanged!", this);
        // invoke your event
        onClick.Invoke();
        if (gm.AreSounds())
        {
            gm.ChangeSounds();
            musicOnIcon.SetActive(false);
            musicOffIcon.SetActive(true);
        }
        else
        {
            gm.ChangeSounds();
            musicOffIcon.SetActive(false);
            musicOnIcon.SetActive(true);
        }
    }
}
