
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace AILogic
{

    public class SearchEnemyState : BaseState<AISharedContext>
    {
        public SearchEnemyState(AISharedContext sharedContext) : base(sharedContext)
        {
        }

        private GameObject _currentTarget;

        public override void OnStateEnter()
        {
           if (_sharedContext == null)
                return;
           
            _currentTarget = _sharedContext.AIMapHelper.GetRandomWayPoint();
            _sharedContext.AIMovement.SetTarget(_currentTarget.transform);
        }

        public override void Execute()

        {
            if (!_sharedContext.AIWeapon.IsReady)
                _stateSwitcher.Switch(typeof(SearchWeaponsState));

            if (_sharedContext.AIInformation.Health <= 30)
                _stateSwitcher.Switch(typeof(SearchMedicalKit));

            if (!_sharedContext.AIMovement.IsMovementCompleted) 
            {
                
                
            }
            else
            {
                _currentTarget = _sharedContext.AIMapHelper.GetRandomWayPoint();
                _sharedContext.AIMovement.SetTarget(_currentTarget.transform);               
            }

            _sharedContext.AIMovement.MoveToTarget();
        }
    }
}