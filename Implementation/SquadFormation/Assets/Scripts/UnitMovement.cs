using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public bool isInSquad = false;
    public Vector3 target;
    private float velocityMultiplier = 6.0f;
    public Material Material_Squad;


    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        if (isInSquad)
        {
            BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();

            if (boxCollider)
            {
                boxCollider.isTrigger = true;
            }

            gameObject.GetComponent<MeshRenderer>().material = Material_Squad;

            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * velocityMultiplier);
        }
    }
}
