using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopUIManager : MonoBehaviour
{
    [SerializeField] GameObject propertiesPnl;
    [SerializeField] GameObject colorsPnl;

    public void HomeButtonClick() {
       Debug.Log("HomeButtonClicked");
    }

    public void DownloadButtonClick() {
       Debug.Log("DownloadButtonClicked");
    }

    public void MoveButtonClick() {
       Debug.Log("MoveButtonClicked");
    }

    public void ScaleButtonClick() {
       Debug.Log("ScaleButtonClicked");
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

    public void ColorsButtonClick() {
       Debug.Log("ColorsButtonClicked");

       if(colorsPnl.activeSelf) {
            colorsPnl.SetActive(false);
        }
        else {
            colorsPnl.SetActive(true);
        }
    }

    public void HideButtonClick() {
       Debug.Log("HideButtonClicked");
    }

    public void DeleteButtonClick() {
       Debug.Log("DeleteButtonClicked");
    }

    public void ScreenshotButtonClick() {
       Debug.Log("ScreenshotButtonClicked");
    }

    public void SaveButtonClick() {
       Debug.Log("SaveButtonClicked");
    }

    public void SettingsButtonClick() {
       Debug.Log("SettingsButtonClicked");
    }

    public void VolumeButtonClick() {
       Debug.Log("VolumeButtonClicked");
    }

}
