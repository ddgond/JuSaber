using UnityEngine;

[System.Serializable]
public class MapData {
    public string _version;
    public float _beatsPerMinute;
    public int _beatsPerBar;
    public float _noteJumpSpeed;
    public float _shuffle;
    public float _shufflePeriod;
    public float _time;
    public Note[] _notes;
}
