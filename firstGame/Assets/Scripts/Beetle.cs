using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : MonoBehaviour
{
    private float _speed = 4f;
    private bool _isWait = false;
    private bool _isHidden = false;
    [SerializeField] private float _waitTime = 4f;
    [SerializeField] private Transform _point;

    void Start()
    {
        _point.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z); 
    }
 
    void Update()
    {
        MovementBeetle();
    }

    private void MovementBeetle()
    {
        if (_isWait == false)
            transform.position = Vector3.MoveTowards(transform.position, _point.position, _speed * Time.deltaTime);
        if (transform.position == _point.position)
        {
            if (_isHidden)
            {
                _point.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
                _isHidden = false;
            }
            else
            {
                _point.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
                _isHidden = true;
            }

            _isWait = true;
            StartCoroutine(Waiting());
        }
    }

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(_waitTime);
        _isWait = false;
    }
}
