using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ExtinguisherSwitch : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public Collider assignedCollider;
    private XRGrabInteractable grabInteractable;
    public bool colliderEnabled = false;
    public bool extinguisherOn = false;
    
    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(EnableExtinguisher);
        grabInteractable.deactivated.AddListener(DisableExtinguisher);
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
            assignedCollider.enabled = colliderEnabled;
        }
    }

    private void Update()
    {
        if (particleSystem != null)
        {
            var mainModule = particleSystem.main;

            if (extinguisherOn)
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

    public void EnableExtinguisher(ActivateEventArgs args)
    {
        extinguisherOn = true;
    }

    public void DisableExtinguisher(DeactivateEventArgs args)
    {
        extinguisherOn = false;
    }
}
