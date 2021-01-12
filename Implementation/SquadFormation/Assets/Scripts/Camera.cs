using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    
    public GameObject _player = null;
    
    public float _followSpeed = 5.0f;

    private void FixedUpdate()
    {
        if (_player != null)
        {
            transform.position = Vector3.Lerp(transform.position, _player.transform.position, _followSpeed * Time.deltaTime);
        }
    }
}
