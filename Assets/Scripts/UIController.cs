using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Canvas canvas;
    public GameObject mainMenuPrefab;
    public AudioSource audioSource;
    public NoteGrid noteGrid;
    public Text comboText;
    private GameObject mainMenu = null;
    private List<Song> loadedSongs;
    private int selectedSongIndex = 0;
    private MenuMode mode = MenuMode.Main;

    private Song currentSong = null;
    private MapData currentMapData = null;
    private AudioClip currentSongClip = null;

    private float songStartTime = 0;
    private int comboCount = 0;

    private bool downPressed = false;
    private bool upPressed = false;

    private static string[] keyMap = {
        "V", "B", "N", "M",
        "F", "G", "H", "J",
        "R", "T", "Y", "U"
    };
    private bool[] inputUsed = {
            false, false, false, false,
            false, false, false, false,
            false, false, false, false
        };
    private static float hitPrecision = 0.5f;
    private float speedScale = 0.8f;

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        MainMenuLoop();
        CheckKeyUp(); // Prevent same key being used for two notes
        GameplayLoop();
        CheckKeyDown(); // Prevents holds from counting
    }

    private void MainMenuLoop() {
        if (mode != MenuMode.Main) {
            return;
        }
        if (Input.GetAxisRaw("Vertical") < -0.5)
        {
            if (!downPressed)
            {
                downPressed = true;
                NextSong();
            }
        }
        else
        {
            downPressed = false;
        }

        if (Input.GetAxisRaw("Vertical") > 0.5)
        {
            if (!upPressed)
            {
                upPressed = true;
                PreviousSong();
            }
        }
        else
        {
            upPressed = false;
        }

        if (Input.GetAxisRaw("Submit") > 0.5)
        {
            SelectSong();
        }
    }

    private void CheckKeyUp() {
        for (int i = 0; i < keyMap.Length; i++) {
            if (Input.GetAxisRaw(keyMap[i]) < 0.5) {
                inputUsed[i] = false;
            }
        }
    }

    private void GameplayLoop() {
        if (mode != MenuMode.Game) {
            return;
        }

        if (Input.GetAxisRaw("Cancel") > 0.5) {
            mode = MenuMode.Main;
            audioSource.Stop();
            currentSong = null;
            currentMapData = null;
            currentSongClip = null;
            ResetCombo();
            noteGrid.ClearSquares();
            return;
        }

        //TODO: Bombs are currently becoming notes. Don't them.

        noteGrid.ClearSquares();
        foreach (Note note in currentMapData._notes) {
            float noteTime = note._time / currentMapData._beatsPerMinute * 60f + songStartTime - 0 * currentSong.GetOffset();
            int keyMapIndex = note._lineLayer * 4 + note._lineIndex;
            string noteInput = keyMap[keyMapIndex];
            float notePrecision = hitPrecision / (currentMapData._beatsPerMinute * speedScale) * 60f;
            if (noteTime > Time.time - notePrecision && noteTime < Time.time + notePrecision * 2) { // Visual timing
                float frac = (Time.time - noteTime + notePrecision * 2f) / (notePrecision * 3f);
                if (noteTime < Time.time + notePrecision && noteTime > Time.time - notePrecision) { // Active hitbox timing
                    if (Input.GetAxisRaw(noteInput) > 0.5 && note.NotYetHit() && !inputUsed[keyMapIndex]) { // Make sure each key press counts for one note
                        Debug.Log("Note hit!");
                        note.Hit();
                        IncrementCombo();
                        inputUsed[keyMapIndex] = true;
                    }
                } else { // Inactive hitbox timing (too early)
                    if (Input.GetAxisRaw(noteInput) > 0.5 && note.NotYetHit() && note.NotYetMissed() && !inputUsed[keyMapIndex]) {
                        Debug.Log("Note hit too early!");
                        note.Miss();
                        ResetCombo();
                        inputUsed[keyMapIndex] = true;
                    }
                }
                noteGrid.AddNote(keyMapIndex, frac, !note.NotYetHit(), !note.NotYetMissed());
            }
            if (noteTime < Time.time - notePrecision && note.NotYetHit() && note.NotYetMissed()) { // Hitbox missed
                Debug.Log("Note missed!");
                note.Miss();
                ResetCombo();
            }
        }
    }

    private void CheckKeyDown()
    {
        for (int i = 0; i < keyMap.Length; i++)
        {
            if (Input.GetAxisRaw(keyMap[i]) > 0.5)
            {
                inputUsed[i] = true;
            }
        }
    }

    private void PreviousSong() {
        if (mode != MenuMode.Main) {
            return;
        }
        selectedSongIndex -= 1;
        if (selectedSongIndex < 0) {
            selectedSongIndex = loadedSongs.Count - 1;
        }
        UpdateSelectedSongText();
    }

    private void NextSong() {
        if (mode != MenuMode.Main)
        {
            return;
        }
        selectedSongIndex += 1;
        if (selectedSongIndex >= loadedSongs.Count) {
            selectedSongIndex = 0;
        }
        UpdateSelectedSongText();
    }

    private void SelectSong() {
        if (mode != MenuMode.Main)
        {
            return;
        }
        mode = MenuMode.LoadingSong;
        currentSong = GetSelectedSong();
        currentMapData = currentSong.GetMapData();
        Debug.Log("First note is at time: " + currentMapData._notes[0]._time);
        WWW www = new WWW("file://" + currentSong.GetSoundFilePath());
        Debug.Log("Now loading " + currentSong.GetName());
        while (!www.isDone) {
            Debug.Log("Progress is: " + www.progress);
            continue;
        }
        Debug.Log("Progress is: " + www.progress);
        audioSource.clip = www.GetAudioClip(false, false);
        Debug.Log("Loading audio data succeeded: " + audioSource.clip.LoadAudioData());
        PlaySong();
    }

    private void PlaySong() {
        if (mode != MenuMode.LoadingSong) {
            return;
        }
        audioSource.Play();
        songStartTime = Time.time;
        mode = MenuMode.Game;
    }

    public void LoadMainMenu() {
        if (mainMenu != null)
        {
            Destroy(mainMenu);
        }
        mainMenu = Instantiate(mainMenuPrefab, canvas.transform);
        UpdateSelectedSongText();
    }

    public void LoadMainMenu(List<Song> songs) {
        loadedSongs = songs;
        LoadMainMenu();
    }

    private void UpdateSelectedSongText() {
        Text selectedSongText = mainMenu.GetComponentInChildren<Text>();
        if (selectedSongText != null) {
            selectedSongText.text = GetSelectedSong().GetName();
        }
    }

    private Song GetSelectedSong() {
        return loadedSongs[selectedSongIndex];
    }

    private void IncrementCombo() {
        comboCount++;
        UpdateComboText();
    }

    private void ResetCombo() {
        comboCount = 0;
        UpdateComboText();
    }

    private void UpdateComboText() {
        comboText.text = "Combo: " + comboCount;
    }
}
