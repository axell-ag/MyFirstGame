using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shoot;

    [SerializeField] private float _timeShoot = 4f;
    
    private void Start()
    {
        _shoot.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z); 
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        yield return new WaitForSeconds(_timeShoot);
        Instantiate(_bullet, _shoot.transform.position, transform.rotation);  

        StartCoroutine(Shooting());
    }
}
