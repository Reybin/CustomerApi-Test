# Propellerhead Test

## Information
> Propellerhead API Assessment: A simple and intuitive customer management and note-taking api, without front-end
> Technologies built with:
- .Net 6
- EntityFramework Core (ORM)
- Automapper (Data transfer purpose)
- xUnit (Unit Test Package)
- Bogus (Fake Data generator for test purposes)

## Prerequisites (Build): 
- Visual Studio 2022
- Local MSSQL SERVER 18 
 (The current windows user must have access to the local server, feel free to change the connection string on appsettings.json to use an onpremises server)

## Tests are currently available in the TEST project

## Steps
(After cloning the repo and opening it in visual studio 2022)
- Run the migration on the package manager console
  ![run-migration](https://github.com/Reybin/CustomerApi-Test/blob/master/PropellerHeadTest/readme-images/run-migration.png)
- Run API project from the solution

> Please select the API on the top bar during both steps.  
 ![run](https://github.com/Reybin/CustomerApi-Test/blob/master/PropellerHeadTest/readme-images/selectAPI.png)

## Filter Example:

 ![run](https://github.com/Reybin/CustomerApi-Test/blob/master/PropellerHeadTest/readme-images/filter.png)

## Duration Time: 7h
> With more time, I would like to add more API validation, custom responses, Test, Authorization and Azure secrets for the connection string.
