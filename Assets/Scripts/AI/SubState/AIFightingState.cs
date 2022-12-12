using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFightingState : BaseState
{
    AiStateMachine _context;
    Vector3[] _path;
    int _poistionInPath;
    Vector3 _lastPoint;
    Coroutine _animatorUpdateFloats;
    public AIFightingState(AiStateMachine currentContext) : base(currentContext)
    {
        _context = currentContext;
    }
    public override bool CheckSwitchStates()
    {
        if (Vector3.Distance(_context.ControlerContext.transform.position, _context.ControlerContext.PlayerTransfrom.position) > 4)
        {
            SwitchState(_context.States.Follow());
            return true;
        }
        if (_context.ControlerContext.HasAttackToken)
        {
           // SwitchState(_context.States.Attack());
            return true;
        }

        return false;
    }

    public override void EnterState()
    {
        _context.ControlerContext.AnimatorManager.SetCombatMovement(true);
        _lastPoint = _context.ControlerContext.PlayerTransfrom.position;
        GetnewRandomPoint();
        _animatorUpdateFloats = _context.ControlerContext.StartCoroutine(_context.ControlerContext.AnimatorManager.Updatefloats());
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }

    protected override void ExitState()
    {
        _context.ControlerContext.StopCoroutine(_animatorUpdateFloats);
        _context.ControlerContext.AnimatorManager.SetCombatMovement(false);
    }

    protected override void FixedUpdateState()
    {
    }

    protected override void OnAnimatorMoveState()
    {
    }

    protected override void UpdateState()
    {
        if(Vector3.Distance(_context.ControlerContext.transform.position,_context.ControlerContext.PlayerTransfrom.position) < 2.5f && !_context.ControlerContext.HasAttackToken)
        {
            _context.ControlerContext.RequestAttackToken();
        }
        if (Vector3.Distance(_lastPoint, _context.ControlerContext.PlayerTransfrom.position) > 1)
        {
            _lastPoint = _context.ControlerContext.PlayerTransfrom.position;
            GetnewRandomPoint();
        }
        if (_path == null) { return; }
        Debug.DrawLine(_context.ControlerContext.transform.position, _path[_poistionInPath]);
        if (Vector3.Distance(_path[_poistionInPath], _context.ControlerContext.transform.position) < 0.5f)
        {
            _poistionInPath++;
            
            if (_poistionInPath >= _path.Length) { GetnewRandomPoint(); }
        }
        RotateToPoint(_context.ControlerContext.PlayerTransfrom.position);

        Vector3 direction = UtilityFunctions.Vector3Direction(_context.ControlerContext.transform.position, _path[_poistionInPath]);
        Debug.DrawRay(_context.ControlerContext.transform.position, direction, Color.green);
        direction = _context.ControlerContext.transform.rotation * direction;
        direction = direction.normalized;
        Debug.DrawRay(_context.ControlerContext.transform.position, direction,Color.red);
        _context.ControlerContext.AnimatorManager.SetMovementXWithDamp(direction.x);
        _context.ControlerContext.AnimatorManager.SetMovementYWithDamp(direction.z);


    }

    void GetnewRandomPoint()
    {
        Vector3 targetPoint = new Vector3(_lastPoint.x + Random.Range(-3.5f, 3.5f), _lastPoint.y, _lastPoint.z + Random.Range(-3.5f, 3.5f));
        _path = _context.ControlerContext.CalculatePath(targetPoint);
        _context.ControlerContext.path = _path;
        _poistionInPath = 0;
        if (_path == null) { GetnewRandomPoint(); }
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
