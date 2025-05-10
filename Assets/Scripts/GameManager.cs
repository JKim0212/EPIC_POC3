using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;

    static GameManager _instance;
    public bool IsPlaying => _isPlaying;
    public int NumSuccess => _numShot;
    public int NumFailed => _numMistake;
    public float PayPerShot => _payPerShot;
    public float LossPerDay => _lossPerDay;
    public float RemainingMoney => _remainingMoney;

    [SerializeField] float _dayLength;
    [SerializeField] float _payPerShot;
    [SerializeField] float _lossPerDay;
    int _stage = 0;
    float _time = 0;
    int _numShot = 0;
    int _numMistake = 0;
    bool _isPlaying = false;
    float _remainingMoney = 200;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _isPlaying = true;
    }

    private void Update()
    {
        if (_isPlaying)
        {
            _time += Time.deltaTime;
            if (_time >= _dayLength)
            {
                EndDay();
            }
        }
    }

    public void IncreaseNumShot()
    {
        _numShot++;
    }

    public void IncreaseMistake()
    {
        _numMistake++;
    }

    void EndDay()
    {
        _time = 0;
        _isPlaying = false;
        UIManager.Instance.ToggleEndDayCanvas(true);
        Debug.Log($"오늘 {_numShot}번 발사했고, {_numMistake}번의 실수를 했습니다.");
    }

    public void EarnMoney(float amount)
    {
        _remainingMoney += amount - _lossPerDay;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void StartNextStage()
    {
        _numMistake = 0;
        _numShot = 0;
        _stage += 1;
        SceneManager.LoadScene(_stage);
    }
}
