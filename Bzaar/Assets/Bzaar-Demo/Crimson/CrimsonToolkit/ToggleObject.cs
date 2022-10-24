using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crimson
{
    public class ToggleObject : MonoBehaviour
    {
        [SerializeField] GameObject target;
        public void Toggle()
        {
            if (!target)
            {
                Debug.LogError("No target attached to ObjectToggle");
                return;
            }
            target.SetActive(!target.activeSelf);
        }
        public void SetEnabled(bool value)
        {
            if (!target)
            {
                Debug.LogError("No target attached to ObjectToggle");
                return;
            }
            target.SetActive(value);
        }
    }
}



