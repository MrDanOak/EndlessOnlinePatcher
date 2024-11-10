# Endless Online Patcher 🌙

This is a tool for patching [Endless Online](https://endless-online.com) because the developers were so rude as to come back after 10+ years and start working on it again 😂

# Building

This project targets .NET 7, so you would require the .NET SDK version 7 at a minimum. I have built this project using Visual Studio 2022, so it should just work™️ if you were to build using Visual Studio 2022.

# How it works

This tool connects to the eo main server with an init packet to query what version the server is running. If the server version is different from the version stored locally in the .txt file, then it will download the latest client version automatically from the official EO website.

This tool gets the installed location of Endless online either by checking the parent folder to the tool (e.g. This tool would install to `Endless Online/Patcher` if you were to use the installer provided) or if there is no Endless.exe in the parent folder, it defaults to `C:/Program Files (x86)/Endless Online/` as the install directory for the game.

If your local version is older than the most recent version according to the client download page, then you will get the option to patch which will download the .zip archive of the latest version, extract it to a temporary directory, and then copy all the files across to the install location (except the config) and then give you the option to launch the game.

# Features

* Automatic pickup of new version available
* Automatic patching
* Ability to launch the game directly from the patcher
* UI fits the theme of the game
* Ignores the downloaded config so it doesn't overwrite the config you have locally
* Installs the game if not already installed

![New Version Available](./docs/img/new_version.png)

![Patching](./docs/img/patching.png)

# Todo
* EO Directory picker
    * Optional: Save this in some sort of config so it wouldn't need to be reset on every load
* Checkbox for auto-launching the application after patch or if patching is not necessary 
    * Optional: Save this in some sort of config so it wouldn't need to be reset on every load
* Embed News?
* Tests???
