using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGroup
{
    private GameObject[] particles;
    private int currentIndex = 0;

    public ParticleGroup(string tag)
    {
        
        particles = GameObject.FindGameObjectsWithTag(tag);
        Debug.Log(tag + " " + particles.Length);
    }

    public void PlayParticleFX(float x, float y)
    {
        if (currentIndex < particles.Length)
        {
            GameObject particleObject = particles[currentIndex];
            if (particleObject != null)
            {
                particleObject.transform.position = new Vector3(x, y, -2f);
                Particle particle = particleObject.GetComponent<Particle>();
                if (particle != null)
                {
                    particle.Play();
                }
                currentIndex = (currentIndex + 1) % particles.Length;
            }
        }
    }
}
