using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    private Light lanternLight;
    public float minIntensity = 0.8f;
    public float maxIntensity = 1.2f;
    public float flickerSpeed = 0.1f;

    void Start()
    {
        lanternLight = GetComponent<Light>();
    }

    void Update()
    {
        if (lanternLight != null)
        {
            lanternLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PerlinNoise(Time.time * flickerSpeed, 0));
        }
    }
}
