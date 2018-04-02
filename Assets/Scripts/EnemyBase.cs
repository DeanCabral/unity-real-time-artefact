using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour {

    public Image healthBar;
    public GameObject enemyUnit;
    private GameManager GM;
    private float baseHealth = 100f;
    public bool canDamage = false;

    // Use this for initialization
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        canDamage = false;
        StartCoroutine(SpawnEnemy(5));
    }

    private void Update()
    {
        if (canDamage)
        {
            DamageBase();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(KillPlayer(collision.gameObject));
            canDamage = true;
        }
    }

    public void SpawnUnit()
    {
        Instantiate(enemyUnit, new Vector3(0, 2, -34), Quaternion.identity);
    }

    public void DamageBase()
    {
        baseHealth -= 0.1f;
        healthBar.fillAmount = baseHealth / 100;
        GM.IncreaseScore();

        if (baseHealth <= 0)
        {
            BaseDestroyed();
        }
    }

    public void SetState(bool type)
    {
        canDamage = type;
    }

    private void BaseDestroyed()
    {
        Destroy(transform.parent.gameObject);
        GM.GameOver(false);
    }

    IEnumerator SpawnEnemy(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnUnit();
        StartCoroutine(SpawnEnemy(5));
    }

    IEnumerator KillPlayer(GameObject player)
    {
        yield return new WaitForSeconds(3);
        canDamage = false;
        Destroy(player);
    }
}
