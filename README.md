# Imagegram
A system that allows you to upload images and comment on them

## How Far Did I Get?
- I have done the implementation that described in the User Stories section as well as most of the functional requirements and the non-functional requirements.
- However, I did not check the slow connection cases. About the `Maximum image size - 100MB`, by default Azure Function limits the HTTP request length (please refer [this](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-http-webhook-trigger?tabs=csharp#limits))

## Technologies
Azure Function, Sql Server, Dapper.

## How to build code locally?

  1. Clone the repository (skip this step if you have the project on your machine) and install or update .NET Core version >= 3.0 and Visual Studio 2019 or above.
  
  2. Running two scripts in the Scripts folder (database_script and data) and please run the `database_script` first. After running these scripts, please update the sql connection string (SqlConnectionString) in the `local.settings.json`
  
  3. Open the Visual Studio as an Administrator to avoid some permission issues.

  4. Finally, click `F5` to start the project normally.

## APIs
  - Creating single post:
    + [Post] /api/Post
    + Form-Data: Caption, PostBy, Image
    + Response: Empty (need to improve)
    
  - Create single comment:
    + [Post] /api/Post/Comment
    + Json: { "PostId": "8", "Comment": "OlalaHey Boy", "CommentBy": "1" }
    + Response: Empty
    
  - Get posts:
    + [GET] /api/Post
    + QueryString: MaxItemPerPage (number), ContinuationToken (string)
    + Response: 
    ```
    { "Posts": [ { "PostId": 8, "Caption": "Hello World", "ImageUrl": null, "Comments": [ { "CommentText": "OlalaHey Boy", "PostId": 8, "CommentBy": 0, "Id": 0, "CreatedDateTime": "2021-11-21T13:36:17.680009Z", "UpdatedDateTime": null } ] } ], "ContinuationToken": "eyJGcm9tSWQiOjgsIk1heEl0ZW1QZXJQYWdlIjoxfQ==" }
    ```
## Deploy To Prod (Azure)
  1. Build the project.
  3. Create an Azure SQL Database resource and run the scripts inside the folder `Scripts`. 
  4. After that, Create Azure Key Vault resource and add `RunningEnvironment=AzureEnv` and `SqlConnectionString={AzureSqlConnectionString}`
  5. Then, create a Managed Identity resource and do the mapping between Azure Function and Key Vault resource. Please refer to this [link](https://daniel-krzyczkowski.github.io/Integrate-Key-Vault-Secrets-With-Azure-Functions/). This step will help us to map whatever we defined in the key vault to our project via Enviroment Variables.
  6. After setting up the Managed Identity and Key Vault, we need to create an Azure Function resource and follow the instruction below
  7. Publish the project and follow the instruction in this [link](https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs?tabs=in-process#publish-to-azure)
  8. Finally, the Imagegram is ready to test.

## Improvements
There are many improvement for this project following below:

+ Creating unit test for the project.

+ Add more comments for classes, properties, orchestrator and activity to make the project understandable.

+ Remove some hard-coding code.

+ Using redis to store some stable data such as ResourceTypes.

+ Implement `FluentValidation` Framework to validate the inputted data. 

+ Doing more test with slow connection cases. 

+ Something that I haven't figured it out yet :D!

Thanks for reading this.;)
