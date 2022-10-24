using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopUIManager : MonoBehaviour
{
    [SerializeField] GameObject propertiesPnl;

    public void HomeButtonClick() {
       Debug.Log("HomeButtonClicked");
    }

    public void PropertiesButtonClick() {
        Debug.Log("PropertiesButtonClicked");

        if(propertiesPnl.activeSelf) {
            propertiesPnl.SetActive(false);
        }
        else {
            propertiesPnl.SetActive(true);
        }
    }
}
