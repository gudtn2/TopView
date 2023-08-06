using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterController
{
    public class MoveState : BaseState
    {
        public const float CONVERT_UNIT_VALUE = 0.01f;
        public const float DEFAULT_CONVERT_MOVESPEED = 3f;
        public const float DEFAULT_ANIMATION_PLAYSPEED = 0.9f;
        private int _animIDSpeed;
        public MoveState()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
        }

        public override void OnEnterState()
        {
        }

        public override void OnUpdateState()
        { 
        }

        public override void OnFixedUpdateState()
        {
            float moveSpeed = Player.Instance.Speed;
            Player.Instance.animator.SetFloat(_animIDSpeed, Player.Instance.Controller.inputDir.magnitude * moveSpeed);
            Player.Instance.transform.position += Player.Instance.Controller.inputDir.normalized * Time.deltaTime * moveSpeed;
        }

        public override void OnExitState()
        {
        }
    }
}
