using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial;
    private Material defaultMaterial;
    private Transform _selection;

    private void Update()
    {
        if (_selection != null)
        {
            Renderer selectionRend = _selection.GetComponent<Renderer>();
            // reset back to default material color
            selectionRend.material = defaultMaterial;
            // resets _selection to null
            _selection = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            Transform selection = hit.transform;
            if (selection.CompareTag("Selectable"))
            {
                Renderer selectionRend = selection.GetComponent<Renderer>();
                if (selectionRend != null)
                {
                    // store default material color
                    defaultMaterial = selectionRend.material;
                    // set material to selectable (highlighted)
                    selectionRend.material = highlightMaterial;
                }
                _selection = selection;
                
                // outputs click - TEMPORARY (should be in 3D object base class or child class)
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log(hit.transform.name);
                }
            }            
        }        
    }
}