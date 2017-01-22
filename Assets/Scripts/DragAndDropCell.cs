using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;



public class DragAndDropCell : MonoBehaviour, IDropHandler
{
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    #region IDropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {
        // Don't allow notes to be moved on the scale, and dropped section must be free
        if (!item && !DragAndDropItem.itemBeingDragged.transform.GetComponent<cardClass>().isNote()) 
        {
            // Turn the given card into a "note" on the scale, and remove it from the player's hand
            DragAndDropItem.itemBeingDragged.transform.SetParent(transform);
            GameMaster.Instance.getPlayerHand().removeCardFromHand(DragAndDropItem.itemBeingDragged.GetComponent<cardClass>().getPlaceInHand());
            // Straightens the note
            DragAndDropItem.itemBeingDragged.transform.GetComponent<RectTransform>().rotation = new Quaternion(0, 0, 0, 1);
            UIManager.Instance.placeCardOnScale(item);
            
            //GameObject note = PhotonNetwork.Instantiate("Note", transform.position, Quaternion.identity, 0) as GameObject;
            //note.transform.SetParent(transform); 
         
            //Destroy(item);

            //ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject,null,(x,y) => x.HasChanged ());
        }
    }
    #endregion

}