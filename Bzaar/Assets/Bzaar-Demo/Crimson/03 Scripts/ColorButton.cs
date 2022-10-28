using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Bzaar
{

    public class ColorButton : MonoBehaviour
    {
        private void Start()
        {
            if (gameObject.TryGetComponent(out Button btn))
            {
                btn.onClick.AddListener(SetColor);
            }
        }


        public void SetColor()
        {
            Color color = gameObject.GetComponent<Image>().color;
            App.instance.outfit.materialController.SetSelectedObjColor(color);
        }
    }

}
