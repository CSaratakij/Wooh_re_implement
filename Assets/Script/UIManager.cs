using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    const string SOUND_BGM_KEY = "SOUND_BGM_LEVEL";


    [SerializeField]
    PlayerController player;

    [SerializeField]
    Text txtScore;

    [SerializeField]
    Image imgTutorial;

    [SerializeField]
    WaveController waveController;

    [SerializeField]
    Sprite[] imgHealthStates;

    [SerializeField]
    Image[] imgHealths;

    [SerializeField]
    Image[] imgPerformanceStates;

    [SerializeField]
    GameObject gameOverPanel;

    [SerializeField]
    GameController _gameController;

    [SerializeField]
    Text txtResultScore;

    [SerializeField]
    Text txtPerfectStack;

    [SerializeField]
    GameObject pausePanel;

    [SerializeField]
    GameObject txtLeaderboardParent;

    [SerializeField]
    SaveManager saveManager;

    [SerializeField]
    Text txtPlayerName;

    [SerializeField]
    Button btnRestart;

    [SerializeField]
    Button btnLeaderboardInGameOverUI;

    [SerializeField]
    Slider sliderBgmLevel;
    

    Image _currentPerformanceState;
    Text[] _txtLeaderboards;


    public void ApplyCurrentSoundLevel()
    {
        if (sliderBgmLevel) {
            AudioListener.volume = sliderBgmLevel.value;
        }
    }

    public void SaveCurrentSoundLevel()
    {
        if (sliderBgmLevel) {
            if (!PlayerPrefs.HasKey(SOUND_BGM_KEY)) {
                PlayerPrefs.SetFloat(SOUND_BGM_KEY, 1.0f);

            } else {
                PlayerPrefs.SetFloat(SOUND_BGM_KEY, sliderBgmLevel.value);
            }
        }

        PlayerPrefs.Save();
    }


    void _LoadCurrentSoundLevel()
    {
        if (!PlayerPrefs.HasKey(SOUND_BGM_KEY)) {
            PlayerPrefs.SetFloat(SOUND_BGM_KEY, 1.0f);
        }
        var currentSoundLevel = PlayerPrefs.GetFloat(SOUND_BGM_KEY);
        AudioListener.volume = currentSoundLevel;
        sliderBgmLevel.value = currentSoundLevel;
    }

    void Awake()
    {
        _LoadCurrentSoundLevel();
    }

    void Start()
    {
        UpdateLeaderBoard();
    }

    void Update()
    {
        if (gameOverPanel) {
            if (_gameController.IsGameInit && _gameController.IsGameStart && _gameController.IsGameOver) {
                gameOverPanel.SetActive(true);
            }
        }

        if (player && txtScore) {
            if (_gameController.IsGameInit) {
                txtScore.text = player.Score.ToString();
            }
        }

        if (imgTutorial) {
            if (_gameController.IsGameStart) {
                imgTutorial.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < imgHealths.Length; i++) {
            if (player.Health >= (i + 1)) {
                imgHealths[i].sprite = imgHealthStates[0];
            } else {
                imgHealths[i].sprite = imgHealthStates[1];
            }
        }

        if (_gameController.IsGameStart && _gameController.IsGameOver) {
            var resultText = "Score: ";
            txtResultScore.text = resultText + txtScore.text;
        }

        if (pausePanel && _gameController.IsGamePause) {
            pausePanel.SetActive(true);
        }

        if (_gameController.IsGameStart && _gameController.IsGameOver) {
            UpdateLeaderBoard();
        }

        if (_gameController.IsGameStart && _gameController.IsGameOver) {
            if (txtPlayerName && btnRestart && btnLeaderboardInGameOverUI) {
                btnRestart.enabled = txtPlayerName.text != "";
                btnLeaderboardInGameOverUI.enabled = txtPlayerName.text != "";
            }
        }
    }


    public void UpdateLeaderBoard()
    {
        if (saveManager && txtLeaderboardParent) {
            if (_txtLeaderboards == null) {
                _txtLeaderboards = txtLeaderboardParent.gameObject.GetComponentsInChildren<Text>();
                
            } else {
                for (int i = 0; i < _txtLeaderboards.Length; i++) {
                    var scoreText = (PlayerPrefs.GetInt(SaveManager.SCORE_KEY_PREFIX + i.ToString())).ToString();
                    var playerText = (PlayerPrefs.GetString(SaveManager.NAME_KEY_PREFIX + i));

                    _txtLeaderboards[i].text = "NAME: " + playerText + "    " + "Score: " + scoreText;
                }
            }
        }
    }

    public void ShowPerformance(string performance, float hideDelay)
    {
        switch (performance) {

            case "Miss":
                _currentPerformanceState = imgPerformanceStates[0];
                txtPerfectStack.text = "";

            break;

            case "Bad":
                _currentPerformanceState = imgPerformanceStates[1];
                txtPerfectStack.text = "";

            break;

            case "Good":
                _currentPerformanceState = imgPerformanceStates[2];
                txtPerfectStack.text = "";
            break;

            case "Perfect":
                _currentPerformanceState = imgPerformanceStates[3];
                txtPerfectStack.text = "(x " + player.PerfectStack + ")";

            break;

            default:
            break;
        }

        foreach (Image img in imgPerformanceStates) {
            img.enabled = false;
        }

        if (player.PerfectStack > 1) {
            txtPerfectStack.gameObject.SetActive(true);
        }

        _currentPerformanceState.enabled = true;
        StartCoroutine("_ShowPerformanceCallBack", hideDelay);
    }

    IEnumerator _ShowPerformanceCallBack(float hideDelay)
    {
        yield return new WaitForSeconds(hideDelay);
        _currentPerformanceState.enabled = false;
        txtPerfectStack.gameObject.SetActive(false);
    }
}
