JuSaber is a Jubeat clone built for keyboard with the ability to read
Beat Saber map files. For usability reasons (to ensure the player doesn't get
lost on the keyboard), this game uses a 3x4 grid centered on the home row rather
than the standard Jubeat 4x4. At the moment, the game will only play the hardest
difficulty found within a song folder, difficulty selection is not enabled.
The keyboard is used as follows:

 ––––––––\
|R|T|Y|U|\
 ––––––––\
|F|G|H|J|\
 ––––––––\
|V|B|N|M|\
 ––––––––

To add Beat Saber maps to the game, insert the map folder into
Assets/StreamingAssets/CustomSongs/. For example, if you were to add Believer,
the folder structure would be
Assets/StreamingAssets/CustomSongs/Believer/info.json,
Assets/StreamingAssets/CustomSongs/Believer/Expert.json,
Assets/StreamingAssets/CustomSongs/Believer/song.ogg,
etc.
