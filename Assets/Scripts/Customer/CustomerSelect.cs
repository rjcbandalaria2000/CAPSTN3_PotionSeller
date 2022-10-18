using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class CustomerSelect : MonoBehaviour
{
    public OnSelectableObjectClicked onSelectableObjectClickedEvent = new OnSelectableObjectClicked();

    public string objectName; //For Debug Purposes (Can be remove)
  
    public Customer selectCustomer;
    public bool isSelected;

    private void Start()
    {
        isSelected = false;

        if (this.gameObject.GetComponent<Customer>() != null)
        {
            selectCustomer = this.gameObject.GetComponent<Customer>();
            
            objectName = selectCustomer.gameObject.name; //For Debug Purposes (Can be remove)
        }
    }

    private void OnEnable()
    {
        onSelectableObjectClickedEvent.AddListener(OnInteract);
    }

    private void OnDisable()
    {
        onSelectableObjectClickedEvent.RemoveListener(OnInteract);
    }

    public virtual void OnInteract()
    {
        //isSelected = true;
    }
}
