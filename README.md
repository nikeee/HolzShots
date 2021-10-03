# ![Logo](src/HolzShots/Resources/Logo-32x32.png) HolzShots ![CI](https://github.com/nikeee/HolzShots/workflows/CI/badge.svg) ![Chocolatey Version](https://img.shields.io/chocolatey/v/holzshots)
A lightweight screen capturing utility that gets out of your way.
- No settings UI – configurable via JSON, like VSCode.
- Upload to you own services via config files – like ShareX – but keep them updated – like user scripts.
- No bloat like a DNS changer, hash validation or an FTP client. Just screenshots and screen recordings.
- Tries to cover 80% of use cases - if you need more features for some tasks, try other tools[^1].

## Requirements
- Requires a supported Version of Windows 10; 64 Bit
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

### Licenses
HolzShots is built on the shoulders of giants and uses various tools and libraries which all hav their respective license. Thank you!

[^1]: Like [ScreenToGif](https://github.com/NickeManarin/ScreenToGif) or [OBS](https://obsproject.com)
