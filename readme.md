# Glue42 Configuration Service Example

[**Glue42 Enterprise**](https://glue42.com/desktop-enterprise/) uses application and layout configurations defined on the local machine, but can also be reconfigured to fetch them from a REST service.

This example project shows how to run an ASP .NET REST service that provides [application](https://docs.glue42.com/glue42-concepts/application-management/overview/index.html#application_stores-rest_service_app_stores) and [layout](https://docs.glue42.com/glue42-concepts/windows/layouts/overview/index.html#layout_stores-rest_service_layout_store) stores for **Glue42 Enterprise**.


## Configuration and Start

This example uses application definitions in JSON format located in the `Glue42RestConfig\config\apps` folder. Layout definitions are fetched from and saved in the `Glue42RestConfig\config\layouts` folder. You can also use your own application definitions, but they must be in the standard Glue42 [application definition](https://docs.glue42.com/developers/configuration/application/index.html) format.

To start:
- open with Visual Studio 2017 or later;
- restore NuGet packages;
- rebuild solution;
- press F5 to run;

## Glue42 Enterprise Configuration

To enable fetching application and layout definitions from the REST service, you need to edit the `appStores` and `layouts` top-level keys in the **Glue42 Enterprise** `system.json` file, usually located in the `%LOCALAPPDATA%\Tick42\GlueDesktop\config` folder.

### Applications

Find the `appStores` top-level key in the `system.json` file and add a new entry (or replace existing entries) with the following configuration:

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
*Note that you need to replace **`<URL>`** with the actual URL of the service.* 

### Layouts

Find the `layouts` top-level key in the `system.json` file and edit the `store` property - change the `type` to `"rest"` and assign the URL of the service to the `restURL`:

```json
 "layouts": {
    "store": {
        "type": "rest",
        "restURL": "http://<URL>/"
      }
  } 

```
*Note that you need to replace **`<URL>`** with the actual URL of the service.*

## Advanced

TBD
