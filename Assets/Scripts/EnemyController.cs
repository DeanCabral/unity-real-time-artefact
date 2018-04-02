using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    private float health = 100;
    private NavMeshAgent agent;
    private GameManager GM;
    private GameObject playerBase;
    private GameObject paths;
    private Transform[] points;
    private int destPoint = 0;

    // Use this for initialization
    void Start () {

        GM = FindObjectOfType<GameManager>();
        playerBase = GameObject.Find("AttackPointH");
        agent = GetComponent<NavMeshAgent>();
        paths = GameObject.Find("Paths");
        points = new Transform[4];

        for (int i = 0; i < 4; i++)
        {
            points[i] = paths.transform.GetChild(i);
        }

        GotoNextPoint();
    }
	
	// Update is called once per frame
	void Update () {

        CheckDeath();

        if (agent != null)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }        
    }

    void CheckDeath()
    {
        if (health <= 0)
        {
            GM.IncreaseScore();
            GM.IncreaseMoney();
            Destroy(this.gameObject);
        }
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;
        int index = Random.Range(0, 4);
        Debug.Log(index);
        if (destPoint == 0) agent.destination = points[index].position;
        else agent.destination = playerBase.transform.position;

        destPoint = (destPoint + 1) % points.Length;
    }

    public void StopMovement()
    {
        agent.isStopped = true;
    }

    public void DamageEnemy()
    {
        health -= 5;
    }
}
