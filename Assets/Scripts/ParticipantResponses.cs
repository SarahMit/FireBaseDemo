using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ParticipantResponses
{
    //stores all responses added by UpdateSelection since the last call of TrackChanges
    private Dictionary<string, int> recentParticipantResponses = new Dictionary<string, int>();

    //stores all responses 
    private Dictionary<string, int> totalParticipantResponses = new Dictionary<string, int>();

    public static ExperimentLogger Instance { get; private set; }
    int pressCount = 0;


    public void UpdateSelection()
    {
        GameObject currentGO = EventSystem.current.currentSelectedGameObject;
        string currentNameKey = currentGO.name;
        //ParticipantResponseDict[currentNameKey] = value;

        // if button then count how often it is pressed
        if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null)
        {
            pressCount++;
            Debug.Log("Button pressed " + pressCount + " times.");
            recentParticipantResponses[currentNameKey] = pressCount;
        }

        // if toggle button
        if (EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>() != null)
        {
            Toggle toggle = EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>();
            recentParticipantResponses[currentNameKey] = toggle.isOn ? 1 : 0;
        }
        
        // if slider
        if (EventSystem.current.currentSelectedGameObject.GetComponent<Slider>() != null)
        {
            Slider slider = EventSystem.current.currentSelectedGameObject.GetComponent<Slider>();
            recentParticipantResponses[currentNameKey] = (int)slider.value;
        }
        Debug.Log("After: " + DictionaryToString(recentParticipantResponses));
    }

    public Dictionary<string, int> GetRecentParticipantResponses()
    {
        return recentParticipantResponses;
    }

    public Dictionary<string, int> TrackChanges()
    {
        pressCount = 0;

        Dictionary<string, int> changes = new Dictionary<string, int>();
        foreach (var pair in recentParticipantResponses)
        {
            if (totalParticipantResponses.ContainsKey(pair.Key))
            {
                if (totalParticipantResponses[pair.Key] != pair.Value)
                    changes.Add(pair.Key, pair.Value);
            }
            else
            {
                changes.Add(pair.Key, pair.Value);
            }
            totalParticipantResponses[pair.Key] = pair.Value;
        }
        recentParticipantResponses.Clear();
        return changes;
    }

    public Dictionary<string, int> GetTotalParticipantResponses()
    {
        Dictionary<string, int> responses = new Dictionary<string, int>();
        foreach (var pair in recentParticipantResponses)
        {
            if (responses.ContainsKey(pair.Key))
            {
                responses.Add(pair.Key, pair.Value);
            }
        }
        return responses;
    }

    public string DictionaryToString(Dictionary<string, int> dict)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder("");
        foreach (var pair in dict)
        {
            sb.Append(pair.Key.ToString() + ": " + pair.Value.ToString() + "; ");
        }
        return sb.ToString();
    }

    public string DictionaryToJSON(Dictionary<string, int> dict)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder("");
        sb.Append('"' + "Responses" + '"' + ":{\n");
        int startlength = sb.Length;
        foreach (var pair in dict)
        {
            sb.Append('"' + pair.Key.ToString() + '"' + ':' + pair.Value.ToString() + ",\n");
        }

        // remove the comma of the last line
        if (sb.Length > startlength)
        {
            Debug.Log(sb.Length - 1);
            sb.Remove(sb.Length - 2, 1);
        }

        sb.Append("}");
        return sb.ToString();
    }

}
