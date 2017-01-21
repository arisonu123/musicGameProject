using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// Every "drag and drop" item must contain this script
/// </summary>
[RequireComponent(typeof(Image))]
public class DragAndDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    static public DragAndDropItem draggedItem;                                      // Item that is dragged now
    static public GameObject icon;                                                  // Icon of dragged item
    static public DragAndDropCell sourceCell;                                       // From this cell dragged item is

    public delegate void DragEvent(DragAndDropItem item);
    static public event DragEvent OnItemDragStartEvent;                             // Drag start event
    static public event DragEvent OnItemDragEndEvent;                               // Drag end event

    public void OnBeginDrag(PointerEventData eventData)
    {
        //sourceCell = GetComponentInParent<DragAndDropCell>();                       // Remember source cell
        draggedItem = this;                                                        // Set as dragged item

        //Canvas canvas = GetComponentInParent<Canvas>();                             // Get parent canvas

        if (OnItemDragStartEvent != null)
        {
            OnItemDragStartEvent(this);                                             // Notify all about item drag start
        }
    }
		
    public void OnDrag(PointerEventData data)
    {

            this.transform.position = Input.mousePosition;                          // Item's icon follows to cursor
    }
		
    public void OnEndDrag(PointerEventData eventData)
    {
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

    public void MakeRaycast(bool condition)
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.raycastTarget = condition;
        }
    }
		
    public void MakeVisible(bool condition)
    {
        GetComponent<Image>().enabled = condition;
    }
}
