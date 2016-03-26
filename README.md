# fglrxindicator

This is a simple indicator for Unity 7 (the desktop enviornment for Ubuntu) that shows the temperature of your AMD Radeon GPU. I made this indicator for a [video](https://www.youtube.com/watch?v=DBPdWCGbQVg) I made on my YouTube channel.

## Uses

This indicator is no more than a simple icon that appears in the indicator area in Unity 7. When clicked it displays GPU temperature information.

## Compatibility

fglrxindicator was written using Mono and targeted at Unity 7. Ubuntu has switched to Unity 8 which uses QT instead of GTK (which is what this indicator is written with) which means this is no longer compatible with Ubuntu.

## Dependencies

The following dlls are used to build this project:

* appindicator-sharp
* atk-sharp
* glib-sharp
* gtk-sharp
