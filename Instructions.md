
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

