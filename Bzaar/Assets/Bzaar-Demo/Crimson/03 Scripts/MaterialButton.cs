using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Bzaar
{
    public class MaterialButton : MonoBehaviour
    {
        public void SpawnAnimation()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(1, 1f).SetEase(Ease.OutBounce); ;
        }

        public void SetMaterial()
        {
            Sprite texture = gameObject.GetComponent<UnityEngine.UI.Image>().sprite;
            App.instance.outfit.materialController.SetObjSkin(texture);
        }
    }
}
