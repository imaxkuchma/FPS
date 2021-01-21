using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.AI;


public class AIInformation
{
    public int Health;

    public AIInformation()
    {
        Health = 100;
    }
}

public class AIItemDetection
{
    public GameObject Item;
    public void OnTriggerEnter(Collider other)
    {
        Item = other.gameObject;
        Debug.Log(other.gameObject.tag);
    }
}

public class AIWeapon: MonoBehaviour
{
    public bool IsReady = false;
    public int CountBullets = 0;
    public bool IsTargetIsDead;
    public Transform WeaponPosition;

    public void Fire()
    {

    }

    public void SetWaepon()
    {

    }

    public void SetAttackTarget(Transform target)
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            IsReady = true;
        }
    }

}

public class AIMovement
{
    public NavMeshAgent AIAgent;
    public bool IsMovementCompleted;

    private Transform _currentTarget;

    public void SetTarget(Transform target)
    {
        _currentTarget = target;  
    }

    public void MoveToTarget()
    {
        IsMovementCompleted = Vector3.Distance(AIAgent.transform.position, _currentTarget.position) <= 2.0f;

        AIAgent.SetDestination(_currentTarget.transform.position);
    }
}

public class AIMapHelper
{
    public GameObject[] WayPoints;
    public GameObject[] WeaponPoints;
    public GameObject[] MedicalKitPoints;

    public GameObject GetRandomWayPoint()
    {  
        return WayPoints[UnityEngine.Random.Range(0, WayPoints.Length)];
    }

    public GameObject GetRandomWeaponPoint()
    {
        return WeaponPoints[UnityEngine.Random.Range(0, WeaponPoints.Length)];
    }

    public GameObject GetRandomMedicalKitPoint()
    {
        return MedicalKitPoints[UnityEngine.Random.Range(0, MedicalKitPoints.Length)];
    }
}






