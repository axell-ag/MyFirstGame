using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBridge : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    private int _index = 1;
    private float _speed = 2f;

    void Start()
    {
        gameObject.transform.position = new Vector3(_points[0].position.x, _points[0].position.y, transform.position.z);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float posX = transform.position.x;
            float posY = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, _points[_index].position, _speed * Time.deltaTime);

            collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x + transform.position.x - posX, collision.gameObject.transform.position.y + transform.position.y - posY, collision.gameObject.transform.position.z);

            if (transform.position == _points[_index].position)
            {
                if (_index < _points.Length - 1)
                    _index++;
                else
                    _index = 0;
            }
        }
    }

}
