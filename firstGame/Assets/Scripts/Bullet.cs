using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed = 3f;
    private float _timeToDisable = 10f;

    private void Start()
    {
        StartCoroutine(SetDisable());
    }

   
    private void Update()
    {
        transform.Translate(Vector2.down * _speed * Time.deltaTime);
    }

    private IEnumerator SetDisable()
    {
        yield return new WaitForSeconds(_timeToDisable);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopCoroutine(SetDisable());
        gameObject.SetActive(false);
    }
}
