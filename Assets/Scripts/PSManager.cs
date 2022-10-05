using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSManager : MonoBehaviour
{
    // Create an accessible reference to this singleton
    public static PSManager Instance { get; private set; }

    // Ensure there are no other instances of FeedbackFX in our scene
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Spawn "ps" at "position" with (optional) "rotation", and play it
    public void SpawnPS(ParticleSystem ps, Vector3 position, Quaternion rotation = new Quaternion())
    {
        // If a rotation was not assigned, use the default rotation of the ps.gameObject
        if (rotation == new Quaternion())
            rotation = ps.transform.rotation;

        // Create a new "ps" gameObject at "position" and "rotation" and play it
        ParticleSystem _currentPS;
        _currentPS = Instantiate(ps);
        _currentPS.transform.position = position;
        _currentPS.transform.rotation = rotation;
        _currentPS.Play();
    }
}
