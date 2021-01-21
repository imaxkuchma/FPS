using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AILogic
{
    public class SearchMedicalKit : BaseState<AISharedContext>
    {
        public SearchMedicalKit(AISharedContext sharedContext) : base(sharedContext)
        {
        }

        private GameObject _currentTarget;

        public override void OnStateEnter()
        {
            if (_sharedContext == null)
                return;

            _currentTarget = _sharedContext.AIMapHelper.GetRandomMedicalKitPoint();
            _sharedContext.AIMovement.SetTarget(_currentTarget.transform);
        }

        public override void Execute()
        {
            if (_sharedContext.AIMovement.IsMovementCompleted)
            {
                _currentTarget = _sharedContext.AIMapHelper.GetRandomMedicalKitPoint();
                _sharedContext.AIMovement.SetTarget(_currentTarget.transform);
            }

            if (_sharedContext.AIInformation.Health <= 30)
                _stateSwitcher.Switch(typeof(SearchMedicalKit));

            if (_sharedContext.AIWeapon.IsReady)
                _stateSwitcher.Switch(typeof(SearchEnemyState));

            _sharedContext.AIMovement.MoveToTarget();
        }
    }
}



