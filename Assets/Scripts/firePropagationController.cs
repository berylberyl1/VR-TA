using System.Collections;
using System.Collections.Generic;
using FernandoOleaDev.FyreSystem;
using UnityEngine;

public class firePropagationController : MonoBehaviour
{
    public GameObject burnableObject;
    public GameObject burnableObjectLight;
    [SerializeField] BurnableObject burnableObjectScript;
    public Collider colliderObject;
    public Collider colliderExtinguisher;
    public float extinguishPercentage = 0.1f;
    public float extinguishTime = 10f;
    public bool isExtinguishing = false;
    public bool isExtinguished = false;

    // Start is called before the first frame update
    void Start()
    {
        burnableObjectScript.flamePropagationSeconds = 10.0f;
        burnableObjectScript.combustionSeconds = 100.0f;
        burnableObjectScript.coolingSeconds = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (burnableObjectLight == null)
        {
            burnableObjectLight = burnableObject.transform.Find("Test Barrel - Light").gameObject;
        }
        if (!isExtinguished && extinguishPercentage < 1f && colliderObject && colliderExtinguisher && colliderObject.bounds.Intersects(colliderExtinguisher.bounds))
        {
            isExtinguishing = true;

            float increment = Time.deltaTime / extinguishTime;
            extinguishPercentage = Mathf.Clamp01(extinguishPercentage + increment);

            if (extinguishPercentage == 1f)
            {
                isExtinguished = true;
                isExtinguishing = false;
                burnableObjectScript.flamePropagationSeconds = 0.2f;
                burnableObjectScript.combustionSeconds = 0.2f;
                burnableObjectScript.generateLight = false;
                Destroy(burnableObjectLight);
            }
        }
        else if (!isExtinguished && !isExtinguishing && extinguishPercentage > 0f && colliderObject && colliderExtinguisher && !colliderObject.bounds.Intersects(colliderExtinguisher.bounds))
        {
            float decrement = Time.deltaTime / extinguishTime;
            extinguishPercentage = Mathf.Clamp01(extinguishPercentage - decrement);
        }
        
    }

    
}
