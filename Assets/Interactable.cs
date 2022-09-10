using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent<GameObject> EvtInteract;
    public UnityEvent<GameObject> EvtEndInteract;

    public void Interact(GameObject player = null)
    {
        EvtInteract.Invoke(player);
    }

    public void EndInteract(GameObject player = null)
    {
        EvtEndInteract.Invoke(player);
    }
}
