checksum [![Build Status](https://travis-ci.org/victorheld/checksum.svg?branch=master)](https://travis-ci.org/victorheld/checksum) [![Build Status](http://80.60.83.220:8070/buildStatus/icon?job=checksum)](http://80.60.83.220:8070/job/checksum/)
========

A program to check the hash of a file nicely integrated in the context menu

## Releases ##
* [Stable Release](https://github.com/victorheld/checksum/releases/latest)

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

## Resources ##
* Icons provided by [Pixel Mixer](http://pixel-mixer.com) and [Micheal Rowe](http://stylicons.com/) under the Creative Commons License
