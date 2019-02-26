using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteSquare : MonoBehaviour {

    public Image notePrefab;

    private Color colorStart = Color.white;
    private Color colorMidA = Color.gray;
    private Color colorMidB = Color.yellow;
    private Color colorEnd = new Color(.99f,.75f,.25f);
    private Color colorHitStart = Color.blue;
    private Color colorHitEnd = Color.cyan;
    private Color colorMiss = Color.red;

	// Use this for initialization
	void Start () {
		
	}
	
	void Update () {
		
	}

    public void AddNote(float frac, bool hit, bool missed) {
        Image noteImage = Instantiate(notePrefab, transform);
        noteImage.rectTransform.localScale = new Vector3(frac, frac, 1);
        if (frac < 2f/3f) {
            noteImage.color = Color.Lerp(colorStart, colorMidA, frac / (2f/3f));
        } else {
            if (missed) {
                noteImage.color = colorMiss;
            } else if (hit) {
                noteImage.color = Color.Lerp(colorHitStart, colorHitEnd, (frac - (2f / 3f)) * 3);
            } else {
                noteImage.color = Color.Lerp(colorMidB, colorEnd, (frac - (2f / 3f)) * 3);
            }
        }

    }

    public void ClearData() {
        for (int i = transform.childCount - 1; i >= 0; i--) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
