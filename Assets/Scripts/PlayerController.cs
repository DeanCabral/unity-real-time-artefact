using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour {

    private GameManager GM;
    private GameController GC;
    private NavMeshAgent agent;
    private float health = 100;

    void Start()
    {
        GC = FindObjectOfType<GameController>();        
    }

    void Update()
    {
        MoveAgent();
        CheckDeath();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            DamagePlayer();
            collision.gameObject.GetComponent<EnemyController>().DamageEnemy();
        }
    }

    public void MoveAgent()
    {
        if (GC.unitSelected)
        {
            agent = GC.selectedUnit.GetComponent<NavMeshAgent>();
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    if (hit.collider.tag == "Ground")
                    {
                        agent.destination = hit.point;
                        GC.DeselectUnit();
                    }                  
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                
                if (Physics.Raycast(ray, out hit, 500.0f))
                {                    
                    if (hit.collider.tag == "Enemy")
                    {
                        hit.collider.GetComponentInChildren<EnemyController>().StopMovement();
                        agent.destination = hit.collider.transform.position;
                        GC.DeselectUnit();
                    }
                    else if (hit.collider.tag == "EnemyBase")
                    {                        
                        agent.destination = hit.point;
                        GC.DeselectUnit();
                    }
                }
            }
        }        
    }

    void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void DamagePlayer()
    {
        health -= 2.5f;
    }


}
