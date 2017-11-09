using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    Transform target;       // Target to follow
    NavMeshAgent agent;     // Reference to our agent

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update(){
        if(target != null){
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.isStopped = false;
        agent.SetDestination(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        agent.isStopped             = false;
        agent.stoppingDistance      = newTarget.radius * .8f;
        agent.updateRotation        = false;   
        target                      = newTarget.interactionTransform;
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance      = 0f;
        agent.updateRotation        = true;
        target                      = null;
    }

    public void Stop()
    {
        agent.isStopped = true;
    }

    void FaceTarget()
    {
        var direction = (target.position - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
