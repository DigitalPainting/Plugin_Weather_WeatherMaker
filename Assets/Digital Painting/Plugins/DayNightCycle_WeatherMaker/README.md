# Digital Ruby's Weather Maker

[Digital Ruby's Weather Maker](https://assetstore.unity.com/packages/tools/particles-effects/weather-maker-sky-weather-water-volumetric-light-60955) doesn't just provide weather effects, it also has a Day Night cycle. At present we only have integration with the Day Night Cycle. Patches are welcome to add weather integrations too.

# Usage

Add the `WeatherMakerCommon/Scenes/WeatherMakerCommon` scene as an additive scene.

If you are only using the Day Night Cycle you can disable all the children except `DayNightCycle`, `SkySphere` and `LightManager` from the `WeatherMakerSystem` game object.

If you main scene already has a manager with a `DayNightCycle` and/or `WeatherSystem` component you should remove or disable these.

If your main scene already has a camera you should either remove it or ensure that it has (at least) the same components as the `WeatherMakerMainCamera`, and delete this latter camera.
    