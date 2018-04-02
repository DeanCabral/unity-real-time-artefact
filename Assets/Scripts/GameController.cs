using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private GameManager GM;
    private float speed = 20.0f;
    public GameObject selectedUnit;
    public bool unitSelected = false;

    // Use this for initialization
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update () {

        CheckMovement();
        CheckInteraction();
	}

    private void CheckMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0, -1, 0), Space.Self);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0, 1, 0), Space.Self);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            transform.position += Vector3.down * speed * 2 * Time.deltaTime;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            transform.position += Vector3.up * speed * 2 * Time.deltaTime;
        }

    }

    private void CheckInteraction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 500.0f))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "HomeBase")
                    {
                        GM.ToggleBaseMenu();
                    }
                    else if (hit.collider.tag == "Player")
                    {
                        SetSelectedUnit(hit.collider.gameObject);
                    }
                }
                
            }
        }       
    }

    private void SetSelectedUnit(GameObject unit)
    {
        selectedUnit = unit;
        selectedUnit.GetComponent<Renderer>().material = GM.selectedMat;
        unitSelected = true;
    }

    public void DeselectUnit()
    {
        if (selectedUnit != null)
        {
            selectedUnit.GetComponent<Renderer>().material = GM.normalMat;
            unitSelected = false;
            selectedUnit = null;
        }        
    }
}
