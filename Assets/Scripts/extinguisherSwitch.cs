using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguisherSwitch : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public Collider assignedCollider;

    private void Start()
    {
        if (particleSystem == null)
        {
            particleSystem = GetComponent<ParticleSystem>();
        }
        if (assignedCollider == null)
        {
            assignedCollider = GetComponent<Collider>();
        }
        if (assignedCollider != null)
        {
            assignedCollider.enabled = false;
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

                if (assignedCollider != null)
                {
                    assignedCollider.enabled = true; // Enable the collider
                }
            }
            else
            {
                mainModule.startLifetime = 0.0f; // Set Start Lifetime to 0 otherwise

                if (assignedCollider != null)
                {
                    assignedCollider.enabled = false; // Disable the collider
                }
            }
        }
    }
}
