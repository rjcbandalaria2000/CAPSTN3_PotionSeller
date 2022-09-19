using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#region Selectable Object Events
public class OnSelectableObjectClicked : UnityEvent { };
#endregion

#region Inventory Events
public class AddItemEvent : UnityEvent<GameObject, IngredientScriptableObject, string, int> { };
public class RemoveItemEvent : UnityEvent<GameObject, IngredientScriptableObject, string, int> { };
#endregion

#region TimeManager Events
public class TimeChangedEvent : UnityEvent <int, int> { }
public class HourChangedEvent : UnityEvent <int> { }
#endregion