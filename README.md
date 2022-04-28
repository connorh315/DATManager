# DATManager
C# program for extracting files from newer Traveller's Tales videogames.
# Introduction
Traveller's Tales games use a proprietary archiving format (.DAT files) that need custom tools to be extracted and the archiving format is frequently revised on a game-to-game basis.

Until recently, modders and game enthusiasts would need to use quickbms in combination with a script in order to extract the files, this has worked flawlessly and still continues to. However, The Skywalker Saga has archives that contain >30,000 files per archive and the entire game extracts to a hefty size, and as such the script slugs along in the process due to overhead.

This tool extracts the files much quicker (from 20-30 minutes down to 3 minutes in some cases).

# Usage

Ensure you have .net 5 installed on your PC, then head to Releases and download the latest version. Extract the .zip file and move 'oo2core_8_win64.dll' from your Skywalker Saga game directory to the newly extracted folder. Run the program and click "File" -> "Open .DAT archive" to get started extracting!

# Advantages of using DATManager

- Much quicker at extracting archives
- Allows for extracting individual files
- Provides a user-friendly UI

# Advantages of using QuickBMS

- Supports many more compression methods for legacy archives
- Has had a lot more testing
- Cross-platform + 32-bit support
- Supports extracting legacy games much better

# List of supported games/archives

- The Skywalker Saga
- **Some** of the previous games, such as Dimensions or DCSV, can extract to a certain extent
