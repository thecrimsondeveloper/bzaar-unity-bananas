using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bzaar {
    public class MovementController : MonoBehaviour
    {
        const float MOVE_OBJECT_SENSITIVITY = 0.025f;
        Outfit outfit => gameObject.GetComponent<Outfit>();

        [Header("Controls")]
        [SerializeField] float moveSensitivity = 5f;
  

        private void Update()
        {
            Movement();
        }

        Vector2 lastMoveValues = Vector3.zero;
        void Movement()
        {
            if (Input.touchCount == 2 && outfit.selectedObject)
            {

                Vector2 midPoint = (Input.GetTouch(0).position + Input.GetTouch(1).position) / 2;
                Vector2 posDif = lastMoveValues - midPoint;
                if (lastMoveValues != Vector2.zero)
                {
                    outfit.selectedObject.transform.Translate(Vector3.up * -posDif.y * Time.deltaTime * moveSensitivity * MOVE_OBJECT_SENSITIVITY);

                    if (outfit.IsFacingForward)
                        outfit.selectedObject.transform.Translate(Vector3.left * -posDif.x * moveSensitivity * Time.deltaTime * MOVE_OBJECT_SENSITIVITY);
                    else if (outfit.IsFacingBackward)
                        outfit.selectedObject.transform.Translate(Vector3.left * posDif.x * moveSensitivity * Time.deltaTime * MOVE_OBJECT_SENSITIVITY);
                    if (outfit.IsFacingLeft)
                        outfit.selectedObject.transform.Translate(Vector3.forward * posDif.x * moveSensitivity * Time.deltaTime * MOVE_OBJECT_SENSITIVITY);
                    else if (outfit.IsFacingRight)
                        outfit.selectedObject.transform.Translate(Vector3.forward * -posDif.x * moveSensitivity * Time.deltaTime * MOVE_OBJECT_SENSITIVITY);

                }
                lastMoveValues = midPoint;
            }
            else
            {
                lastMoveValues = Vector3.zero;
            }
        }

        private void OnMouseDrag()
        {
            
        }

        public void AlignFront()
        {

        }

        public void AlignSide()
        {

        }
    }

}
