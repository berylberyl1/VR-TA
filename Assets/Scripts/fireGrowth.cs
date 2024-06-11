using UnityEngine;

public class FireGrowth : MonoBehaviour
{
    public ParticleSystem fireParticleSystem;
    public Collider colliderFire;
    public Collider colliderFirePropagation;
    public Collider colliderExtinguisher;

    public float startSizeMinStart = 1f;
    public float startSizeMinEnd = 5f;
    public float startSizeMaxStart = 1f;
    public float startSizeMaxEnd = 10f;
    public float emissionRateStart = 3f;
    public float emissionRateEnd = 10f;
    public float growthDuration = 10f; // Time it takes to grow to 100%
    public float currentPercentage = 0.1f; // The current growth percentage
    public bool isGrowing = true; // Controls if the fire should be growing
    public bool isDecreasing = false; // Controls if the percentage is decreasing

    private void Start()
    {
        if (colliderFirePropagation == null)
        {
            colliderFirePropagation = GetComponent<Collider>();
        }

        if (colliderFirePropagation != null)
        {
            colliderFirePropagation.enabled = false;
        }
    }

    void Update()
    {
        if (colliderFire && colliderExtinguisher && colliderFire.bounds.Intersects(colliderExtinguisher.bounds))
        {
            isDecreasing = true;
        }
        else
        {
            isDecreasing = false;
        }

        if (isDecreasing) // Decrease the percentage if colliderFire and colliderExtinguisher overlap
        {
            float decrement = 2 * (Time.deltaTime / growthDuration);
            currentPercentage = Mathf.Clamp01(currentPercentage - decrement);

            InterpolateValues(currentPercentage);

            if (currentPercentage <= 0f)
            {
                isGrowing = false;
            }
        }
        else if (isGrowing) // If not decreasing, and still growing, increase the percentage
        {
            float increment = Time.deltaTime / growthDuration;
            currentPercentage = Mathf.Clamp01(currentPercentage + increment);

            InterpolateValues(currentPercentage);

            if (currentPercentage >= 0.8f) // Enable the propagation collider when percentage is 80% or more
            {
                if (colliderFirePropagation != null && !colliderFirePropagation.enabled)
                {
                    colliderFirePropagation.enabled = true;
                    Debug.Log("Fire propagation enabled");
                }
            }

            if (currentPercentage >= 1f) // Set growth to false when it reaches 100%
            {
                isGrowing = false;
                Debug.Log("Fire growth complete");
            }
        }
    }

    void InterpolateValues(float percentage)
    {
        // Interpolate start size based on the percentage
        float interpolatedStartSizeMin = Mathf.Lerp(startSizeMinStart, startSizeMinEnd, percentage);
        float interpolatedStartSizeMax = Mathf.Lerp(startSizeMaxStart, startSizeMaxEnd, percentage);

        var mainModule = fireParticleSystem.main;
        mainModule.startSizeX = new ParticleSystem.MinMaxCurve(interpolatedStartSizeMin, interpolatedStartSizeMax);

        // Interpolate emission rate based on the percentage
        float interpolatedEmissionRate = Mathf.Lerp(emissionRateStart, emissionRateEnd, percentage);

        var emissionModule = fireParticleSystem.emission;
        emissionModule.rateOverTime = interpolatedEmissionRate;
    }
}
