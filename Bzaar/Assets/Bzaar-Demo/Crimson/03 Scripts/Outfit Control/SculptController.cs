using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Bzaar
{
    public class SculptController : MonoBehaviour
    {
        const float STRETCH_SENSITIVITY_MULTIPLIER = 0.1f;
        Outfit outfit => gameObject.GetComponent<Outfit>();

        [Header("Controls")]
        [SerializeField] float stretchSensitivity = 5f;


        float pinchDistanceLastFrame = 0;
        // Update is called once per frame
        void Update()
        {
            Stretching();



        }

        private void LateUpdate()
        {
            SetPinchDistanceLastFrame();
        }

        void SetPinchDistanceLastFrame()
        {
            pinchDistanceLastFrame = 0;
            if (Input.touchCount < 2) return;
            pinchDistanceLastFrame = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
        }

        void Stretching()
        {
            if (!outfit.selectedObject)  return;
            if (Input.touchCount < 2)    return;
            if (pinchDistanceLastFrame == 0) { return; }

            float xDif = Input.GetTouch(0).position.x - Input.GetTouch(1).position.x;
            float yDif = Input.GetTouch(0).position.y - Input.GetTouch(1).position.y;
            bool OnXAxis = Mathf.Abs(xDif) > Mathf.Abs(yDif);

            float distance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            float difference = distance - pinchDistanceLastFrame;

            Debug.Log(OnXAxis);
            
               
            Vector3 changeV = Vector3.zero;
            if (!OnXAxis)
            {
                changeV.y += outfit.selectedObject.transform.localScale.y * (difference * Time.deltaTime);
            }
            else if (outfit.IsFacingForward || outfit.IsFacingBackward)
            {
                changeV.x += outfit.selectedObject.transform.localScale.x * (difference * Time.deltaTime);
            }
            else
            { 
                changeV.z += outfit.selectedObject.transform.localScale.z * (difference * Time.deltaTime);
            }


            outfit.selectedObject.transform.localScale += changeV * stretchSensitivity * STRETCH_SENSITIVITY_MULTIPLIER;
        }
    }
}

