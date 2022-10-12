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
public class AddItemEvent : UnityEvent<string, int> { };
public class RemoveItemEvent : UnityEvent<string, int> { };
#endregion

#region TimeManager Events
public class TimeChangedEvent : UnityEvent <int, int> { }
public class HourChangedEvent : UnityEvent <int> { }
public class DayEndedEvent : UnityEvent<int> { }
#endregion

#region Quest Events
public class QuestCompletedEvent : UnityEvent<Quest> { }
public class AddQuestEvent : UnityEvent<Quest> { }
public class RemoveQuestEvent : UnityEvent<Quest> { }
public class RemoveAllQuestsEvent : UnityEvent { }
#endregion

#region StoreLevel Events
public class OnGainExp : UnityEvent <int> { };
public class OnRefreshLevelUI : UnityEvent { };

#endregion