using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiWanderState : BaseState
{
    AiStateMachine _context;
    Vector3[] _path;
    Vector3 _originPoint;
    int _poistionInPath;

    public AiWanderState(AiStateMachine currentContext) : base(currentContext)
    {
        _context = currentContext;
        _originPoint = currentContext.ControlerContext.transform.position;
    }

    public override bool CheckSwitchStates()
    {
        if(_path != null) { return false; }
        SwitchState(_context.States.Idle());
        return true;
    }

    public override void EnterState()
    {
        _context.ControlerContext.AnimatorManager.SetWalkforward(true);
        GetnewRandomPoint();
    }

    public override void InitializeSubState()
    {
        //throw new System.NotImplementedException();
    }

    protected override void ExitState()
    {
        _context.ControlerContext.AnimatorManager.SetWalkforward(false);
    }

    protected override void FixedUpdateState()
    {
       // throw new System.NotImplementedException();
    }

    protected override void OnAnimatorMoveState()
    {
        //throw new System.NotImplementedException();
    }

    protected override void UpdateState()
    {
       if(_path == null) { return; }
       if(Vector3.Distance(_path[_poistionInPath],_context.ControlerContext.transform.position) < 1)
        {
            _poistionInPath++;
            if(_poistionInPath >= _path.Length) { _path = null; return; }
        }
        RotateToPoint(_path[_poistionInPath]);


    }


    void RotateToPoint(Vector3 point)
    {
        Vector3 direction = UtilityFunctions.Vector3Direction(_context.ControlerContext.transform.position, point);
        float movementfloat = Mathf.Clamp01(Mathf.Abs(direction.x) + Mathf.Abs(direction.y));
        float turningMod = 0.2f;
        float turnSpeed = _context.ControlerContext.TurningSpeed + turningMod * movementfloat;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(_context.ControlerContext.transform.eulerAngles.y, targetAngle, ref _context.ControlerContext.turnSmoothVelocity, turnSpeed);
        _context.ControlerContext.transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }


    void GetnewRandomPoint()
    {
        Vector3 targetPoint = new Vector3(_originPoint.x + Random.Range(-10, 11), _originPoint.y, _originPoint.z + Random.Range(-10, 11));
        _path = _context.ControlerContext.CalculatePath(targetPoint);
        _poistionInPath = 0;
        if(_path == null) { GetnewRandomPoint(); }
    }


}
