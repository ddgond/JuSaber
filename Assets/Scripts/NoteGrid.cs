using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGrid : MonoBehaviour {

    public NoteSquare[] noteSquares;

	// Use this for initialization
	void Start () {
        Hide();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClearSquares() {
        foreach (NoteSquare noteSquare in noteSquares) {
            noteSquare.ClearData();
        }
    }

    public void AddNote(int index, float frac, bool hit, bool missed) {
        noteSquares[index].AddNote(frac, hit, missed);
    }

    public void Display() {
        
    }

    public void Hide() {
        
    }
}
