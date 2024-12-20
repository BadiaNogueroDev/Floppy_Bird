using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; set; }
    public Bird _bird;
    public Menu _menu;
    
    private int _currentScore;
    private int _maxScore;
    
    private void Awake()
    {
        if (instance != null && instance != this) 
            Destroy(this);
        else 
            instance = this;
        
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _maxScore = PlayerPrefs.GetInt("MaxScore", 0);
    }

    public void SaveScore()
    {
        if (_currentScore > _maxScore)
        {
            PlayerPrefs.SetInt("MaxScore", _currentScore);
            _maxScore = _currentScore;
        }
    }

    public void IncreaseScore()
    {
        _currentScore++;
        _menu.UpdateScoreText(_currentScore);
        AudioManager.instance.PlayAudio(AudioManager.AUDIOS.COIN);
    }

    public void ResetScore()
    {
        _currentScore = 0;
    }

    public int GetCurrentScore() { return _currentScore; }
    public int GetMaxScore() { return _maxScore; }
}
