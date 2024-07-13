using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    public NavMeshAgent agent;
    public MeshRenderer meshRenderer;

    public float chaseDistance;
    public int health;

    private Transform target;
   
    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
            target = PlayerController.Instance.transform;

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


    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Death();
        }

        StopDamageFlash(); // so they don't overlap
        StartCoroutine(DoDamageFlash());
    }

    private void Death()
    {
        Debug.Log($"{gameObject.name} died.");
        Destroy(gameObject);
    }

    private IEnumerator DoDamageFlash()
    {
        meshRenderer.enabled = false;

        yield return new WaitForSeconds(0.05f);

        meshRenderer.enabled = true;
    }

    private void StopDamageFlash()
    {
        StopCoroutine(DoDamageFlash());

        meshRenderer.enabled = true;
    }
}
