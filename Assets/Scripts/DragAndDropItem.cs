using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragAndDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler {
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
        this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		transform.position = eventData.position;
	}


    #endregion


    #region IEndDragHandler implementation

    /// <summary>
    /// When this object is dropped, it will check if it's a note from the scale
    /// if it is, it will add itself to the "pool" of notes, and delete the game object
    /// 
    /// Otherwise, if it's a note from teh player's hand it will snap back to their hand
    /// </summary>
    public void OnEndDrag (PointerEventData eventData)
	{
		itemBeingDragged = null;

        this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        // If the location where this object is dropped already has a card in it, rebound this card to hand
        if (transform.parent == startParent)
        {
            cardClass temp = gameObject.GetComponent<cardClass>();
            if (temp.isNote())
            {
                if(!GameMaster.Instance.getPlayerHand().handFull())
                {
                    GameMaster.Instance.getPlayerHand().returnCardToHand(gameObject);
                }
                else
                {
                    GameMaster.Instance.insertCard(temp.cardNum);
                    Destroy(gameObject);
                }
            }
            else transform.position = startPosition;
		}

	}

    #endregion

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropping Item...(Unexpected Case)");
        if (transform.parent == startParent)
        {
            cardClass temp = gameObject.GetComponent<cardClass>();
            if (temp.isNote())
            {
                if (!GameMaster.Instance.getPlayerHand().handFull())
                {
                    GameMaster.Instance.getPlayerHand().returnCardToHand(gameObject);
                }
                else
                {
                    GameMaster.Instance.insertCard(temp.cardNum);
                    Destroy(this);
                }
            }
            else transform.position = startPosition;
        }
    }


    }