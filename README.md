# GuildWars Build Saver

API for managing builds in Guild Wars 2

## API
Generate builds using a saved copy of [GW2 API v2](https://api.guildwars2.com/) and save using Azure CosmosDb.
Endpoints:
<pre>
  GET:
    /api/skill: get all skills
    /api/skill/{id}: get skill by id
    /api/build: get all saved builds
    /api/build/{id}: get saved build    
  POST:
    /api/builds: add new build by form
  PUT:
    /api/builds/{id}: update an existing build
</pre>    
Example:
<pre>
  GET /api/builds/MyBuild
  
  Response: 
  
  {
    "name": "MyBuild"
    "build": {
        "profession": "",
        "name": "",
        "id": "",
        "mainHandWeapon": "", 
        "offHandWeapon": "",
        "mainHandSwap": "",
        "offHandSwap": "",
        "heal": "",
        "utility1": "",
        "utility2": "",
        "utility3": "",
        "elite": ""
    }
  }
</pre>

Note: Requires a T-SQL Server running, see GuildWarsBuildSaver/appsettings.json

## Frontend
To be continued
