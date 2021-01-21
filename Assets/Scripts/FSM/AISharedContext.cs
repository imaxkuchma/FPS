using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISharedContext 
{
    public readonly AIMovement AIMovement;
    public readonly AIMapHelper AIMapHelper;
    public readonly AIWeapon AIWeapon;
    public readonly AIItemDetection AIItemDetection;
    public readonly AIInformation AIInformation;

    public AISharedContext()
    {
        AIMapHelper = new AIMapHelper();
        AIMovement = new AIMovement();
        AIWeapon = new AIWeapon();
        AIItemDetection = new AIItemDetection();
        AIInformation = new AIInformation();
    }
}
