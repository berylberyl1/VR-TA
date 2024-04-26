using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extinguisherSwitch : MonoBehaviour
{
    public ParticleSystem particleSystem;

    private void Start()
    {
        if (particleSystem == null)
        {
            particleSystem = GetComponent<ParticleSystem>();
        }
    }

    private void Update()
    {
        if (particleSystem != null)
        {
            var mainModule = particleSystem.main;

            if (Input.GetMouseButton(0)) // Left-click
            {
                mainModule.startLifetime = 1.0f; // Set Start Lifetime to 1 when left-click is held
            }
            else
            {
                mainModule.startLifetime = 0.0f; // Set Start Lifetime to 0 otherwise
            }
        }
    }
}
