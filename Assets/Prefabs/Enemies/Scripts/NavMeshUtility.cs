using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class NavMeshUtility
{
    // Returns a random location on the navmesh, within the given radius
    public static Vector3 RandomNavMeshLocation(float radius, GameObject origin)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
        randomDirection += origin.transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;

        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    // Returns true if the agent has reached its destination or given up
    public static bool ReachedDestinationOrGaveUp(NavMeshAgent agent)
    {
        // If a path has been computed 
        if (!agent.pathPending)
        {
            // If the remaining distance is less than the agent's stopping distance
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                // If the agent does not have a path OR if the agent is moving really slowly?
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
