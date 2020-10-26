using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedAirPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] _points;

    private float _speed = 2f;
    private float _waitTime = 1f;
    private bool _canGo = true;
    private int _index = 0;

    void Start()
    {
        gameObject.transform.position = new Vector3(_points[0].position.x, _points[0].position.y, transform.position.z);
    }


    void Update()
    {
        MovementCubePatrol();
    }

    private void MovementCubePatrol()
    {
        if (_canGo)
            transform.position = Vector3.MoveTowards(transform.position, _points[_index].position, _speed * Time.deltaTime); // 1 - получаем текущие координаты, 2 - получаем координаты старта, двигаемся

        if (transform.position == _points[_index].position)
        {
            if (_index < _points.Length - 1)
            {
                _index++;
            }
            else
            {
                _index = 0;
            }

            _canGo = false;
            StartCoroutine(Waiting());
        }
    }

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(_waitTime);
        _canGo = true;
    }
}
