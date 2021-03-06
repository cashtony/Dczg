using UnityEngine;

[AddComponentMenu("NGUI/UI Item Slot")]
public class sdItemSlot : UIItemSlot
{
	public InvEquipment equipment;
	public InvBaseItem.Slot slot;

	override protected InvGameItem observedItem
	{
		get
		{
			return (equipment != null) ? equipment.GetItem(slot) : null;
		}
	}

	/// <summary>
	/// Replace the observed item with the specified value. Should return the item that was replaced.
	/// </summary>

	override protected InvGameItem Replace (InvGameItem item)
	{
		return (equipment != null) ? equipment.Replace(slot, item) : item;
	}
}


