using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material selectMaterial;
    private Material defaultMaterial;
    private Transform _selection;

    [Header("Customer Selection")]
    private Customer customer;

    [Header("Potion Selection")]
    private Potion potion;


    private void Update()
    {
        if (_selection != null)
        {
            if (customer != null && !customer.isSelect)
            {
                Renderer selectionRend = _selection.GetComponent<Renderer>();
                // reset back to default material color
                selectionRend.material = defaultMaterial;
                // resets _selection to null
                _selection.gameObject.GetComponent<SelectableObject>().objectNameUI.SetActive(false);
            }
            if (potion != null && !potion.isSelect)
            {
                Renderer selectionRend = _selection.GetComponent<Renderer>();
                // reset back to default material color
                selectionRend.material = defaultMaterial;
                // resets _selection to null
                _selection.gameObject.GetComponent<SelectableObject>().objectNameUI.SetActive(false);
            }
            if (_selection.CompareTag("Selectable"))
            {
                Renderer selectionRend = _selection.GetComponent<Renderer>();
                // reset back to default material color
                selectionRend.material = defaultMaterial;
                // resets _selection to null
                _selection.gameObject.GetComponent<SelectableObject>().objectNameUI.SetActive(false);
            }

            _selection = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            if(EventSystem.current.IsPointerOverGameObject()) { return; }
           
            Transform selection = hit.transform;
            
            if (selection.CompareTag("Selectable")) // FOR OBJECTS
            {
                Renderer selectionRend = selection.GetComponent<Renderer>();
                if (selectionRend != null)
                {
                    // store default material color
                    defaultMaterial = selectionRend.material;
                    // set material to selectable (highlighted)
                    selectionRend.material = highlightMaterial;
                    selection.gameObject.GetComponent<SelectableObject>()?.objectNameUI.SetActive(true);
                }
                _selection = selection;
                
                // outputs click - TEMPORARY? (should be in 3D object base class or child class)
                if (Input.GetMouseButtonDown(0))
                {
                    // Output name
                    // Debug.Log(hit.transform.name);
                    
                    // Fire an event
                    selection.gameObject.GetComponent<SelectableObject>()?.onSelectableObjectClickedEvent.Invoke();
                }
            }

            //---------------------------------------------------------------------------------------------------------------------
           
            customer = hit.transform.gameObject.GetComponent<Customer>();
            if (selection.CompareTag("Customer"))
            {
                Renderer selectionRend = customer.GetComponent<Renderer>();

                if (selectionRend != null)
                {
                    if(!customer.isSelect)
                    {
                        defaultMaterial = selectionRend.material;
                        selectionRend.material = highlightMaterial;
                    }
                    else
                    {
                        selectionRend.material = selectMaterial;
                    }
                   
                    selection.gameObject.GetComponent<SelectableObject>()?.objectNameUI.SetActive(true);

                }
                _selection = selection;

                // outputs click - TEMPORARY? (should be in 3D object base class or child class)
                if (Input.GetMouseButtonDown(0))
                {
                    if(customer!=null && !customer.isSelect)
                    {
                        customer.isSelect = true;
                    }
                 
                    selection.gameObject.GetComponent<SelectableObject>()?.onSelectableObjectClickedEvent.Invoke();
                }

                if(Input.GetMouseButtonDown(1))
                {
                    if (customer != null && customer.isSelect)
                    {
                        customer.isSelect = false;
                    }
                
                    selection.gameObject.GetComponent<SelectableObject>()?.onSelectableObjectClickedEvent.Invoke();
                }
            }

            //---------------------------------------------------------------------------------------------------------------------

            potion = hit.transform.gameObject.GetComponent<Potion>();
            if (selection.CompareTag("Potion"))
            {
                Renderer selectionRend = potion.GetComponent<Renderer>();

                if (selectionRend != null)
                {
                    if (!potion.isSelect)
                    {
                        defaultMaterial = selectionRend.material;
                        selectionRend.material = highlightMaterial;
                    }
                    else
                    {
                        selectionRend.material = selectMaterial;
                    }

                    selection.gameObject.GetComponent<SelectableObject>()?.objectNameUI.SetActive(true);

                }
                _selection = selection;

                // outputs click - TEMPORARY? (should be in 3D object base class or child class)
                if (Input.GetMouseButtonDown(0))
                {
                    if (potion != null && !potion.isSelect)
                    {
                        potion.isSelect = true;
                    }

                    selection.gameObject.GetComponent<SelectableObject>()?.onSelectableObjectClickedEvent.Invoke();
                }

                if (Input.GetMouseButtonDown(1))
                {
                    if (potion != null && potion.isSelect)
                    {
                        potion.isSelect = false;
                    }

                    selection.gameObject.GetComponent<SelectableObject>()?.onSelectableObjectClickedEvent.Invoke();
                }
            }
        }        
    }
}