using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public class TouchController : MonoBehaviour
{
    public static TouchController instance;

    [Header("Gesture Attributes")]
    [SerializeField] float holdTouchDuration = 0.1f;


    [Header("Debugging")]
    [SerializeField] bool displayInspectorData = false;

    [Header("   Data")]
    [ShowIf("displayInspectorData"), SerializeField] private int touchCountLastFrame = 0;
    [ShowIf("displayInspectorData"), SerializeField] Vector3 lastMousePosition;
    [ShowIf("displayInspectorData"), SerializeField] private float mouseDownTime = 0;
    [ShowIf("displayInspectorData"), SerializeField] bool holdReleased = false;
    [ShowIf("displayInspectorData"), SerializeField] bool tapReleased = false;

    #region Getters
    public bool MouseHolding => mouseDownTime >= holdTouchDuration;
    public int TouchCountLastFrame => touchCountLastFrame;
    public Vector3 LastMousePosition => lastMousePosition;
    public bool TapReleased => tapReleased;
    public bool HoldReleased => holdReleased;
    #endregion

    [Space(20)]
    [Header("Events")]
    public UnityEvent onTouchReleased;

    private void Awake()
    {
        if (instance)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
        Input.simulateMouseWithTouches = true;
    }


    private void Update()
    {
        if (Input.touchCount == 0 && touchCountLastFrame > 0)
        {
            holdReleased = MouseHolding;
            tapReleased = !MouseHolding;

            onTouchReleased.Invoke();
        }
        else
        {
            holdReleased = false;
            tapReleased = false;
        }
    }
    private void LateUpdate()
    {
        touchCountLastFrame = Input.touchCount;
        lastMousePosition = Input.touchCount > 0 ? Input.mousePosition : Vector3.zero;
        mouseDownTime = Input.touchCount > 0 ? mouseDownTime + Time.deltaTime : 0;
    }

    void TouchReleased()
    {

    }


}
