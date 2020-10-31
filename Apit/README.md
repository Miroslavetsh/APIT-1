# APIT - 2020
> version 1.0.1 alpha

| Role       | Developer           | GitHub    | 
|------------|---------------------|-----------| 
| back-end   | Yurii Yermakav      | JuriLents | 
| front-end  | Miroslav Toloshnyi  | MiTo      | 
| testing    | Donylo Baida        | Dan4ef    | 

-----------------------------------------------------------------------------------

## Get started in back-end


#### Init env variables and other support data

`cd [Apit]`

UNIX: `. ./setenv.sh`

WIN:  `setenv.cmd`


-----------------------------------------------------------------------------------

#### Add new DB migration

| Command                                   | Description                         | 
|-------------------------------------------|-------------------------------------| 
| `dotnet tool install --global dotnet-ef`  | install EntityFramework globally    | 
| `cd [DatabaseLayer]`                      | change directory                    | 
| `. ./migration.sh [migration_name] [-v]`  | run bash script (windows . => bash) | 
-----------------------------------------------------------------------------------
