using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPatrol : MonoBehaviour
{
    [SerializeField] private Transform _point1;
    [SerializeField] private Transform _point2;
    [SerializeField] private float _waitTime = 2f;
    private float _speed = 3f;
    private bool _canGo = true;

    private void Start()
    {
        gameObject.transform.position = new Vector3(_point1.position.x, _point2.position.y, transform.position.z); 
    }

   
    private void Update()
    {
        MovementPatrol();
    }

    private void MovementPatrol()
    {
        if (_canGo)
            transform.position = Vector3.MoveTowards(transform.position, _point1.position, _speed * Time.deltaTime);

        if (transform.position == _point1.position)
        {
            Transform t = _point1;
            _point1 = _point2;
            _point2 = t;
            _canGo = false;
            StartCoroutine(Waiting());
        }
    }

    private IEnumerator Waiting() 
    {
        yield return new WaitForSeconds(_waitTime);
        if (transform.rotation.y == 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
        _canGo = true;
    }
}
