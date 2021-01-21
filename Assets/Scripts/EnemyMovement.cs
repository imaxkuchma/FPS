using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float swingSpeed = 5.0f;
    private NavMeshAgent _agent;
    private GameObject[] _waypoints;
    private GameObject _currentWP;

    // Start is called before the first frame update
    void Start()
    {
        _agent = gameObject.GetComponent<NavMeshAgent>();
        _waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        if (_waypoints.Length>0)
        _currentWP = GetRandomWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (_waypoints.Length == 0) return;
        if (Vector3.Distance(gameObject.transform.position, _currentWP.transform.position) < 3.0f)
        {
            _currentWP = GetRandomWaypoint();
        }

        var direction = _currentWP.transform.position - _agent.transform.position;
        _agent.transform.rotation = Quaternion.Slerp(_agent.transform.rotation, Quaternion.LookRotation(direction), swingSpeed * Time.deltaTime);
        _agent.SetDestination(_currentWP.transform.position);
    }

    private GameObject GetRandomWaypoint()
    {
        return _waypoints[Random.Range(0, _waypoints.Length)];
    }

    
}
