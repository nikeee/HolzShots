# HolzShots ![CI](https://github.com/nikeee/HolzShots/workflows/CI/badge.svg)
> A screenshot utility.

## Requirements
- Requires >= Windows 7 SP1

## Installation
Download the latest release [here](http://github.com/nikeee/HolzShots/releases/latest/download/HolzShots.zip).

Or install it using [Chocolatey](https://chocolatey.org):
```shell
choco install holzshots
```

## CLI Options
HolzShots is a single-instance application. However, you can interact with the running instance.
If you pass command-line parameters, they'll invoke actions on the running instance.

Currently, these are supported:
```shell
holzshots capture-area
holzshots capture-full

holzshots open-image [optional image path]
holzshots upload-image [optional image path]
```
Feel free to integrate this into your tooling!
