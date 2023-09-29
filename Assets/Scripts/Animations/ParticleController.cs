using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController
{

    GameObject[] CPlacingFx;
    GameObject[] WPlacingFx;
    public string CPlacingTag = "CPlacingFX";
    public string WPlacingTag = "WPlacingFX";
    int i_c;
    int i_w;

    public ParticleController()
    {
        i_c = 0;
        i_w = 0;
        CPlacingFx = GameObject.FindGameObjectsWithTag(CPlacingTag);
        WPlacingFx = GameObject.FindGameObjectsWithTag(WPlacingTag);
    }

    public void PlaceParticleFX(float x, float y, bool placedCorrectly)
    {
        if (CPlacingFx[i_c] && placedCorrectly)
        {
            CPlacingFx[i_c].transform.position = new Vector3(x, y, -2f);
            Particle particle = CPlacingFx[i_c].GetComponent<Particle>();

            if (particle)
            {
                particle.Play();
            }
            i_c++;
            if (i_c == 4) i_c = 0;
        }
        else if (WPlacingFx[i_w] && !placedCorrectly)
        {
            WPlacingFx[i_w].transform.position = new Vector3(x, y, -2f);
            Particle particle = WPlacingFx[i_w].GetComponent<Particle>();

            if (particle)
            {
                particle.Play();
            }
            i_w++;
            if (i_w == 4) i_w = 0;
        }

    }



}
