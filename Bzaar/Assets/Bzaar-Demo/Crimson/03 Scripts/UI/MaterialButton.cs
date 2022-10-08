using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bzaar
{
    public class MaterialButton : MonoBehaviour
    {
        public void SetMaterial()
        {
            Sprite texture = gameObject.GetComponent<UnityEngine.UI.Image>().sprite;
            Editor.instance.outfit.materialController.SetObjSkin(texture);
        }
    }
}
