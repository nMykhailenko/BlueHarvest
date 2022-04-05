# BlueHarvest API

## Solution structure

BlueHarvest.API - the API base on Modular Architecture.
Modules - contains **users** & **transactions** modules.
Shared - contains shared Application and Infrastructure layers.

Every modules has such structure:
    - Application - contains all business logic and contracts for Infrastructure layer
    - Domain - contains all domain entities, enums, aggregation root etc.
    - Infrastructure - contains third party integration like database connections, api clients etc. 

## Blueharvest API Flow

 1. You need to send **POST** request via  **/api/v1.0/users**. (if the balance greater than 0, the system will send a message to create transaction instance)
 2. You will recieve create user.
 3. You can find user information via /api/v1.0/users/{userId}.
 4. You can find user's transactions via /api/v1.0/users/{userId}/transactions

## TODO

    Add Authorization/Authentication 
    Add integration, pefomance tests.

 ## How to run

 In **/deployment** folder you need to run:
 ```
 docker-compose build
 ``` 
 then 
 ```
 docker-compose up
 ```