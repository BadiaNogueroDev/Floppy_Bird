using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private GameObject [] _grounds;
    [SerializeField] private float _speed;
    [SerializeField] private float _limit;
    
    void Update()
    {
        if (!GameManager.instance._bird.IsDead())
        {
            for (int i = 0; i < _grounds.Length; i++)
            {
                GameObject previousGround = i == 0 ? _grounds[^1] : _grounds[i - 1];
             
                _grounds[i].transform.position -= new Vector3(_speed * Time.deltaTime, 0, 0);
            
                if (_grounds[i].transform.position.x <= -_limit)
                    _grounds[i].transform.position = new Vector3(previousGround.transform.position.x + _limit, previousGround.transform.position.y, _grounds[i].transform.position.z);
            }
        }
    }
}
