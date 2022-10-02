using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Bzaar {
    public class CustomDropDown : MonoBehaviour
    {
        public GameObject optionsPanel;
        public GameObject dropDirectionImage;
        public TMP_Text rootLabel;

        public List<Button> optionsList = new List<Button>();
        public Color selectedCol;
        public Color unSelectedCol;

        bool dropDownOpened = false;
        public int currentlySelectedIndex = 0;

        public void RootClicked()
        {
            for (int i = 0; i < optionsList.Count; i++)
            {
                if (!optionsList[i]) continue;
                optionsList[i].GetComponentsInChildren<TMP_Text>()[0].color = Color.black;
                optionsList[i].GetComponent<Image>().color = unSelectedCol;
            }
            optionsList[currentlySelectedIndex].GetComponentsInChildren<TMP_Text>()[0].color = Color.white;
            optionsList[currentlySelectedIndex].GetComponent<Image>().color = selectedCol;
            optionsPanel.SetActive(!optionsPanel.activeSelf);
            if (dropDirectionImage) dropDirectionImage.gameObject.SetActive(!optionsPanel.activeSelf);
        }

        public void OptionSelected(int selectedIndex)
        {
            currentlySelectedIndex = selectedIndex;
            rootLabel.text = optionsList[selectedIndex].GetComponentsInChildren<TMP_Text>()[0].text;
            optionsList[selectedIndex].GetComponentsInChildren<TMP_Text>()[0].color = Color.white;
            optionsList[selectedIndex].GetComponent<Image>().color = selectedCol;
            optionsPanel.SetActive(false);
            if (dropDirectionImage) dropDirectionImage.gameObject.SetActive(true);
        }
    }
}
