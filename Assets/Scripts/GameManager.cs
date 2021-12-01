using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using System;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { set; get; }
	public static string TARGET_OBJECT_TAG = "Target";
	public GameObject[] levelPrefabs;

	private List<GameObject> allTargets;
	private int currentLevel = 0;
	private bool isGameComplete = false;

	private void Awake()
	{
		Instance = this;
		DontDestroyOnLoad(this.gameObject);

		//Load stuff

		ChangeScene("Menu");
		SceneManager.sceneLoaded += OnLoadedLevel;
	}

	private void Update()
	{
		if (isGameComplete) GameCompleted();
	}

    public void ChangeScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	public void OnLoadedLevel(Scene scene, LoadSceneMode mode)
	{
		isGameComplete = false;
		if (scene.name == "Game")
		{
			CreateLevel();  
		}
	}

	private void CreateLevel()
	{
		allTargets = new List<GameObject>();
		if (currentLevel < levelPrefabs.Length)
		{
			GameObject level = GameObject.Instantiate(levelPrefabs[currentLevel]);
			foreach(Transform child in level.transform)
            {
				allTargets.Add(child.gameObject);
            }
		}
		else
		{
			isGameComplete = true;
		}
	}

	public void RemoveBlock(GameObject block)
	{
		if(allTargets.Find(b => b == block))
		{
			allTargets.Remove(block);
			Destroy(block, 3f);
		}

		if (allTargets.Count <= 0)
		{
			LevelCompleted();
		}
	}

	private void LevelCompleted()
	{
		currentLevel++;
		CleanLevel();
		CreateLevel();
	}

	private void GameCompleted()
	{
		currentLevel = 0;
		ChangeScene("Menu");
	}


	private void CleanLevel()
	{
		GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
		foreach(GameObject bullet in bullets){
			Destroy(bullet);
		}
	}
}
