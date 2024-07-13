using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;
    public float chaseDistance;
   
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (target == null)
            target = PlayerInventory.Instance.transform;

        StartCoroutine(ChaseTick());
    }

    private IEnumerator ChaseTick()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, target.position) < chaseDistance)
                agent.SetDestination(target.position);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
