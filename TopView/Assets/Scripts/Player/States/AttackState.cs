using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterController
{
    public class AttackState : BaseState
    {
        public bool IsAttack { get; set; } = false;

        private int _animIDAim;
        private int _animIDSpeed;

        public AttackState()
        {
            _animIDAim = Animator.StringToHash("Aiming");
            _animIDSpeed = Animator.StringToHash("Speed");
        }

        public override void OnEnterState()
        {
            Player.Instance.animator.SetTrigger(_animIDAim);
            IsAttack = true;
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
