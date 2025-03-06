# Summary

Budget manager application is tool to manage your family budget

# Database container setup

## 1. Install docker desktop or rancher desktop

docker desktop

https://www.docker.com/products/docker-desktop/

rancher desktop

https://rancherdesktop.io/

## 2. Open terminal or powershell and go to this location: ./budget-manager-server

ex: cd C:\Projects\Courses\BudgetManager\budget-manager-server

## 3. Run docker-compose up command

ex: docker-compose up

## 4. To stop container use ctrl+c from your keyboard

# Run Api using Visual Studio Code

1. Open project using Visual Studio Code
2. Open new terminal using Terminal-> New terminal
3. Navigate to budget-manager-server\src\BudgetManager.Api with cd budget-manager-server\src\BudgetManager.Api
4. Type dotnet run in terminal
5. Swagger url for budget manager api: https://localhost:7188/swagger/index.html  

# Run Api using Visual Studio

1. Set BudgetManager.Api as start up project
2. Run the project using F5 or Debug -> Start Debugging
3. Swagger url should be open 