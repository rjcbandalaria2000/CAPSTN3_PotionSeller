using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{
    private Transform selectedObject;
    private bool isCustomer;
    private List<Material> defaultMaterials = new();
    [SerializeField] private Material highlightMaterial;
    private Material defaultMaterial;
    private Transform _selection;

    [Header("Customer Selection")]
    private Customer customer;

    [Header("Potion Selection")]
    private Potion potion;

    private void Start()
    {

    }

    private void Update()
    {
        if (_selection != null)
        {
            if (_selection.CompareTag("Customer"))
            {
                Renderer selectionRend = _selection.GetComponent<Renderer>();
                //reset back to default material color
                selectionRend.material = defaultMaterial;

                if (_selection.gameObject.GetComponent<SelectableObject>().objectNameUI.activeInHierarchy)
                {
                    _selection.gameObject.transform.GetComponent<SelectableObject>().objectNameUI?.SetActive(false);
                }

                if (_selection.gameObject.GetComponent<SelectableObject>().objectOrderUI != null)
                {
                    _selection.gameObject.GetComponent<SelectableObject>()?.objectOrderUI.SetActive(false);
                }
            }
            if (potion != null && !potion.isSelect)
            {
                Renderer selectionRend = _selection.GetComponent<Renderer>();
                // reset back to default material color
                selectionRend.material = defaultMaterial;
                _selection.gameObject.GetComponent<SelectableObject>().objectNameUI?.SetActive(false);
            }
            if (_selection.CompareTag("Selectable"))
            {
                if (isCustomer && _selection.GetChild(0).childCount > 0)
                {
                    int i = 0;
                    foreach (Transform child in _selection.transform.GetChild(0))
                    {
                        Renderer childSelection;
                        if (child.GetComponent<SkinnedMeshRenderer>())
                        {
                            childSelection = child.GetComponent<SkinnedMeshRenderer>();
                            childSelection.material = defaultMaterials[i];
                            i++;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    defaultMaterials.Clear();
                }
                else if (!isCustomer && _selection.transform.childCount > 0)
                {
                    int i = 0;
                    foreach (Transform child in _selection.transform)
                    {
                        Renderer childSelection = child.GetComponent<Renderer>();
                        childSelection.material = defaultMaterials[i];
                        i++;  
                    }
                    defaultMaterials.Clear();
                }                
                else
                {
                    Renderer selectionRend = _selection.GetComponent<Renderer>();
                    // reset back to default material color
                    selectionRend.material = defaultMaterial;
                }

                

                if (_selection.gameObject.GetComponent<SelectableObject>().objectOrderUI != null)
                {
                    _selection.gameObject.GetComponent<SelectableObject>()?.objectOrderUI.SetActive(false);
                }
            }

            _selection = null;            
        }
        else
        {
            if (selectedObject)
            {
                selectedObject?.GetComponent<SelectableObject>()?.objectNameUI?.SetActive(false);
            }
            
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            if(EventSystem.current.IsPointerOverGameObject()) { return; }
           
            Transform selection = hit.transform;

            //Debug.Log(selection);
            
            if (selection.CompareTag("Selectable")) // FOR OBJECTS
            {
                Renderer selectionRend = selection.GetComponent<Renderer>();
                if (selectionRend != null)
                {                    
                    if (selection.GetComponent<Customer>() && selection.GetChild(0).childCount > 0)
                    {
                        isCustomer = true;
                        foreach (Transform child in selection.transform.GetChild(0))
                        {
                            Renderer childSelection;
                            if (child.GetComponent<SkinnedMeshRenderer>())
                            {
                                childSelection = child.GetComponent<SkinnedMeshRenderer>();
                                defaultMaterials.Add(childSelection.material);
                                childSelection.material = highlightMaterial;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else if (selection.transform.childCount > 0)
                    {
                        isCustomer = false;
                        foreach (Transform child in selection.transform)
                        {
                            Renderer childSelection = child.GetComponent<Renderer>();
                            defaultMaterials.Add(childSelection.material);
                            childSelection.material = highlightMaterial;
                        }
                    }                    
                    else
                    {
                        // store default material color
                        defaultMaterial = selectionRend.material;
                        // set material to selectable (highlighted)
                        selectionRend.material = highlightMaterial;
                    }

                    if (!selection.gameObject.GetComponent<SelectableObject>().objectNameUI.activeInHierarchy)
                    {
                        if (selectedObject)
                        {
                            selectedObject?.gameObject.GetComponent<SelectableObject>().objectNameUI.SetActive(false);
                        }
                        
                        selection.gameObject.GetComponent<SelectableObject>().objectNameUI.SetActive(true);
                        selectedObject = selection;
                    }

                    if (selection.gameObject.GetComponent<SelectableObject>().objectOrderUI != null)
                    {                        
                        selection.gameObject.GetComponent<SelectableObject>()?.objectOrderUI.SetActive(true);
                    }

                }
                _selection = selection;
                
                // outputs click - TEMPORARY? (should be in 3D object base class or child class)
                if (Input.GetMouseButtonDown(0))
                {
                    // Output name
                    //Debug.Log(hit.transform.name);
                    
                    // Fire an event
                    selection.gameObject.GetComponent<SelectableObject>()?.onSelectableObjectClickedEvent.Invoke();
                }
            }

            //---------------------------------------------------------------------------------------------------------------------
           
            if (selection.CompareTag("Customer"))
            {
                Renderer selectionRend = selection.GetComponent<Renderer>();
                if (selectionRend != null)
                {
                    defaultMaterial = selectionRend.material;
                    selectionRend.material = highlightMaterial;
                    selection.gameObject.GetComponent<SelectableObject>()?.objectNameUI?.SetActive(true);
                    
                    if (selection.gameObject.GetComponent<SelectableObject>().objectOrderUI != null)
                    {
                        selection.gameObject.GetComponent<SelectableObject>()?.objectOrderUI.SetActive(true);
                    }
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
                        selectionRend.material = highlightMaterial;
                    }

                    selection.gameObject.GetComponent<SelectableObject>()?.objectNameUI?.SetActive(true);

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