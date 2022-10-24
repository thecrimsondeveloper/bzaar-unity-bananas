using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ArrowGizmos : MonoBehaviour
{
#pragma strict

    float slowDown = 0.2f;
    [SerializeField] GameObject XArrow;
    [SerializeField] GameObject YArrow;
    [SerializeField] GameObject ZArrow;
    [SerializeField] GameObject All;
    enum DirectionType
    {
        X,
        Y,
        Z,
        ALL,
        NONE
    }

    [SerializeField, ReadOnly] DirectionType directionType = DirectionType.ALL;

    //Define the camera that we will use
    //Don't forget to set this to the camera in your game
    Camera cam;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
    }


    float zDistance = 5.0f;

    Vector3 clampPos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.Log("Object: " + hit.collider.name);
                if (hit.collider.gameObject == XArrow)
                {
                    directionType = DirectionType.X;
                }
                else if (hit.collider.gameObject == YArrow)
                {
                    directionType = DirectionType.Y;
                }
                else if (hit.collider.gameObject == ZArrow)
                {
                    directionType = DirectionType.Z;
                }
                else if (hit.collider.gameObject == All)
                {
                    directionType = DirectionType.ALL;
                }
            }
            else
            {
                directionType = DirectionType.NONE;
                Debug.Log("No object");
            }

            //ray cast to find object
            clampPos = transform.position;

        }
        if (Input.GetMouseButton(0))
        {
            var mousePos = Input.mousePosition;
            if (directionType == DirectionType.X)
            {
                Vector3 xClamped = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, zDistance));
                xClamped.z = clampPos.z;
                xClamped.y = clampPos.y;
                this.transform.position = xClamped;
            }
            if (directionType == DirectionType.Y)
            {
                Vector3 yClamped = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, zDistance));
                yClamped.z = clampPos.z;
                yClamped.x = clampPos.x;
                this.transform.position = yClamped;
            }
            if (directionType == DirectionType.Z)
            {
                Vector3 zClamped = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, zDistance));
                zClamped.x = clampPos.x;
                zClamped.y = clampPos.y;
                this.transform.position = zClamped;
            }
            if (directionType == DirectionType.ALL)
            {
                Vector3 zClamped = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, zDistance));
                this.transform.position = zClamped;
            }
        }
    }
    private void OnMouseUp()
    {
        directionType = DirectionType.NONE;
    }

}
