using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _bird;
    
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _inGamehMenu;
    
    [SerializeField] private Toggle _audioToggle;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _okButton;
    
    [SerializeField] private TMP_Text _currentScoreIngameText;
    [SerializeField] private TMP_Text _currentScoreDeathText;
    [SerializeField] private TMP_Text _maxScoreDeathText;
    
    [SerializeField] private GameObject _newRecordImage;
    [SerializeField] private Image _medalImage;
    [SerializeField] private Sprite _bronzeSprite;
    [SerializeField] private Sprite _silverSprite;
    [SerializeField] private Sprite _goldSprite;
    private Animator _animator;
    
    private void Start()
    {
        GameManager.instance._menu = this;
        _animator = GetComponent<Animator>();
        _audioToggle.isOn = AudioManager.instance._mute;
        
        _startButton.onClick.AddListener(HideMainMenu);
        _okButton.onClick.AddListener(RestartGame);
        _audioToggle.onValueChanged.AddListener(AudioManager.instance.SwitchMute);
    }

    void HideMainMenu()
    {
        _mainMenu.SetActive(false);
        _inGamehMenu.SetActive(true);
        _bird.SetActive(true);
    }
    
    public void UpdateScoreText(int score)
    {
        _currentScoreIngameText.text = score.ToString();
    }

    public IEnumerator ShowDeathMenu()
    {
        SetMedal();
        _inGamehMenu.SetActive(false);
        _animator.SetTrigger("Flash");
        
        _currentScoreDeathText.text = GameManager.instance.GetCurrentScore().ToString();
        _maxScoreDeathText.text = GameManager.instance.GetMaxScore().ToString();
        
        yield return new WaitForSeconds(0.5f);
        
        AudioManager.instance.PlayAudio(AudioManager.AUDIOS.SWOOSH);
        _animator.SetTrigger("ShowMenu");
    }

    void SetMedal()
    {
        if (GameManager.instance.GetCurrentScore() < GameManager.instance.GetMaxScore())
            _medalImage.sprite = _bronzeSprite;
        else if (GameManager.instance.GetCurrentScore() == GameManager.instance.GetMaxScore())
            _medalImage.sprite = _silverSprite;
        else if (GameManager.instance.GetCurrentScore() > GameManager.instance.GetMaxScore())
        {
            _medalImage.sprite = _goldSprite;
            _newRecordImage.SetActive(true);
        }
        
        GameManager.instance.SaveScore();
    }

    void RestartGame()
    {
        GameManager.instance.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
