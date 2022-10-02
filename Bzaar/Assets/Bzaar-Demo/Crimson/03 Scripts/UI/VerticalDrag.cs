using UnityEngine;
using UnityEngine.EventSystems;
public class VerticalDrag : MonoBehaviour, IDragHandler,IDropHandler
{

    [SerializeField] float maxYVal;
    [SerializeField] float minYVal;
    [SerializeField] RectTransform panelRectTransform;
    RectTransform draggingObjectRectTransform => gameObject.GetComponent<RectTransform>();
    Vector3 velocity;

    Vector3 startingPosition;
    Vector2 panelStartingDeltas;
    Vector3 panelStartingPos;

    private void Start()
    {
        startingPosition = draggingObjectRectTransform.position;
        panelStartingDeltas = panelRectTransform.sizeDelta;
        panelStartingPos = panelRectTransform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateDragPos(eventData);
    }

    public void OnDrop(PointerEventData eventData)
    {
        UpdateDragPos(eventData);
    }

    public void UpdateDragPos(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingObjectRectTransform,
                                                                    eventData.position,
                                                                    eventData.pressEventCamera,
                                                                    out Vector3 GlobalMousePos)) 
        {

            Vector3 verticalPos = GlobalMousePos;

            if (verticalPos.y > maxYVal) verticalPos.y = maxYVal;
         
            verticalPos.x = startingPosition.x;
            verticalPos.z = 0;
            draggingObjectRectTransform.position = verticalPos;


            float posDiff = panelRectTransform.position.y-verticalPos.y;

            Debug.Log($"{panelRectTransform.position.y} | {verticalPos.y} | {posDiff}");

            panelRectTransform.sizeDelta = new Vector2(panelRectTransform.sizeDelta.x, posDiff);
        }


       


        Vector3 ScreenToViewPort(Vector3 screenPosition)
        {
            return new Vector3(
              screenPosition.x / Screen.width,
              screenPosition.y / Screen.height,
              0);
        }
}
}
