using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AILogic;
using System;
using UnityEngine.AI;

public class AIFSMBehaviour : MonoBehaviour
{
    [SerializeField] private Transform WeaponPosition;

    private FiniteStateMachine<AISharedContext> _finiteStateMachine;

    private Dictionary<Type, BaseState<AISharedContext>> _allAIStates = new Dictionary<Type, BaseState<AISharedContext>>();

    private AISharedContext _aISharedContext;


    // Start is called before the first frame update
    void Start()
    {
        InitFSM();
    }

    // Update is called once per frame
    void Update()
    {
        _finiteStateMachine.Update();
    }

    private void InitFSM()
    {
        _aISharedContext = new AISharedContext();

        _aISharedContext.AIMapHelper.WayPoints = GameObject.FindGameObjectsWithTag("WayPoint");
        _aISharedContext.AIMapHelper.WeaponPoints = GameObject.FindGameObjectsWithTag("WeaponPoint");
        _aISharedContext.AIMapHelper.MedicalKitPoints = GameObject.FindGameObjectsWithTag("MedicalKitPoint");
        _aISharedContext.AIMovement.AIAgent = gameObject.GetComponent<NavMeshAgent>();
        _aISharedContext.AIWeapon.WeaponPosition = WeaponPosition;

        _allAIStates[typeof(SearchEnemyState)] = new SearchEnemyState(_aISharedContext);
        _allAIStates[typeof(SearchWeaponsState)] = new SearchWeaponsState(_aISharedContext);
        _allAIStates[typeof(SearchMedicalKit)] = new SearchMedicalKit(_aISharedContext);

        _finiteStateMachine = new FiniteStateMachine<AISharedContext>();
        _finiteStateMachine.InitStates(_allAIStates);
        _finiteStateMachine.Switch(typeof(SearchEnemyState));
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((_aISharedContext != null))
        {
            _aISharedContext.AIWeapon.OnTriggerEnter(other);
        }      
    }
}
