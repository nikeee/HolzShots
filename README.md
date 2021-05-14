# ![Logo](src/HolzShots/Resources/editcutSmall.png) HolzShots ![CI](https://github.com/nikeee/HolzShots/workflows/CI/badge.svg)
A lightweight screenshot utility that gets out of your way.
- Configurable via JSON, like VSCode.
- Upload to you own services via config files, like ShareX, but keep them updated like user scripts.
- No bloat like DNS changers or video recording. If you want to do video, use a tool that does it properly (like [ScreenToGif](https://github.com/NickeManarin/ScreenToGif)).

## Requirements
- Requires >= Windows 7 SP1; 64 Bit
- An internet connection is probably a good idea

## Installation
Download the latest release [here](http://github.com/nikeee/HolzShots/releases/latest/download/HolzShots.zip).

Or install it using [Chocolatey](https://chocolatey.org):
```cmd
choco install holzshots
```

## CLI Options
HolzShots is a single-instance application. However, you can interact with the running instance.
If you pass command-line parameters, they'll invoke actions on the running instance (or start a new one if HolzShots isn't already running).

Currently, these are supported:
```cmd
holzshots capture-area
holzshots capture-full

holzshots open-image [optional image path]
holzshots upload-image [optional image path]
```
Feel free to integrate this into your tooling!
