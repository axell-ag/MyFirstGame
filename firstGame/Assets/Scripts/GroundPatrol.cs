using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPatrol : MonoBehaviour
{
    private float _speed = 1.5f;
    private bool _moveRight = true;

    [SerializeField] private Transform _groundDetect;
    
    private void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime); 
        RaycastHit2D groundInfo = Physics2D.Raycast(_groundDetect.position, Vector2.down, 1f);

        if (groundInfo.collider == false)
        {
            if (_moveRight)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                _moveRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                _moveRight = true;
            }
        }
    }
}
