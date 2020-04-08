# Glue42 configuration service example

This example project shows how to run a ASP.NET REST API service that provides applications definitions and layouts for a Glue42 instance.

# How to start 

Open with Visual Studio and run.

This example will use the applications as defined in JSON format in **config\\apps** folder. These should be in the standard Glue42 application definition format.

Layouts will be fetched/saved from **config\\layouts** folder.

# Glue42 Configuration

You will need to edit the `system.json` file located in your Glue42 installation (by default  `%LOCALAPPDATA%\Tick42\GlueDesktop\config` folder)

To enable fetching applications from the REST service, you need to add a new entry to the `appStores` top-level key:

```json
"appStores": [
    {
        "type": "rest",
        "details": {
            "url": "http://<URL>/apps/",
            "auth": "no-auth",
            "pollInterval": 30000,
            "enablePersistentCache": true,
            "cacheFolder": "%LocalAppData%/Tick42/UserData/%GLUE-ENV%-%GLUE-REGION%/gcsCache/"
        }
    }
]
``` 
!!! Note that you need to replace <URL> with the actual url of the service 

To enable fetching/storing layouts from the REST service, you need to update your layouts store to be of type rest and add the correct URL:

```json
 "layouts": {
    "store": {
        "type": "rest",
        "restURL": "http://<URL>/",
        "restFetchInterval": 20
      }
  } 

```

!!! Note that you need to replace <URL> with the actual url of the service

# Advanced

TBD

