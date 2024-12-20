using UnityEngine;
using Random = UnityEngine.Random;

public class Bird : MonoBehaviour
{
    [SerializeField] private GameObject[] _birdColor;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpSpeed;
    private Rigidbody2D _rigidbody2D;
    private bool _gameStarted;
    private bool _isDead;
    
    private void Start()
    {
        GameManager.instance._bird = this;
        
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0;
        
        Instantiate(_birdColor[Random.Range(0, _birdColor.Length)], transform);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _rigidbody2D.gravityScale = 1;
            _gameStarted = true;

            if (!_isDead)
            {
                AudioManager.instance.PlayAudio(AudioManager.AUDIOS.FLY);
                _rigidbody2D.linearVelocity = new Vector2(0, _jumpSpeed);
                _rigidbody2D.SetRotation(30);
            }
        }
        
        if (_rigidbody2D.linearVelocity.y < 0)
        {
            _rigidbody2D.SetRotation(_rigidbody2D.rotation - _rotationSpeed * Time.deltaTime);
            _rigidbody2D.SetRotation(Mathf.Clamp(_rigidbody2D.rotation, -90, 30));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!_isDead)
        {
            _isDead = true;
            _rigidbody2D.linearVelocity = Vector2.zero;
            AudioManager.instance.PlayAudio(AudioManager.AUDIOS.HIT);
            AudioManager.instance.PlayAudio(AudioManager.AUDIOS.DIE);
            GameManager.instance._menu.StartCoroutine("ShowDeathMenu");
        }
    }
    
    public bool GameStarted() { return _gameStarted; }
    public bool IsDead() { return _isDead; }
}
