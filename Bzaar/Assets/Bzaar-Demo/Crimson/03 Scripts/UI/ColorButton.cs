using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Bzaar {

    public class ColorButton : MonoBehaviour
    {
        [SerializeField] ColorManager colorManager;

        private void Start()
        {
            if (gameObject.TryGetComponent(out Button btn))
            {
                btn.onClick.AddListener(SetColor);
            }
        }


        public void SetColor()
        {
            colorManager.SetFinalColor(gameObject.GetComponent<Image>().color);
        }
    }

}
