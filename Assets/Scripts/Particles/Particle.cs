using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{

    public ParticleSystem[] allParticles;

    void Start()
    {
        allParticles = GetComponentsInChildren<ParticleSystem>();
    }

    public void Play()
    {
        foreach (ParticleSystem ps in allParticles)
        {
            ps.Stop();
            ps.Play();
        }
    }

}
