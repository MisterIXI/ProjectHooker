using UnityEngine;

public class RopeVerlet : MonoBehaviour
{

    [SerializeField] private int _pointsCount = 10;
    [SerializeField] private Vector2[] _currentPointsPos;
    [Range(0.0001f,1)][SerializeField] private float _ropeSegmentLength = 0.5f;
    [Range(0f,1f)][SerializeField] private float _damping = 0.1f;
    [Range(0f,10f)][SerializeField]private float _gravityStrength = 9.81f;
    [Range(0f,15f)][SerializeField]private float _ropeMoveDeltaMax= 5f;
    private Vector2[] _lastPointsPos;

    private void Start()
    {
        _currentPointsPos = new Vector2[_pointsCount];
        _lastPointsPos = new Vector2[_pointsCount];
        for (int i = 0; i < _pointsCount; i++)
        {
            _currentPointsPos[i] = transform.position;
            _lastPointsPos[i] = transform.position;
        }
    }

    private void Update()
    {
        // Rope simulation using Verlet integration
        for (int i = 0; i < _pointsCount; i++)
        {
            Vector2 temp = _currentPointsPos[i];
            //apply gravity
            _currentPointsPos[i] += Vector2.down * _gravityStrength * Time.deltaTime;
            _currentPointsPos[i] += (_currentPointsPos[i] - _lastPointsPos[i])*(_damping * (_currentPointsPos[i] - _lastPointsPos[i]).magnitude);
            // _currentPointsPos[i] += (_currentPointsPos[i] - _lastPointsPos[i])*_damping ;
            
            _lastPointsPos[i] = temp;
            // constrain points in the rope
            if (i == 0)
            {
                _currentPointsPos[i] = transform.position;
                _lastPointsPos[i] = transform.position;
            }
            else
            {
                Vector2 difference = _currentPointsPos[i] - _currentPointsPos[i - 1];
                float dist = difference.magnitude;
                if (dist == 0) continue;
                Vector2 direction = difference / dist;

                _currentPointsPos[i] = _currentPointsPos[i - 1] + direction * _ropeSegmentLength;
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