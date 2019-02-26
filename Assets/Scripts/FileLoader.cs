using System.IO;
using UnityEngine;

public class FileLoader{
    public static string customSongsFolderName = "CustomSongs";

    public static void LoadSongData(SongManager songManager)
    {
        songManager.ClearSongs();
        string customSongsFolderPath = Path.Combine(Application.streamingAssetsPath, customSongsFolderName);
        DirectoryInfo customSongsFolderInfo = new DirectoryInfo(customSongsFolderPath);

        foreach (DirectoryInfo songFolderInfo in customSongsFolderInfo.GetDirectories()) {
            SongInfo songInfo = null;
            foreach (FileInfo songFileInfo in songFolderInfo.GetFiles()) {
                if (songFileInfo.Name.Equals("info.json")) {
                    string songInfoJson = File.ReadAllText(songFileInfo.FullName);
                    songInfo = JsonUtility.FromJson<SongInfo>(songInfoJson);
                }
            }
            if (songInfo == null) {
                continue;
            }
            Song song = new Song(songInfo, songFolderInfo.FullName);
            songManager.AddSong(song);
        }
    }

    public static MapData LoadMapData(DifficultyLevel difficultyLevel, string songPath) {
        string difficultyPath = Path.Combine(songPath, difficultyLevel.jsonPath);
        string difficultyJson = File.ReadAllText(difficultyPath);
        return JsonUtility.FromJson<MapData>(difficultyJson);
    }
}
