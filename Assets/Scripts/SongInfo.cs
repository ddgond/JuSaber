using UnityEngine;

[System.Serializable]
public class SongInfo {
    public string songName;
    public string songSubName;
    public string authorName;
    public float beatsPerMinute;
    public float previewStartTime;
    public float previewDuration;
    public string coverImagePath;
    public string environmentName;
    public DifficultyLevel[] difficultyLevels;

    private static string[] difficulties = {
        "Easy",
        "Normal",
        "Hard",
        "Expert",
        "ExpertPlus"
    };

    public DifficultyLevel GetHighestDifficulty() {
        DifficultyLevel highestDifficulty = null;
        int highestRank = -1;
        foreach (DifficultyLevel level in difficultyLevels) {
            for (int rank = 0; rank < difficulties.Length; rank++) {
                if (level.difficulty.Equals(difficulties[rank]) && rank > highestRank) {
                    highestDifficulty = level;
                    highestRank = rank;
                    break;
                }
            }
        }
        return highestDifficulty;
    }
}
