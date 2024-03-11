using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class ExitButton : MonoBehaviour, IPointerClickHandler
{
    // add callbacks in the inspector like for buttons
    public UnityEvent onClick;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in the Editor
        #else
            Application.Quit(); // Quits the application in a build
        #endif
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
        ExitGame(); // Exit the game
    }
}