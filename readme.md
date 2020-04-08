# Glue42 configuration service example

This example project shows how to run an ASP .NET REST service that provides applications and layouts stores for a Glue42 instance.

By Glue42 uses applications/layouts defined on the local machine, but can be re-configured to point to a REST service.

# Configuration and start

This example will use the applications as defined in JSON format in **config\\apps** folder. These should be in the standard Glue42 application definition format.

Layouts will be fetched/saved from **config\\layouts** folder.

To start:
* open with Visual Studio 2017 or later
* restore nuget packages
* re-build solution
* press F5 to run

# Glue42 Configuration

You will need to do two changes to re-configure Glue42 to point to a REST service.

For both you will need to edit the `system.json` file located in your Glue42 installation (by default  `%LOCALAPPDATA%\Tick42\GlueDesktop\config` folder)

To enable fetching applications from the REST service, you need to add a new entry to the `appStores` top-level key:

* find the appStores top-level key in the system.json
* add new entry (or replace the existing entries) with teh config bellow
```json
"appStores": [
    {
        "type": "rest",
        "details": {
            "url": "http://<URL>/apps/"           
        }
    }
]
``` 
!!! Note that you need to replace **URL** with the actual url of the service 

To enable fetching/storing layouts from the REST service, you need to update your layouts store to be of type rest and add the correct URL:

* find the layouts top-level key in the system.json
* change the store type to rest and set the correct URL
```json
 "layouts": {
    "store": {
        "type": "rest",
        "restURL": "http://<URL>/"
      }
  } 

```

!!! Note that you need to replace **URL** with the actual url of the service

# Advanced

TBD


