
## Setup

- Create a modern WinUI multi-page desktop app named MusicCreator
- Make the navigation panel resizable

# On Startup

- Create a FirstRun service that runs on app startup. When running for the first time, open a dialog for customizing the app.
- Set default path of SoundClipsPath to Music\SoundClips.
- Show only OK button on FirstRunDialog

- Show first run dialog when app starts. 
- Add a button to setting panel. when clicked, reset all settings to their default values
- When the FirstRunDialog dialog is run, if the SoundClips folder doesn't exist, create it.

# AI Functionality

- Add functionality allowing the app to connect to 



## Settings

- Add a dropdown to Settings page. This will allow users set app theme for Dark mode, light mode, and System default.
- Remember all settings when they change
- Remember the width of the navigation bar, and if it is expanded
- Remember position and size of main window

- Add a folder path to Settings called SoundClips. Include a folder selection button. Save changes.
- Make default path of to Music\SoundClips\
- Add a button next to the browse button in Settings. When clicked, open a Windows explorer windows pointing to the specified folder

- Change App to Full screen when user types F11. Restore when user types F11 again.

## Library

- List all music files in the SoundClips folder. Display it in the Library panel
- Update Library panel when files in the SoundClips folder changes

## Music Synthesis

- Add a tab to the list of pages called MusicSynthesis
- Add functionality to the MusicSynthesis page to allow a user to enter AI queries. Add settings to connect to AI models, both locally and online
- Add a help page explaining how to set up connecting to an AI model, both locally and online
- Show a message to set up AI settings in the MusicSynthesis tab if setup isn't complete, and hide the prompt textbox and generate button until setup is complete


