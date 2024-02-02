/* 制作日
*　製作者
*　最終更新日
*/

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
 
public class GameOverManager : MonoBehaviour, IFGameOverManager, IFInputGameOver
{
    [SerializeField, Tooltip("GameOverロゴ")]
    private GameObject _gameOverLogo = default;

    public void DecisionKeyDown()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void GameOverEvent()
    {
        _gameOverLogo.SetActive(true);
        // タイマーストップ
    }
}