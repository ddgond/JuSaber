using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public UIController uiController;
    private SongManager songManager = new SongManager();

	// Use this for initialization
	void Start () {
        FileLoader.LoadSongData(songManager);
        uiController.LoadMainMenu(songManager.GetSongs());
        ScreenSetup();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void ScreenSetup() {
        Screen.fullScreen = false;
        Screen.SetResolution(1024, 1024, false);
    }
}
