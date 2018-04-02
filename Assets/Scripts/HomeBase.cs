using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeBase : MonoBehaviour {

    public Image healthBar;
    public GameObject playerUnit;
    public bool canDamage = false;
    private GameManager GM;
    private float baseHealth = 100f;   

	// Use this for initialization
	void Start () {

        GM = FindObjectOfType<GameManager>();
        canDamage = false;
	}

    void Update()
    {
        if (canDamage)
        {
            DamageBase();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(KillEnemy(collision.gameObject));
            canDamage = true;
        }
    }

    public void SpawnUnit()
    {
        Instantiate(playerUnit, new Vector3(0, 2, 34), Quaternion.identity);
    }

    public void IncreaseHealth()
    {
        baseHealth += 20;
        healthBar.fillAmount = baseHealth / 100;
    }

    public void DamageBase()
    {
        baseHealth -= 0.1f;
        healthBar.fillAmount = baseHealth / 100;

        if (baseHealth <= 0)
        {
            BaseDestroyed();
        }
    }

    private void BaseDestroyed()
    {
        Destroy(transform.parent.gameObject);
        GM.GameOver(true);
    }

    IEnumerator KillEnemy(GameObject enemy)
    {
        yield return new WaitForSeconds(3);
        canDamage = false;
        Destroy(enemy);
    }
}
