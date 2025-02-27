using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : CommonState
{
    //protected AgentMovement _agentMovement;

    //public override void SetUp(Transform agentRoot)
    //{
    //    base.SetUp(agentRoot);
    //    _agentMovement = agentRoot.GetComponent<AgentMovement>();
    //}

    public override void OnEnterState()
    {
        _agentMovement.StopImmediately();
        _agentInput.OnMovementKeyPress += OnMovementHandle; //들어올 때 키입력 구독
        _agentInput.OnAttackKeyPress += OnAttackHandle;
        _agentInput.OnRollingKeyPress += OnRollingHandle;

        _agentMovement.IsActiveMove = true;
    }
       

    public override void OnExitState()
    {
        _agentMovement.StopImmediately();
        _agentInput.OnMovementKeyPress -= OnMovementHandle; //나갈때 키입력 구독해제
        _agentInput.OnAttackKeyPress -= OnAttackHandle;
        _agentInput.OnRollingKeyPress -= OnRollingHandle;

        _agentMovement.IsActiveMove = false;
    }

    private void OnAttackHandle()
    {
        //공격키를 처음 누른 순간 공격상태로 전환되는데 
        _agentMovement.SetRotation(_agentInput.GetMouseWorldPosition());
        _agentController.ChangeState(StateType.Attack);
    }


    private void OnMovementHandle(Vector3 obj)
    {
        _agentMovement?.SetMovementVelocity(obj);
    }

    private void OnRollingHandle()
    {
        _agentController.ChangeState(StateType.Rolling);
    }

    public override bool UpdateState()
    {
        return true;
    }
}
