using System.IO;
using UnityEngine;
public class Song {
    
    private SongInfo songInfo;
    private string path;

    public Song(SongInfo songInfo, string path) {
        this.songInfo = songInfo;
        this.path = path;
    }

    public string GetName() {
        return songInfo.songName;
    }

    public string GetSoundFilePath() {
        return Path.Combine(path,songInfo.GetHighestDifficulty().audioPath);
    }

    public MapData GetMapData() {
        return FileLoader.LoadMapData(songInfo.GetHighestDifficulty(), path);
    }

    public float GetOffset() {
        return songInfo.GetHighestDifficulty().offset / 1000f;
    }
}
