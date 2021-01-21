using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AILogic
{
    public class SearchWeaponsState : BaseState<AISharedContext>
    {
        public SearchWeaponsState(AISharedContext sharedContext) : base(sharedContext)
        {
        }

        private GameObject _currentTarget;

        public override void OnStateEnter()
        {
            if (_sharedContext == null)
                return;

            _currentTarget = _sharedContext.AIMapHelper.GetRandomWeaponPoint();
            _sharedContext.AIMovement.SetTarget(_currentTarget.transform);
        }

        public override void Execute()
        {
            if (_sharedContext.AIMovement.IsMovementCompleted) 
            { 
                _currentTarget = _sharedContext.AIMapHelper.GetRandomWeaponPoint();
                _sharedContext.AIMovement.SetTarget(_currentTarget.transform);
            }

            if (_sharedContext.AIWeapon.IsReady)
                _stateSwitcher.Switch(typeof(SearchEnemyState));

            _sharedContext.AIMovement.MoveToTarget();
        }
    }
}


