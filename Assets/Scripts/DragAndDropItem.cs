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
        UIManager.Instance.currentCardChosen = this.gameObject.GetComponent<cardClass>();
        UIManager.Instance.playSound(this.gameObject);
//		GetComponent<CanvasGroup>().blocksRaycasts = false;

		//GetComponent<CanvasGroup>().blocksRaycasts = false;

	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		transform.position = eventData.position;
	}


	#endregion

	#region IEndDragHandler implementation

	private Vector2 origPos;


	#endregion

	#region IEndDragHandler implementation


	public void OnEndDrag (PointerEventData eventData)
	{
		itemBeingDragged = null;
		//GetComponent<CanvasGroup>().blocksRaycasts = true;
		if(transform.parent == startParent){
			transform.position = startPosition;
			//transform.localPosition = new Vector3 (0, 0, 0);
		}

	}

	#endregion




  
}