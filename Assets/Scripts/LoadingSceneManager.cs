/* 制作日
*　製作者
*　最終更新日
*/

using UnityEngine;
using UnityEngine.SceneManagement;
 
public class LoadingSceneManager : MonoBehaviour {
 
	private void Awake()
	{
		SceneManager.LoadScene("MainGameScene");
	}
}