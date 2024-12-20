using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tube : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _limit;
    [SerializeField] private float _minHeight;
    [SerializeField] private float _maxHeight;
    private bool _scoreIncreased;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, Random.Range(_minHeight, _maxHeight), 0);
    }

    void Update()
    {
        if (!GameManager.instance._bird.IsDead() && GameManager.instance._bird.GameStarted())
        {
            transform.position -= new Vector3(_speed * Time.deltaTime, 0, 0);

            if (transform.position.x <= -_limit)
            {
                transform.position = new Vector3(_limit, Random.Range(_minHeight, _maxHeight), transform.position.z);
                _scoreIncreased = false;
            }

            if (GameManager.instance._bird.transform.position.x < transform.position.x || _scoreIncreased)
                return;
            
            _scoreIncreased = true;
            GameManager.instance.IncreaseScore();
        }
    }
}
