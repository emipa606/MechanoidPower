# GitHub Copilot Instructions for Mechanoid Power (Continued) Mod

## Mod Overview and Purpose

The Mechanoid Power (Continued) mod is an update to Trunkens mod, introducing a new energy source for RimWorld. It adds a unique power-generating structure called the Mechanoid Power Source, which can be constructed using a rare and obtainable item, the Mechanoid Power Cell. This mod is designed for advanced colonies and requires specific research to implement.

## Key Features and Systems

- **Mechanoid Power Source**: A new building that generates power passively.
- **Mechanoid Power Cell**: A unique item obtained from crashed ship parts.
- **Research Requirement**: Requires the research of "Mechanoid Power Source" to use the cell in a power generator.
- **High Market Value**: The Mechanoid Power Cell has a high market value of 2000.
- **Security and Protection**: Requires security and protection to function properly.
- **Instability**: Can be damaged by gunfire and explosions.

## Coding Patterns and Conventions

- **C# Files**: The mod includes 5 C# files, each with specific responsibilities:
  - `ShipWreck_CompSnowExpand.cs`: Handles snow expansion on ship wrecks.
  - `CompProperties_SnowExpandCrashWreck.cs`: Properties for snow expansion on ship wrecks.
  - `MechPowerMod.cs`: Main mod class.
  - `Static.cs`: Static methods and properties.
  - `MechPowerSetting.cs`: Settings management for the mod.

- **XML Integration**: The mod uses XML for defining various game elements, including:
  - `CustomNotificationLetters.xml`: Custom notification letters.
  - `ResearchProjects.xml`: Research project definitions.
  - `Incidents_Map_Threats.xml`: Incident definitions related to map threats.
  - `Buildings_Exotic.xml`: Definitions for exotic buildings.
  - `Items_Exotic.xml`: Definitions for exotic items.

## Harmony Patching

The mod uses Harmony for patching vanilla game mechanics to integrate new features. Key patches include:
- Modifying ship wreck behavior to include the Mechanoid Power Cell as a potential drop.
- Adjusting research requirements and prerequisites.
- Integrating the Mechanoid Power Source into the game's power generation system.

## Suggestions for Copilot

### General Tips

- **Understand Vanilla Mechanics**: Familiarize yourself with RimWorld's vanilla mechanics, especially those related to research, power generation, and ship wrecks.
- **Use Harmony for Patches**: When adding new features, consider using Harmony to patch existing methods rather than replacing them entirely.
- **Follow XML Conventions**: Ensure that XML files are well-structured and follow RimWorld's conventions for defining game elements.

### Specific Suggestions

- **ShipWreck_CompSnowExpand.cs**:
  - **CompTick**: Consider adding additional logic to handle the Mechanoid Power Cell's unique properties.
  - **PostExposeData**: Ensure data persistence for the Mechanoid Power Cell.

- **MechPowerMod.cs**:
  - **ExposeData**: Implement data exposure for mod settings to ensure proper save/load functionality.

- **MechPowerSetting.cs**:
  - **DoSettingsWindowContents**: Enhance the settings window with more options for configuring the Mechanoid Power Source.
  - **WriteSettings**: Ensure settings are written correctly to the mod's configuration file.

By following these guidelines and utilizing GitHub Copilot effectively, you can contribute to the development of the Mechanoid Power (Continued) mod and enhance its functionality.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

