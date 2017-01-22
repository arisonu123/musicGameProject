using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragAndDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static GameObject itemBeingDragged;
	Vector3 startPosition;
	Transform startParent;

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
//		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		transform.position = eventData.position;
	}

<<<<<<< HEAD
	#endregion

	#region IEndDragHandler implementation
=======
	private Vector2 origPos;
>>>>>>> origin/master

	public void OnEndDrag (PointerEventData eventData)
	{
		itemBeingDragged = null;
		//GetComponent<CanvasGroup>().blocksRaycasts = true;
		if(transform.parent == startParent){
			transform.position = startPosition;
		}
	}

<<<<<<< HEAD
	#endregion
=======
    public void OnBeginDrag(PointerEventData eventData)
    {
		origPos = gameObject.transform.position;
        //sourceCell = GetComponentInParent<DragAndDropCell>();                       // Remember source cell
        draggedItem = this;                                                        // Set as dragged item
>>>>>>> origin/master


<<<<<<< HEAD
=======
        if (OnItemDragStartEvent != null)
        {
            OnItemDragStartEvent(this);                                             // Notify all about item drag start
        }
    }
		
    public void OnDrag(PointerEventData data)
    {
            UIManager.Instance.playSound(this.gameObject);
            this.transform.position = Input.mousePosition;                          // Item's icon follows to cursor
    }
		
    public void OnEndDrag(PointerEventData eventData)
    {
		gameObject.transform.position = origPos;
        if (icon != null)
        {
            Destroy(icon);                                                          // Destroy icon on item drop
        }
        MakeVisible(true);                                                          // Make item visible in cell
        if (OnItemDragEndEvent != null)
        {
            OnItemDragEndEvent(this);                                               // Notify all cells about item drag end
        }
        draggedItem = null;
        icon = null;
        sourceCell = null;
    }
>>>>>>> origin/master

}