using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn : MonoBehaviour
{
    [SerializeField] private GameObject[] _block;
    [SerializeField] private Sprite _buttonDown;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Weight")
        {
            GetComponent<SpriteRenderer>().sprite = _buttonDown;
            GetComponent<CircleCollider2D>().enabled = false;
            foreach (GameObject obj in _block)
            {
                Destroy(obj);
            }
        }
    }
}
