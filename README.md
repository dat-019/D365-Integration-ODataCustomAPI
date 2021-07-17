# D365-Integration-ODataCustomAPI

The solution desmontrates building a custom OData API for integrating data between a Dynamics 365 CE instance and external systems.

# Project structure

## 1. APIService project
 The custom OData API is developed in ASP.NET MVC pattern and leveraging the token-based authentication with the help of OWIN framework. This API will be hosted in specific local server to exchange the data between D365 instance and external system.
 ### Main flow:
    -- Once the appliation starts, the "Application_Start" in the Global.asax.cs runs first. This method is configured to acquire the access token for D365 instance authentication.
    -- The OWIN startup method is invoked once the API url containing the "/api/login" is called (e.g: https://localhost:44333/api/login) to acquire the access token for custom API access.
    -- The APIs methods are defined in the CrmController class. E.g. https://localhost:44333/api/crm/fetch retrieves the data from D365 based on the defined fetch query.
    
## 2. APIService.Provider project
 The project defines helper methods to be used in the APIService project.
 
## 3. CustomHttpRequest project
 The project defined the custom http methods.
 
## 4. Plugin_DynamicServices project
 The D365 CE plugin based project defines the methods to be triggered via the custom action messages. The custom action message deployed inside D365 instance and is called from the custom API - APIService project.
 
## 5. APIConsumer project
 The console application is the custom API consumer desmonstrating how to invoke custom API methods.
