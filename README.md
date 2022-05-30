# DATManager
C# program for extracting files from newer Traveller's Tales videogames.
# Introduction
Traveller's Tales games use a proprietary archiving format (.DAT files) that need custom tools to be extracted, and the archiving format is frequently revised on a game-to-game basis.

Until recently, modders and game enthusiasts would need to use QuickBMS in combination with a script in order to extract the files, this has worked mostly flawlessly and still continues to. However, The Skywalker Saga has archives that contain >30,000 files per archive and the entire game extracts to a hefty size, and as such the script slugs along in the process due to overhead.

This tool extracts the files much quicker (from 20-30 minutes down to 3 minutes in some cases).

# Usage

Ensure you have **.NET 6** installed on your PC, then head to Releases and download the latest version of DATManager. 

Extract the .zip file (and if you plan on extracting The Skywalker Saga, you must move `oo2core_8_win64.dll` from your Skywalker Saga game directory to the newly extracted folder).

Run the program and click `File` > `Open .DAT archive` to get started extracting!

# Advantages of using DATManager

- Much quicker at extracting archives
- Allows for searching for files in archives
- Allows for extracting individual files
- Provides a user-friendly UI

# Advantages of using QuickBMS

- Has had a lot more testing
- Cross-platform + 32-bit support

# List of supported games/archives

All game archives are now theoretically supported with DATManager. If you find one that doesn't work, drop me a message or start an Issue!

# Credits
Due to time constraints, I've had to include a QuickBMS library into my code to help mop up some files that don't extract properly (this sounds ironic I know).

QuickBMS can be found [here](https://aluigi.altervista.org/quickbms.htm) and is written by Luigi Auriemma.

7-Zip is used and can be found here: [7-zip](https://www.7-zip.org/).