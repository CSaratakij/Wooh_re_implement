using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public const int MAX_LEADERBOARD = 10;
    public const string SCORE_KEY_PREFIX = "LEADERBOARD_SCORE_";
    public const string NAME_KEY_PREFIX = "LEADERBOARD_NAME_";


    public bool IsSavedScore { get { return _isSavedScore; } }


    string[] _scoreKeys;
    string[] _nameKeys;

    int[] _loadedScore;
    string[] _loadedName;

    bool _isSavedScore;


    public SaveManager()
    {
        _scoreKeys = new string[MAX_LEADERBOARD];
        _nameKeys = new string[MAX_LEADERBOARD];
        _loadedScore = new int[MAX_LEADERBOARD];
        _loadedName = new string[MAX_LEADERBOARD];
        _isSavedScore = false;
    }

    public void UpdateLeaderboard(string name, int score)
    {
        if (!_isSavedScore) {
            LoadSavedLeaderboard();
            _ReArrangeBoard(name, score);
            _SaveLeaderboard();
            _isSavedScore = true;
        }
    }

    void Awake()
    {
        for (int i = 0; i < _scoreKeys.Length; i++) {

            _loadedScore[i] = 0;
            _loadedName[i] = "";

            var targetScore = SCORE_KEY_PREFIX + i;
            var targetName = NAME_KEY_PREFIX + i;

            _scoreKeys[i] = targetScore;
            _nameKeys[i] = targetName;

            if (!PlayerPrefs.HasKey(targetScore)) {
                PlayerPrefs.SetInt(targetScore, 0);
            }

            if (!PlayerPrefs.HasKey(targetName)) {
                PlayerPrefs.SetString(targetName, "");
            }
        }
    }

    public void LoadSavedLeaderboard()
    {
        for (int i = 0; i < MAX_LEADERBOARD; i++) {
            var targetScore = _scoreKeys[i];
            var targetName = _nameKeys[i];

            if (PlayerPrefs.HasKey(targetScore)) {
                _loadedScore[i] = PlayerPrefs.GetInt(targetScore);
            }

            if (PlayerPrefs.HasKey(targetName)) {
                _loadedName[i] = PlayerPrefs.GetString(targetName);
            }
        }
    }

    void _ReArrangeBoard(string name, int score)
    {
        var isUpdatedScore = false;

        for (int i = 0; i < MAX_LEADERBOARD; i++) {
            if (isUpdatedScore) {
                break;

            } else {
                if (score > _loadedScore[i]) {
                    for (int j = MAX_LEADERBOARD - 1; j > (i + 1); j--) {
                        _loadedScore[j] = _loadedScore[j - 1];
                        _loadedName[j] = _loadedName[j - 1];
                    }
                    _loadedScore[i] = score;
                    _loadedName[i] = name;
                    isUpdatedScore = true;
                }
            }
        }
        /* if (score > _loadedScore[_loadedScore.Length - 1]) { */

        /*     var newScores = new int[MAX_LEADERBOARD]; */
        /*     var newNames = new string[MAX_LEADERBOARD]; */

        /*     var isUpdatedScore = false; */

        /*     for (int i = 0; i < MAX_LEADERBOARD; i++) { */

        /*         if (isUpdatedScore) { */
        /*             break; */

        /*         } else { */
        /*             if (score < _loadedScore[i]) { */
        /*                 newScores[i] = _loadedScore[i]; */
        /*                 newNames[i] = _loadedName[i]; */

        /*             } else if (score >= _loadedScore[i]) { */

        /*                 newScores[i] = score; */
        /*                 newNames[i] = name; */

        /*                 for (int j = i + 1; j < _loadedScore.Length; j++) { */
        /*                     newScores[j] = _loadedScore[j]; */
        /*                     newNames[j] = _loadedName[j]; */
        /*                 } */

        /*                 _loadedScore = newScores; */
        /*                 _loadedName = newNames; */

        /*                 isUpdatedScore = true; */
        /*             } */
        /*         } */
        /*     } */
        /* } */
    }

    void _SaveLeaderboard()
    {
        for (int i = 0; i < MAX_LEADERBOARD; i++) {
            PlayerPrefs.SetInt(_scoreKeys[i], _loadedScore[i]);
            PlayerPrefs.SetString(_nameKeys[i], _loadedName[i]);
        }

        PlayerPrefs.Save();
    }
}
