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
            List<ButtonManager> buttons = optionsPanel.GetComponentsInChildren<ButtonManager>().ToList();
            if (isExpanded) Collapse(buttons);
            else Expand(buttons);
            isExpanded = !isExpanded;
        }

        void Collapse(List<ButtonManager> buttons)
        {
            buttons.ForEach(b =>
            {
                b.transform.DOLocalMove(Vector3.zero, 0.25f).SetEase(Ease.OutCubic).OnComplete(() =>
                {
                    optionsPanel.SetActive(false);
                });
            });
        }
        void Expand(List<ButtonManager> buttons)
        {
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
                b.transform.DOLocalMove(Vector3.zero + Vector3.up * (optionsList.Count - index) * 40, 0.25f).SetEase(Ease.OutCubic);
            });
        }
        public void OptionSelected(int selectedIndex)
        {
            List<ButtonManager> buttons = optionsPanel.GetComponentsInChildren<ButtonManager>().ToList();
            Collapse(buttons);
            currentlySelectedIndex = selectedIndex;
        }

    }
}
