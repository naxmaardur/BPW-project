using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPatrolState : BaseState
{
    AiStateMachine _context;
    Vector3[] _path;
    int _pathIndex;
    bool _loop;
    public AiPatrolState(AiStateMachine currentContext) : base(currentContext)
    {
        _context = currentContext;
    }
    public override bool CheckSwitchStates()
    {
        if (Vector3.Distance(_path[_pathIndex], _context.ControlerContext.transform.position) < 1)
        {
            _pathIndex++;
            SwitchState(_context.States.Idle());
        }
        return false;
    }
    public override void EnterState()
    {
        _context.ControlerContext.AnimatorManager.SetWalkforward(true);
        if (_path != null) { return; }
        if (_context.ControlerContext.patrolPath != null)
        {
            PathData data = _context.ControlerContext.patrolPath.GetDataOfClostestPatrolPoint(_context.ControlerContext.transform.position);
            _path = data.path;
            _pathIndex = data.index;
            _loop = data.loop;
        }
        else
        {
            PathData data = GameMaster.Instance.PatrolPathMaster.GetClostestPath(_context.ControlerContext.transform.position);
            _path = data.path;
            _pathIndex = data.index;
            _loop = data.loop;
        }
    }
    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
    protected override void ExitState()
    {
        _context.ControlerContext.AnimatorManager.SetWalkforward(false);
        if (_pathIndex < _path.Length - 1) { return; }
        if (!_loop)
        {
            _path = null;
            _context.ControlerContext.patrol = false;
        }
        else
        {
            _pathIndex = 0;
        }
    }
    protected override void FixedUpdateState()
    {
    }
    protected override void OnAnimatorMoveState()
    {
    }
    protected override void UpdateState()
    {
        if (_path == null) { return; }
        RotateToPoint(_path[_pathIndex]);
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
}
