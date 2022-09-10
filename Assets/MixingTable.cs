using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixingTable : MonoBehaviour
{ 
    private Interactable interactable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnInteract(GameObject player = null)
    {
        Debug.Log("Interacted");
    }

    public void OnEndInteract(GameObject player = null)
    {

    }

    private void OnMouseDown()
    {
        OnInteract();
    }

}
