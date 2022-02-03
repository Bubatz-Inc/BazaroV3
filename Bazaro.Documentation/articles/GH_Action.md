# GitHub Actions
## Dotnet build
We are building on every PR, thats destination is Main. We are building and are running the Tests.

## Docker Image Build
When you merge into the Main this action is run, it builds a Docker Image and pushes it to Dockerhub, 
unter folgendem Repo "oldviking/bazaro" 