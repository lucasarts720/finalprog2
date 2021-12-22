using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    #region Variables
    public int IdleTime;
    public int ChaseTime;
    bool hunting = false;
    public Jugador player;
    int IdleGOIndex;
    public List<Transform> IdleGO;
    public SphereCollider HuntingArea;
    #endregion

    #region Movement
    public Transform target;
    public NavMeshAgent agent;
    #endregion
    private void Start()
    {
        StartCoroutine(Idle());
        #region Movement
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Jugador>(true);
        #endregion
    }

    private void Update()
    {
        #region Movement
        if (hunting == false)
        {
            Idle();
        }
        else
        {
            Chase();
        }
        agent.SetDestination(target.position);
        transform.LookAt(target);
        #endregion
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hunting = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hunting = false;
        }
    }

    IEnumerator Chase()
    {
        target = player.transform;
        yield return new WaitForSeconds(ChaseTime);
    }

    IEnumerator Idle()
    {
        for (int i = 0; i < 100; i++)
        {
            IdleGOIndex = Random.Range(0, IdleGO.Count);
            target = IdleGO[IdleGOIndex];
            //Wait for X seconds
            yield return new WaitForSeconds(IdleTime);
        }
    }
}
