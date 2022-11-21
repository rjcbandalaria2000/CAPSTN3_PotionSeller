using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#region Selectable Object Events
public class OnSelectableObjectClicked : UnityEvent { };
#endregion

#region OrderManager Events
public class OnCustomerOrder : UnityEvent<PotionScriptableObject, Customer> { };
#endregion

#region Inventory Events
public class AddItemEvent : UnityEvent<ScriptableObject, int> { };
public class RemoveItemEvent : UnityEvent<ScriptableObject, int> { };
#endregion

#region TimeManager Events
public class TimeChangedEvent : UnityEvent <int, int> { }
public class HourChangedEvent : UnityEvent <int> { }
public class DayEndedEvent : UnityEvent<int> { }
public class PauseGameTime : UnityEvent<bool> { }
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

#region OrderEvent

public class OnOrderComplete : UnityEvent { };

#endregion

#region Onboarding Events
public class OnboardingClickEvent : UnityEvent { };

#endregion

#region Prep Station Events

public class OnIncompleteIngredientPotion : UnityEvent { };
public class OnCompleteIngredientPotion : UnityEvent { };

#endregion

#region Ending Condition Events

public class OnGameWin : UnityEvent { };
public class OnGameLose : UnityEvent { };

public class OnGameFinish: UnityEvent { };

#endregion

#region OnSceneChange Events


#endregion