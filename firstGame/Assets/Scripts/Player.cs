using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator animator;

    public Transform GroundCheck;
    public Main Main;
    public SoundEffector SoundEffector;

    private float _speed;
    private float _normalSpeed = 6f;
    private float _jumpHeight = 15f;
    private int _maxHp = 3;
    private int _currentHp;
    private bool _isGrounded;
    private bool _isHit = false;
    private bool _key = false;
    private bool _canTeleport = true;
    private bool _isClimbing = false;
    private int _coin = 0;
    private bool _canHit = true;

    [SerializeField] private Image[] _image;
    [SerializeField] private Sprite IsBlueGem, NonBlueGem, isGreenGem, NonGreenGem, isKey, NonKey;

    public void RecountHp(int deltaHp)
    {
        
        if (deltaHp < 0 && _canHit)
        {
            _currentHp = _currentHp + deltaHp;
            StopCoroutine(OnHit());
            _isHit = true;
            StartCoroutine(OnHit());
        }
        else if (_currentHp < _maxHp && _canHit)
        {
            _currentHp = _currentHp + deltaHp;
            _currentHp = _maxHp;
        }
        print(_currentHp);
        if (_currentHp <= 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            Invoke("Lose", 1.5f);
        }
    }

    public void OnLeftButtonDown()
    {
            _speed = -_normalSpeed;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    public void OnRightButtonDown()
    {
            _speed = _normalSpeed;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
    public void Invulnerability()
    {
        StartCoroutine(EnemyNoHit());
    }

    public void OnButtonUp()
    {
        animator.SetInteger("State", 1);
        _speed = 0f;
    }
    public int GetCoin()
    {
        return _coin;
    }
    public int GetHP()
    {
        return _currentHp;
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>(); 
        _currentHp = _maxHp;
        _speed = 0f;
    }

    private void Update()
    {  
        CheckGround();
        if (_speed != 0f)
        {
            animator.SetInteger("State", 2);
        }
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(_speed, rigidbody.velocity.y);
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, 0.2f, 1);
        _isGrounded = colliders.Length > 1;
        if (!_isGrounded && !_isClimbing)
            animator.SetInteger("State", 3);
        else
            animator.SetInteger("State", 1);
    }

    public void OnJumpButtonDown()
    {
        if (_isGrounded)
        {
            rigidbody.AddForce(transform.up * _jumpHeight, ForceMode2D.Impulse);
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, _jumpHeight);
            SoundEffector.PlayJumpSound();
        }
    }

    private void Lose()
    {
        Main.GetComponent<Main>().Lose();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key") // проверяем на объект столкновения
        {
            Destroy(collision.gameObject);
            _key = true;
            _image[2].sprite = isKey;
        }
        if (collision.gameObject.tag == "Door")
        {
            if (collision.gameObject.GetComponent<Door>().IsOpen && _canTeleport)
            {
                collision.gameObject.GetComponent<Door>().Teleport(gameObject);
                _image[2].sprite = NonKey;
                _canTeleport = false;
                StartCoroutine(WaitTeleport());
            }
            else if (_key)
                collision.gameObject.GetComponent<Door>().Unlock();
        }
        if (collision.gameObject.tag == "Coin") 
        {
            Destroy(collision.gameObject);
            _coin++;
            SoundEffector.PlayCoinSound();
        }
        if (collision.gameObject.tag == "Mushroom")
        {
            Destroy(collision.gameObject);
            RecountHp(-1);
        }
        if (collision.gameObject.tag == "Heart")
        {
            Destroy(collision.gameObject);
            RecountHp(1);
        }
        if (collision.gameObject.tag == "GemBlue")
        {
            Destroy(collision.gameObject);
            StartCoroutine(NoHit());
            
        }
        if (collision.gameObject.tag == "GemGreen")
        {
            Destroy(collision.gameObject);
            StartCoroutine(Acceleration());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            _isClimbing = true;
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
            if (Input.GetAxis("Vertical") == 0)
            {
                animator.SetInteger("State", 4);
            }
            else
            {
                animator.SetInteger("State", 5);
                transform.Translate(Vector3.up * Input.GetAxis("Vertical") * _speed * 0.5f * Time.deltaTime);
            }
        }

        if (collision.gameObject.tag == "Ice")
        {
            if (rigidbody.gravityScale == 1f)
            {
                rigidbody.gravityScale = 7f;
                _speed *= 0.25f;
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            _isClimbing = false;
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }

        if (collision.gameObject.tag == "Ice")
        {
            if (rigidbody.gravityScale == 7f)
            {
                rigidbody.gravityScale = 1f;
                _speed *= 4f;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trempoline")
        {
            StartCoroutine(TrempolineAnimation(collision.gameObject.GetComponentInParent<Animator>()));
        }
        if (collision.gameObject.tag == "Quicksand")
        {
            _speed *= 0.25f;
            rigidbody.mass *= 100f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Quicksand")
        {
            _speed *= 4f;
            rigidbody.mass *= 0.01f;
        }
    }

    private IEnumerator NoHit()
    {
        _image[0].sprite = IsBlueGem;
        _canHit = false;
        yield return new WaitForSeconds(5f);
        _canHit = true;
        _image[0].sprite = NonBlueGem;

    }

    private IEnumerator EnemyNoHit ()
    {
        _canHit = false;
        yield return new WaitForSeconds(2f);
        _canHit = true;
    }

    private IEnumerator Acceleration()
    {
        _image[1].sprite = isGreenGem;
        _speed *= 2;
        yield return new WaitForSeconds(5f);
        _speed /= 2;
        _image[1].sprite = NonGreenGem;
    }

    private IEnumerator TrempolineAnimation(Animator anim)
    {
        anim.SetBool("Spring", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Spring", false);
    }

    private IEnumerator OnHit()
    {
        if (_isHit)
            GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g - 0.04f, GetComponent<SpriteRenderer>().color.b - 0.04f);
        else
            GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g + 0.04f, GetComponent<SpriteRenderer>().color.b + 0.04f);

        if (GetComponent<SpriteRenderer>().color.g <= 0)
            StopCoroutine(OnHit());
        if (GetComponent<SpriteRenderer>().color.g <= 0)
            _isHit = false;

        yield return new WaitForSeconds(0.02f);
        StartCoroutine(OnHit());
    }

    private IEnumerator WaitTeleport()
    {
        yield return new WaitForSeconds(1f);
        _canTeleport = true;
    }
}
