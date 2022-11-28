using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void PlayVFX(ParticleSystem particleEffect)
    {
        particleEffect.Play();
    }

    
    
}
