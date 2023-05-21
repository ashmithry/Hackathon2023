using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDifficulty
{
    public float movementVelocity;
    public float angularVelocity;
    public float detectionDistance;
    public float shootingAngle;
    public float optimalDist;


    public AIDifficulty(float m, float a, float d, float s, float o)
    {
        movementVelocity = m;
        angularVelocity = a;
        detectionDistance = d;
        shootingAngle = s;
        optimalDist = o;
    }

    public static AIDifficulty[] difficulties;

    public static void Init()
    {
        difficulties = new AIDifficulty[4];

        //Easy
        difficulties[0] = new AIDifficulty(2f, 1f, 10f, 4f, 2f);

        //Normal
        difficulties[1] = new AIDifficulty(5f, 2f, 15f, 6f, 2.5f);

        //Hard
        difficulties[2] = new AIDifficulty(8f, 3f, 25f, 10f, 3.5f);

        //Extreme
        difficulties[3] = new AIDifficulty(11.5f, 4f, 40f, 15f, 4f);
    }
}
