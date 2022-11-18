using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform camera;

    private void OnEnable()
    {
        camera = Camera.main.transform;
    }

    private void OnDisable()
    {
        camera = null;
    }
    // Update is called once per frame
    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        this.transform.LookAt(camera);
    }
}
