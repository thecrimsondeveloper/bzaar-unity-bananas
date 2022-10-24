using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bzaar
{
    public class RotateGizmo : MonoBehaviour
    {


        [SerializeField] GameObject controlledoBject;

        [SerializeField] float rotateSpeed;

        float screenXPointLastFrame;
        void OnMouseDrag()
        {
            if (screenXPointLastFrame != 0)
            {
                float delta = Input.mousePosition.x - screenXPointLastFrame;
                controlledoBject.transform.Rotate(Vector3.up, -delta * rotateSpeed * Time.deltaTime);
            }
            screenXPointLastFrame = Input.mousePosition.x;
        }
        private void OnMouseUp()
        {
            screenXPointLastFrame = 0;
        }

    }
}
