using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#region Selectable Object Events
public class OnSelectableObjectClicked : UnityEvent { };
#endregion

#region OrderManager Events
public class OnCustomerOrder : UnityEvent<PotionScriptableObject> { };
#endregion

#region Inventory Events
public class AddItemEvent : UnityEvent<GameObject, IngredientScriptableObject, string, int> { };
public class RemoveItemEvent : UnityEvent<GameObject, IngredientScriptableObject, string, int> { };
#endregion