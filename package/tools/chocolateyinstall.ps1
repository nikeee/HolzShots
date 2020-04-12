$ErrorActionPreference = 'Stop';

$packageName = $env:ChocolateyPackageName
$toolsDir = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url = 'https://github.com/nikeee/HolzShots/releases/download/v__REPLACE_VERSION__/HolzShots.zip'
$checksum = '__REPLACE_CHECKSUM__'
$checksumType = 'sha265'

Install-ChocolateyZipPackage $packageName $url $toolsDir -checkSum $checksum -checksumType $checksumType
