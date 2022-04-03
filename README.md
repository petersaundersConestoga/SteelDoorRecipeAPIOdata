# SteelDoorRecipeAPIOdata #
* simple little repo that contains the current odata implementation
* had to be rewritten as full api as for whatever reason minimal apis do not allow odata

## How to run ##
1. Clone the repository to your computer
2. Install or Run Sql Server Management Studio, SMSS
3. In SMSS click Tasks > Restore
4. Choose Restore From Device
5. Open the db folder in the repository you cloned 
6. Choose the latest dated db 
7. Click restore
8. Open Visual Studio 2022 or later
9.1. Open appsettings.json and edit 'Default Connection' to match the db your just restored
9.2. To get the connection string in visual studio click View Databases > Connect to the db instance
9.3. After having selected the db view the connection properties and copy the connection string
10 Tell visual studio to clean and build the project, it needs to restore NuGET packages, etc
11 Run!
