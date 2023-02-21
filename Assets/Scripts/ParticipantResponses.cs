using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ParticipantResponses : MonoBehaviour
{
    //[SerializeField] GameObject[] UIElements;
    //List<GameObject> UIElementsList = new List<GameObject>();
    public Dictionary<string, int> participantResponseDict = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSelection()
    {
        print("test");
        GameObject currentGO = EventSystem.current.currentSelectedGameObject;
        string currentNameKey = currentGO.name;
        //ParticipantResponseDict[currentNameKey] = value;

        // if toggle button
        if (EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>() != null)
        {
            Toggle toggle = EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>();
            participantResponseDict[currentNameKey] = toggle.isOn ? 1 : 0;
        }
        // if slider
        if (EventSystem.current.currentSelectedGameObject.GetComponent<Slider>() != null)
        {
            Slider slider = EventSystem.current.currentSelectedGameObject.GetComponent<Slider>();
            participantResponseDict[currentNameKey] = (int)slider.value;
        }
    }

    public void NewDict()
    {
        participantResponseDict.Clear();
    }
}
