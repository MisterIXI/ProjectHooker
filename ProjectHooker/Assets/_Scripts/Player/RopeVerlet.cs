using UnityEngine;

public class RopeVerlet : MonoBehaviour
{

    [Range(1, 100)][SerializeField] private int SimulationSteps = 10;
    [SerializeField] private int _pointsCount = 10;
    [Range(0.0001f, 1)][SerializeField] private float _ropeSegmentLength = 0.5f;
    [Range(0f, 1f)][SerializeField] private float _damping = 0.1f;
    [Range(0f, 10f)][SerializeField] private float _gravityStrength = 9.81f;
    [Range(0f,10f)][SerializeField]private float _targetMultiplier = 1f;
    [SerializeField] private Vector2[] _currentPointsPos;
    private Vector2[] _lastPointsPos;
    private Vector2[] _colliderPoints;
    [SerializeField] private Transform _target;
    private Rigidbody2D _targetRb;
    private EdgeCollider2D _edgeCollider2D;
    private void Start()
    {
        _targetRb = _target.GetComponent<Rigidbody2D>();
        _currentPointsPos = new Vector2[_pointsCount];
        _lastPointsPos = new Vector2[_pointsCount];
        _colliderPoints = new Vector2[_pointsCount];
        for (int i = 0; i < _pointsCount; i++)
        {
            _currentPointsPos[i] = transform.position;
            _lastPointsPos[i] = transform.position;
            _colliderPoints[i] = Vector2.zero;
        }
        _edgeCollider2D = GetComponent<EdgeCollider2D>();
    }

    private void Update()
    {
        Simulation();
        for (int i = 0; i < SimulationSteps; i++)
            Constraints();
        // copy currentPoints array to colliderpoints array
        System.Array.Copy(_currentPointsPos, _colliderPoints, _pointsCount);
        // subtract own position of each point to get relative position
        for (int i = 0; i < _pointsCount; i++)
        {
            _colliderPoints[i] -= (Vector2)transform.position;
        }
        _edgeCollider2D.points = _colliderPoints;
    }
    private void Simulation()
    {
        // Rope simulation using Verlet integration
        for (int i = 0; i < _pointsCount; i++)
        {
            // simulation steps
            Vector2 velocity = _currentPointsPos[i] - _lastPointsPos[i];
            _lastPointsPos[i] = _currentPointsPos[i];
            _currentPointsPos[i] += velocity * (1 - _damping);
            // gravity
            // if (i == _pointsCount - 1)
            //     _currentPointsPos[i] += Vector2.down * _gravityStrength * Time.deltaTime;
            // else
            _currentPointsPos[i] += Vector2.down * _gravityStrength * Time.deltaTime;
        }
    }

    private void Constraints()
    {
        _currentPointsPos[0] = transform.position;
        _currentPointsPos[_pointsCount - 1] = _target.position;
        for (int i = 0; i < _pointsCount - 1; i++)
        {
            Vector2 delta = _currentPointsPos[i] - _currentPointsPos[i + 1];
            float distance = delta.magnitude;
            float error = Mathf.Abs(distance - _ropeSegmentLength);
            Vector2 changeDir = Vector2.zero;
            if (distance > _ropeSegmentLength)
            {
                changeDir = delta.normalized;
            }
            else if (distance < _ropeSegmentLength)
            {
                changeDir = -delta.normalized;
            }
            Vector2 changeAmount = changeDir * error;
            if (i == 0)
            {
                _currentPointsPos[i + 1] += changeAmount;
            }
            else if (i == _pointsCount - 2)
            {
                _currentPointsPos[i] -= changeAmount;
                // _targetRb.velocity *= 0.9f;
                _targetRb.AddForce(changeAmount * _targetMultiplier , ForceMode2D.Impulse);
            }
            else
            {
                _currentPointsPos[i] -= changeAmount * 0.5f;
                _currentPointsPos[i + 1] += changeAmount * 0.5f;
            }
        }
    }
    private void OnDrawGizmos()
    {
        // check if play mode
        if (!Application.isPlaying)
            return;
        // draw sphere gizmos at each point if arrays are not null
        if (_currentPointsPos != null)
        {
            for (int i = 0; i < _pointsCount; i++)
            {
                Gizmos.DrawSphere(_currentPointsPos[i], 0.1f);
            }
        }
    }
}