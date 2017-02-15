using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

public class DataController : MonoBehaviour 
{
	private RoundData[] allRoundData;

    private PlayerProgress playerProgrees;
    private string gameDataFileName = "data.json";

	void Start ()  
	{
		DontDestroyOnLoad (gameObject);
        LoadGameData(); 
		
		SceneManager.LoadScene ("MenuScreen");
	}
	
	public RoundData GetCurrentRoundData()
	{
		return allRoundData [0];
	}

    public void SubmitNewPlayerScore(int  newScore)
    {
        if(newScore > playerProgrees.highScore)
        {
            playerProgrees.highScore = newScore;
            SavePlayerProgress();
        }
    }

    public int GethighestScore()
    {
        return playerProgrees.highScore;
    }

    public void LoadPlayerProgress()
    {
        playerProgrees = new PlayerProgress();
        if(PlayerPrefs.HasKey("highScore"))
        {
            playerProgrees.highScore = PlayerPrefs.GetInt("highScore");
        }
    }

    public void SavePlayerProgress()
    {
        PlayerPrefs.SetInt("highScore",playerProgrees.highScore);
    }

    public void LoadGameData( )
    {
        string filePath = Path.Combine(Application.streamingAssetsPath,gameDataFileName);

        if(File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            GameData loadedGameData = JsonUtility.FromJson<GameData>(dataAsJson);

            allRoundData = loadedGameData.allRoundData;
        }
        else
            Debug.LogError("Cannot Load Game Data");
    }
}