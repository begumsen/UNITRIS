using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController
{

    public ParticleGroup CPlacingFxGroup;
    public ParticleGroup WPlacingFxGroup;
    public ParticleGroup CBurstFxGroup;
    public ParticleGroup WBurstFxGroup;

    public ParticleController()
    {
        CPlacingFxGroup = new ParticleGroup("CPlacingFX");
        WPlacingFxGroup = new ParticleGroup("WPlacingFX");
        CBurstFxGroup = new ParticleGroup("CBurstFX");
        WBurstFxGroup = new ParticleGroup("WBurstFX");

    }

    public void PlaceParticleFX(float x, float y, bool placedCorrectly)
    {
        if (placedCorrectly)
        {
            CPlacingFxGroup.PlayParticleFX(x, y);
            CBurstFxGroup.PlayParticleFX(x, y);
        }
        else
        {
            WPlacingFxGroup.PlayParticleFX(x, y);
            WBurstFxGroup.PlayParticleFX(x, y);
        }
    }


}
