
# Document Management System

## General organization of this project

The project is divided into the following Source Folders.

 1. **API**
 This folder contains project for backend api service.

 2. **DataMigrationConsoleApp**
 This folder contains project for Data Migration Console Application .
 
 3. **Domain**
 Database context and model classes resides in this project.
 
 4. **Infrastructure**
 Project in this folder will be used to manage audit trail.

 5. **OutLookAddIn**
 This folder contains project for OutLook VSTO Add In for Sending Email body as .eml file and email attanchment on Web DMS .

 6. **Shared**
This section contains the library projects which will be shared by all other projects, if required.

 7. **Solution Items**
This section contains versioned sql script files which can be run sequentially to build database using sql server management studio.

 8. **WindowsServices**
 This folder contains project for Windows Service for Apply GDPR  .



## Instructions for Running

Create Database in Sql Server Management Studio Named as 'DMS'

Run the All Scripts Solution Items/Migration in DMS Database with Sql server management studio
first Run 001-Initial_Migration.sql then follow filename order 002,003 and so on and run all scripts.

And create Auduit database run sql scripts which are in Solution Items/Migration/Auduit/001-Initial_Migration.sql Directory

### 4. Repository Pattern

In order to use repository pattern built into domain, each model class should implement IEntity interface.

This implementation can be done in partial classes of models, which are in custom folder.


While creating a table in database its primary key must be of type int with name "Id" along with bit "IsActive", bigint "CreatedAt" and "UpdatedAt" 
for IEntity interface

While creating a table in database its primary key must be of type int with name "ComapnyId" along with int "Id", bit "IsActive", bigint "CreatedAt" and "UpdatedAt" 
for ICompanyEntity interface

Doing this will automatically implement IEntity,ICompanyEntity interfaces and model can be managed using repository pattern.

## Default Username & Password

{
  "email": "super@mailinator.com",
  "password": "Admin!23"
}

## Audit database

Audit logs in th Api are created asynchronously using RabbitMQ event bus.

Api stores audit logs in a separate database.

### To Set up RabbitMQ

In order to set up RabbitMQ please follow instruction in following link:

<a href="https://www.rabbitmq.com/download.html" target="_blank">Downloading & Installing RabbitMQ</a>

### To Create Audit Database

Run script file Migrations\Audit\001-Initial-Migration.sql to create audit database.
