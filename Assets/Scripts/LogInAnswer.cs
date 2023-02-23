using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogInAnswer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableNavigation()
    {
        GameObject next = GameObject.Find("/LogHandler/Canvas/Next");
        Debug.Log(next.gameObject.name);
        next.GetComponent<Button>().interactable = true;
    }
}
