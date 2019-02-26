using System.Collections;
using System.Collections.Generic;

public class SongManager {
    private List<Song> songs = new List<Song>();

    public SongManager() {
        
    }

    public void AddSong(Song song) {
        songs.Add(song);
    }

    public List<Song> GetSongs() {
        return new List<Song>(songs);
    }

    public void ClearSongs() {
        songs.Clear();
    }
}
