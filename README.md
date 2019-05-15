# WeatherMakerPlugin

Adds support for Digital Ruby's Weather Maker asset to The Digital Painting asset for Unity.

To include this plugin in your Digital Painting use the following:

```
cd Assets
git submodule add git@github.com:DigitalPainting/WeatherMakerPlugin.git
```

## Use

  1. Create a configuration file using `Assets -> Create -> Wizards Code/PLUGIN TYPE ... ` There will be entries in this folder for each of the plugins you have installed
  2. Drag the generated configuration file into the `Configuration` field of the `PLUGIN Manager` component attached to the `DigitalPaintingManager` game object
  3. Double click the configuration file and configure as appropriate

## Available Plugins

Below you will find a list of available plugins along with an indication of any dependency they have. Note that some plugins fit into multiple categories and are thus listed multiple times.

### Day Night Cycle

These plugins provide a day night cycle that changes the lighting according to the time of day in the scene. The Digital Painting comes with a very basic Day/Night Cycle, the plugins here will provide a much better lucking experience.

#### Digital Ruby's Weather Maker

[Digital Ruby's Weather Maker](https://assetstore.unity.com/packages/tools/particles-effects/weather-maker-sky-weather-water-volumetric-light-60955) includes an advanced Day Night cycle. Please purchase and install this asset first.

### Weather 

The following plugins add weather systems to The Digital Painting.

#### Digital Ruby's Rain Maker (Free)

