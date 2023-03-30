# Lagalt - Backend
## Overview
This repository contains the code for the backend of the LagAlt project handed out as a mandatory project at the Noroff acceleration program (spring 2023).

The project consists of creating the website lagalt.no, a platform that aims to facilitate connecting individuals in creative fields with projects that requires specific skills. In order to apply to join or create projects, the user must register with a username and create a password. After registering, the user accesses their profile page which will contain a portfolio of their projects, a user description and the users skills. The website also has a main page which contains projects by other users. By clicking on one of these projects, the user will get to the project page, the last of the three pages the website contains. In this page, application to a specific project can be done. 

The website uses Oauth through Keycloack for handling authentication, and the website's backend exposes REST API endpoints and utilizes an Azure SQL database. 

## Website Page
The functionalities for the website can be tested at https://lagalt.no/

## Development
The development is done using .NET 6.0 Model Controller View framework for creating APIs. More information about the .NET MVC framework can be found from the following link: https://learn.microsoft.com/en-us/aspnet/core/mvc/overview?view=aspnetcore-6.0.

## Installations and prerequisites
In order to clone repo and play with the project yourself, you need to download the following:
* .NET 6.0 (recommended with visual studio)
* Microsoft.EntityFrameworkCore.SqlServer

### Technologies
* C#
* .NET 6.0
* Azure SQL database

## Authors
The authors of this project are:
* Nora Sophie Backe
* Sofie Bjørneng
* Håvard Lund
* Tine Lovise Storvoll
* Sverre Vinje
--------------------------------------------------------------------------------------------------------------------------------------------------------------------
# User manual
The following youtube video goes through the functionalities of the website and how to use them:  
https://www.youtube.com/watch?v=C1nhJqucTGk
