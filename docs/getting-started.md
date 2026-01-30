# Getting Started

## Installation

### Requirements:
- A supported Version of Windows; 64 Bit
- An internet connection is probably a good idea

### Zip Archive
Download the latest version for Windows [here](https://github.com/nikeee/HolzShots/releases/latest/download/HolzShots.zip).

### Chocolatey
Also available via [Chocolatey](https://chocolatey.org):
```sh
choco install holzshots
```

## Usage

### CLI Usage
HolzShots is a single-instance application. However, you can interact with the running instance. If you pass command-line parameters, they'll invoke actions on the running instance (or start a new one if HolzShots isn't already running).

```sh
holzshots capture-area
holzshots capture-full

holzshots open-image [optional image path]
holzshots upload-file [optional file path]
```
