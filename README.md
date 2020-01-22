# GuildWars Build Saver

[Planned] API and frontend for managing GW2 builds

## API
Generate builds using [GW2 API v2](https://api.guildwars2.com/) and save using Azure Cosmos.
Endpoints:
  GET:
    /api/gw/{path}: get response from GW api
    /api/builds: get all saved builds
    /api/builds/{id}: get saved build
  POST:
    /api/builds: add new build with JSON format
  PUT:
    /api/builds/{id}: update an existing build
    
Example:
  GET /api/builds/1
  Response: 
  {
    "name": "My build"
    "build": {
      "weapons": {
        "main_weapon_1": "", 
        "main_weapon_2": "",
        "off_weapon_1": "",
        "off_weapon_2": ""        
      },
      "skills" {
        "healskill": "",
        "utility_1": "",
        "utility_2": "",
        "utility_3": "",
        "elite_skill": ""
      }
    }
  }

## Frontend
To be continued
