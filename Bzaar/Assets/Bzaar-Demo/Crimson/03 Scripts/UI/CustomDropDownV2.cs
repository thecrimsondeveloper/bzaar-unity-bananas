using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.MUIP;
using DG.Tweening;
using System.Threading.Tasks;
using System.Linq;

namespace Bzaar
{
    public class CustomDropDownV2 : MonoBehaviour
    {
        enum DropDirection
        {
            UP,
            DOWN,
            LEFT,
            RIGHT,
        }
        [SerializeField] DropDirection dropDirection = DropDirection.DOWN;
        public ButtonManager buttonRef;
        public GameObject optionsPanel;

        public List<ButtonManager> optionsList = new();

        public int currentlySelectedIndex = 0;
        bool isExpanded = false;


        private void Start()
        {
            Collapse(optionsPanel.GetComponentsInChildren<ButtonManager>().ToList());
        }

        public void RootClicked()
        {
            if (currentlyExpandingOrCollapsing) return;
            if (isExpanded) Collapse(optionsList);
            else Expand(optionsList);
            isExpanded = !isExpanded;
        }

        bool currentlyExpandingOrCollapsing = false;
        void Collapse(List<ButtonManager> buttons)
        {
            currentlyExpandingOrCollapsing = true;
            buttons.ForEach(b =>
            {
                b.GetComponent<ButtonManager>().enabled = false;
                b.transform.DOLocalMove(Vector3.zero, 0.25f).SetEase(Ease.OutCubic).OnComplete(() =>
                {
                    optionsPanel.SetActive(false);
                    currentlyExpandingOrCollapsing = false;
                });
            });
        }
        void Expand(List<ButtonManager> buttons)
        {
            currentlyExpandingOrCollapsing = true;
            Vector3 collapsDirection = Vector3.down;

            if (dropDirection == DropDirection.DOWN) collapsDirection = Vector3.down;
            if (dropDirection == DropDirection.UP) collapsDirection = Vector3.up;
            if (dropDirection == DropDirection.LEFT) collapsDirection = Vector3.left;
            if (dropDirection == DropDirection.RIGHT) collapsDirection = Vector3.right;
            optionsPanel.SetActive(true);
            buttons.ForEach(b =>
            {
                b.transform.localPosition = Vector3.zero;
                int index = optionsList.IndexOf(b);

                b.transform.DOLocalMove(Vector3.zero + Vector3.up * (optionsList.Count - index) * 40, 0.25f).
                                        SetEase(Ease.OutCubic).
                                        OnComplete(() =>
                {
                    currentlyExpandingOrCollapsing = false;
                });

                b.GetComponent<ButtonManager>().enabled = true;
            });
        }
        public void OptionSelected(int selectedIndex)
        {
            buttonRef.SetText(optionsList[selectedIndex].buttonText);

            if (isExpanded) Collapse(optionsList);
            else Expand(optionsList);
            isExpanded = !isExpanded;
            currentlySelectedIndex = selectedIndex;
        }

    }
}
