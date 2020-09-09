using UnityEngine.SceneManagement;

public class LevelLoader
{
    public bool IsLastLevel;

    public LevelLoader()
    {
        var levelName = SceneManager.GetActiveScene().name;
        IsLastLevel = levelName == "Level3";
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
