using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureZone : MonoBehaviour
{
    public CoreBehavior player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        UnitMovement temp = other.GetComponent<UnitMovement>();

        if (temp != null)
        {
            if (!temp.isInSquad) player.AddAgentToSquad(temp);
        }

    }
}
