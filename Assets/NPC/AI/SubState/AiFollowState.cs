using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiFollowState : BaseState
{
    AiStateMachine _context;
    Vector3[] _path;
    int _poistionInPath;
    Vector3 _lastPoint;
    bool _requestingPath;
    public AiFollowState(AiStateMachine currentContext) : base(currentContext)
    {
        _context = currentContext;
    }
    public override bool CheckSwitchStates()
    {
        if (Vector3.Distance(_context.ControlerContext.transform.position, _context.ControlerContext.PlayerTransfrom.position) <= 4)
        {
            SwitchState(_context.States.Fighting());
            return true;
        }
        return false;
    }
    public override void EnterState()
    {
        _context.ControlerContext.AnimatorManager.SetRunforward(true);
        _lastPoint = _context.ControlerContext.PlayerTransfrom.position;
        RequestPath();
    }
    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
    protected override void ExitState()
    {
        _context.ControlerContext.AnimatorManager.SetRunforward(false);
    }
    protected override void FixedUpdateState()
    {
    }
    protected override void OnAnimatorMoveState()
    {
    }
    protected override void UpdateState()
    {
        if (Vector3.Distance(_lastPoint, _context.ControlerContext.PlayerTransfrom.position) > 1)
        {

            _lastPoint = _context.ControlerContext.PlayerTransfrom.position;
            RequestPath();
        }
        if (_path == null || _requestingPath) { return; }
        if (_poistionInPath > _path.Length - 1) { _path = null; return; }
        if (Vector3.Distance(_path[_poistionInPath], _context.ControlerContext.transform.position) < 1)
        {
            _poistionInPath++;
        }
        RotateToPoint(_path[_poistionInPath]);
    }
    void RotateToPoint(Vector3 point)
    {
        Vector3 direction = UtilityFunctions.Vector3Direction(_context.ControlerContext.transform.position, point);
        float movementfloat = Mathf.Clamp01(Mathf.Abs(direction.x) + Mathf.Abs(direction.y));
        float turnSpeed = 0f;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(_context.ControlerContext.transform.eulerAngles.y, targetAngle, ref _context.ControlerContext.turnSmoothVelocity, turnSpeed);
        _context.ControlerContext.transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
    void RequestPath()
    {
        _requestingPath = true;
        _path = _context.ControlerContext.CalculatePath(_lastPoint);
        _requestingPath = false;
    }
}
