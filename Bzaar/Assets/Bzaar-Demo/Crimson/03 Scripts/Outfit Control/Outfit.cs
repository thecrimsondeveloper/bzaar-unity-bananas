using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

using UnityEngine.Rendering;

namespace Bzaar
{
    public class Outfit : MonoBehaviour
    {

        public bool IsFacingForward => facingDir == FacingDir.forward;
        public bool IsFacingBackward => facingDir == FacingDir.backward;
        public bool IsFacingLeft => facingDir == FacingDir.left;
        public bool IsFacingRight => facingDir == FacingDir.right;
        
        public MovementController movementController => gameObject.GetComponent<MovementController>();
        public SculptController sculptController => gameObject.GetComponent<SculptController>();
        public MaterialController materialController => gameObject.GetComponent<MaterialController>();

        [Header("References")]
        public GameObject top;
        public GameObject bottom;
        public GameObject meshParent;

        [Header("Current Clothing")]
        public GameObject spawnedTop;
        public GameObject spawnedBottom;

        [Header("Public Objects")]
        public GameObject selectedObject;

        [Header("Controls")]
        [SerializeField] float rotationSensititivity = 5f;



        enum FacingDir
        {
            forward,
            backward,
            left,
            right,
        }

        FacingDir facingDir => CalculateFacingDir();

        private void Update()
        {
            SelectObject();
            CalculateRotation();

            if (Input.GetKey(KeyCode.Alpha0))
            {
                SaveCurrentOutfit();
            }
        }


        void SelectObject()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hits = Physics.RaycastAll(ray, 100);

                foreach (RaycastHit item in hits)
                {
                    if (item.collider.tag == "Article" && item.collider.name != "PavilionOld")
                    {
                        selectedObject = item.collider.gameObject;
                        selectedObject.GetComponentInParent<Outline>().enabled = true;
                        selectedObject.GetComponent<Outline>().enabled = true;
                    }

                    if (item.collider.transform.parent &&
                        item.collider.transform.parent.tag == "Article")
                    {
                        if (selectedObject) selectedObject.GetComponentInParent<Outline>().enabled = false;

                        selectedObject = item.collider.transform.parent.gameObject;
                        selectedObject.GetComponentInParent<Outline>().enabled = true;
                        selectedObject.GetComponent<Outline>().enabled = true;
                    }
                }
            }
        }

        void CalculateRotation()
        {
            if(App.instance.touchCountLastFrame > 1)            return;
            if(App.instance.lastMousePosition == Vector3.zero)  return;
            if(Input.touchCount != 1)                           return;

            float yChange = App.instance.lastMousePosition.x - Input.mousePosition.x;
            transform.Rotate(Vector3.up * rotationSensititivity * Time.deltaTime * yChange);
        }

        FacingDir CalculateFacingDir()
        {
            float yVal = transform.rotation.eulerAngles.y;

            const float FIRST_CHECK_ANGLE = 45;
            const float SECOND_CHECK_ANGLE = 135;
            const float THIRD_CHECK_ANGLE = 225;
            const float FOURTH_CHECK_ANGLE = 315;

            //facingForward
            bool facingForward = yVal > FOURTH_CHECK_ANGLE &&
                                    yVal < FIRST_CHECK_ANGLE;

            bool facingLeft = yVal >= FIRST_CHECK_ANGLE &&
                                    yVal < SECOND_CHECK_ANGLE;

            bool facingBackward = yVal >= SECOND_CHECK_ANGLE &&
                                    yVal <= THIRD_CHECK_ANGLE;

            bool facingRight = yVal > THIRD_CHECK_ANGLE &&
                                    yVal <= FOURTH_CHECK_ANGLE;

            FacingDir dir = FacingDir.forward;
            if (facingLeft) dir = FacingDir.left;
            if (facingRight) dir = FacingDir.right;
            if (facingBackward) dir = FacingDir.backward;


            return dir;
        }

        public ArticleSave GenerateSaveData(GameObject articleObj)
        {
            ArticleSave article = new ArticleSave();
            article.entry = new Entry();
            article.color = articleObj.GetComponent<MeshRenderer>().material.color;
            article.textureName = articleObj.GetComponent<MeshRenderer>().material.mainTexture.name;


            return article;
        }

        public OutfitSave SaveCurrentOutfit()
        {
            OutfitSave save = new OutfitSave();
            save.outfitName = "OUTFITSAVE" + DateTime.Now.ToString("hh:mm"); 

            save.topEntry = GenerateSaveData(spawnedTop);
            save.bottomEntry = GenerateSaveData(spawnedBottom);
            return save;
        }

    }
}
