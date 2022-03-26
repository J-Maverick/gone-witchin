using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FishSwimHandler : MonoBehaviour
{
    public GameObject target;
    public GameObject fishJail;
    private NavMeshAgent navMeshAgent;
    public float targetChance = 0.1f;
    public float catchDistance = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target.GetComponent<LureHandler>().AddFish(this);
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    navMeshAgent.SetDestination(target.transform.position);
    //}
    public float wanderRadius;
    public float wanderTimer;
    public float targetTimer;
   
    private NavMeshAgent agent;
    private float timer;
    private float targetCheckTimer;
    private Vector3 targetPos;

    private bool targetingLure;
    private bool isCaught = false;

    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        targetCheckTimer = targetTimer;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        targetCheckTimer += Time.deltaTime;
        if (isCaught)
        {
            agent.SetDestination(fishJail.transform.position);
        }
        else if (Vector3.Distance(transform.position, target.transform.position) < catchDistance)
        {
            transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
            transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;
            transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;
            transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;
            isCaught = true;
        }
        else if (timer >= wanderTimer)
        {
            targetPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(targetPos);
            timer = 0;
            targetingLure = false;
        }

    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void OnDestroy()
    {
        target.GetComponent<LureHandler>().RemoveFish(this);
    }

    public void TargetLure(float targetRoll)
    {
        if (targetCheckTimer > targetTimer)
        {
            if (targetRoll < targetChance)
            {
                agent.SetDestination(target.transform.position);
                Debug.Log("Targeting Lure");
                targetingLure = true;
            }
            targetCheckTimer = 0f;
        }

    }
}
