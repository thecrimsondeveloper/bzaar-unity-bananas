using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crimson
{
    public class ToggleComponent : MonoBehaviour
    {
        [SerializeField] Component component;

        public void Toggle()
        {
            if (!component)
            {
                Debug.LogError("No component attached to ComponentToggle");
                return;
            }
            if (component is Behaviour behaviour)
            {
                behaviour.enabled = !behaviour.enabled;
            }
            else if (component is Renderer renderer)
            {
                renderer.enabled = !renderer.enabled;
            }
            else if (component is Collider collider)
            {
                collider.enabled = !collider.enabled;
            }
        }

        public void SetEnabled(bool value)
        {
            if (!component)
            {
                Debug.LogError("No component attached to ComponentToggle");
                return;
            }
            if (component is Behaviour behaviour)
            {
                behaviour.enabled = value;
            }
            else if (component is Renderer renderer)
            {
                renderer.enabled = value;
            }
            else if (component is Collider collider)
            {
                collider.enabled = value;
            }
        }
    }

}
