checksum
========

A program to check the hash of a file nicely integrated in the context menu

## Releases ##
* [Update 1.6](https://github.com/victorheld/checksum/releases/tag/v1.6)
* [Update 1.5.1](https://github.com/victorheld/checksum/releases/tag/v1.5.1)
* [Update 1.5.0](https://github.com/victorheld/checksum/releases/tag/v1.5.0)
* [Update 1.0.0](https://github.com/victorheld/checksum/releases/tag/v1.0.0)

## Supported Hashes ##
Here is a list of the currently supported hash types:

* MD5
* SHA1
* SHA256
* SHA512

## Recent changes ##
* Added Context Menu (this requires admin privileges to edit the registry)
* Added Drag&Drop Support
* Fixed NullPointerException on close
* Added threading for file processing to prevent application freezing
* Reworked hash functions -> Added support for large file (>2G) Yay!
* Automatic recalculation of hashes when switching methods

## Known Errors ##
* Drag&Drop not working when elevated

## To-Do List ##

## Authors ##
* [Victorheld](https://github.com/victorheld/)
* [Backshifted](https://github.com/backshifted/) -Lead Programmer
* Icons provided by [Pixel Mixer](http://pixel-mixer.com) and [Micheal Rowe](http://stylicons.com/) under the Creative Commons License
