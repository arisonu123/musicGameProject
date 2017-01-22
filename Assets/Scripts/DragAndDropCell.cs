using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class DragAndDropCell: MonoBehaviour, IDropHandler {

	public GameObject item {
		get {
			if(transform.childCount>0){
				return transform.GetChild (0).gameObject;
			}
			return null;
		}
	}

	#region IDropHandler implementation
	public void OnDrop (PointerEventData eventData)
	{
		if(!item){
			DragAndDropItem.itemBeingDragged.transform.SetParent (transform);
			GameObject note = PhotonNetwork.Instantiate("Note", transform.position, Quaternion.identity, 0) as GameObject;
			note.transform.SetParent (transform); //note.transform.parent = transform;
			note.gameObject.SetActive (true);
			Destroy (item);

			//ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject,null,(x,y) => x.HasChanged ());
		}
	}
	#endregion
}