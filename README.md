# GlamSiphon

WIP. Model exporter for FFXIV characters-- after the effects of Penumbra mods and Glamourer equipment alterations.


## Main Points

* Simple functional plugin
  * Slash command
  * Main UI
  * Settings UI
  * Image loading
  * Plugin json
* Simple, slightly-improved plugin configuration handling
* Project organization
  * Copies all necessary plugin files to the output directory
    * Does not copy dependencies that are provided by dalamud
    * Output directory can be zipped directly and have exactly what is required
  * Hides data files from visual studio to reduce clutter
    * Also allows having data files in different paths than VS would usually allow if done in the IDE directly


The intention is less that any of this is used directly in other projects, and more to show how similar things can be done.

## How To Use

### Getting Started

To begin, [clone this template repository][new-repo] to your own GitHub account. This will automatically bring in everything you need to get a jumpstart on development. You do not need to fork this repository unless you intend to contribute modifications to it.

Be sure to also check out the [Dalamud Developer Docs][dalamud-docs] for helpful information about building your own plugin. The Developer Docs includes helpful information about all sorts of things, including [how to submit][submit] your newly-created plugin to the official repository. Assuming you use this template repository, the provided project build configuration and license are already chosen to make everything a breeze.

[new-repo]: https://github.com/new?template_name=GlamSiphon&template_owner=goatcorp
[dalamud-docs]: https://dalamud.dev
[submit]: https://dalamud.dev/plugin-development/plugin-submission





https://github.com/xivdev/Xande
https://github.com/Ottermandias/Glamourer
https://github.com/Ottermandias/OtterGui/tree/f55733a96fdc9f82c9bbf8272ca6366079aa8e32
https://github.com/Ottermandias/Penumbra.Api/tree/80f9793ef2ddaa50246b7112fde4d9b2098d8823
https://github.com/Ottermandias/Penumbra.GameData/tree/545aab1f007158a5d53bc6a7d73b6b2992deb9b3