using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    public NavMeshAgent agent;
    public MeshRenderer meshRenderer;

    public float chaseDistance;
    public int health;
    public float attackTime;
    public GameObject attackHitbox;

    private Transform target;
    private float attackTimer;
   
    // Start is called before the first frame update
    void Start()
    {
        Activate();
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            attackHitbox.SetActive(true); 
        }
    }

    private void Activate()
    {
        if (target == null)
            target = PlayerController.Instance.transform;

        attackTimer = 0f;

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

    public void OnPlayerHit()
    {
        PlayerController.PlayerHealth.TakeDamage(1);
        attackHitbox.SetActive(false);

        attackTimer = attackTime;
    }
}
