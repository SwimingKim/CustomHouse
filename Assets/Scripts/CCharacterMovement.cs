using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CCharacterMovement : MonoBehaviour
{
    // NavMeshAgent _navMeshAgent;

    // float minX = -4, maxX = 19;
    // float minZ = -14, maxZ = 14;

    // public Vector3[] pos;
    // int index = 0;

    // void Awake()
    // {
    //     _navMeshAgent = GetComponent<NavMeshAgent>();
    // }

    // void Start()
    // {
    //     Invoke("Trace", 1.5f);
    // }

    // void Trace()
    // {
    //     Vector3 target = pos[index];

    //     Debug.Log(pos+"를 향해 돌진");
    //     _navMeshAgent.SetDestination(target);
    //     _navMeshAgent.isStopped = false;

    //     StartCoroutine("TraceCoroutine", target);
    // }

    // IEnumerator TraceCoroutine(Vector3 target)
    // {
    //     yield return new WaitForFixedUpdate();
    //     float dist = Vector3.Distance(transform.position, target);

    //     if (dist <= 2)
    //     {
    //         if (index == pos.Length-1) index = 0;

    //         StopAllCoroutines();
    //         _navMeshAgent.isStopped = true;
    //         Invoke("Trace", Random.Range(1.5f, 2));
    //     }
    //     else
    //     {
    //         StartCoroutine("TraceCoroutine", pos);
    //     }
    // }

    NavMeshAgent _navMeshAgent;
    public List<GameObject> target;

    int index = 0;

    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        target.Add(GameObject.Find("obj_tree1b"));
        target.Add(GameObject.Find("obj_tree2b"));
        target.Add(GameObject.Find("obj_tree4"));
        target.Add(GameObject.Find("obj_planter1"));

        Trace(target[index]);
    }

    void Trace(GameObject target)
    {
        _navMeshAgent.SetDestination(target.transform.position);

        StartCoroutine("TraceCoroutine", target.transform.position);
    }

    IEnumerator TraceCoroutine(Vector3 pos)
    {
        yield return new WaitForFixedUpdate();
        float dist = Vector3.Distance(transform.position, pos);

        if (dist <= 2)
        {
            if (index==target.Count-1)
                index = 0;
            else 
                index++;

            StopAllCoroutines();
            Trace(target[index]);
        }
        else
        {
            StartCoroutine("TraceCoroutine", pos);
        }
    }

}
