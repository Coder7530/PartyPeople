# Party People

## Scenario

At Koderly, our employees attend a lot of events throughout the year. We need an application to track our employees, events and which events our employees will be attending.
Koderly have begun creating an application called PartyPeople. The application has been left in a buggy and unfinished state, we need your help to complete it.

### Built With

The PartyPeople application is a ASP.NET Core MVC application, written in C# and targets the .NET 8 Framework. The application makes use of Microsoft SQL Server (MSSQL) as its database of choice.

* C#
* .NET 8
* ASP.NET Core
* MSSQL
* [Dapper](https://github.com/DapperLib/Dapper)
* [FluentValidation](https://docs.fluentvalidation.net/en/latest/)
* [Bootstrap](https://getbootstrap.com/docs/5.3/getting-started/introduction/)

## Getting Started

### Prerequisites

Development for the PartyPeople application relies on the below software:

* Microsoft SQL Server 2019 Express (or higher), incl. Microsoft SQL Server Management Studio
* Visual Studio 2022 Community Edition, incl. ASP.NET and Web Development and SQL Server Data Tools workflows

### Build Steps

1. Clone the repository
	```
	git clone https://github.com/Coder7530/PartyPeople.git
	```

2. Open the PartyPeople.sln in Visual Studio

3. Publish the Database project to your SQL Server instance

4. Update your database connection string in `appsettings.Development.json`
	```
	"ConnectionStrings": {
	  "DefaultConnection": "Server={Server};Database={Database};User Id={SqlUsername};Password={SqlPassword};TrustServerCertificate=True;"
	},
	```

5. Build and Run the Website project in Visual Studio

## Tasks

### Task 1
The PartyPeople project does not currently build. Can you help figure out why, and resolve the build issues?

### Task 2
A bug has been reported that updating events is not working as expected. Can you help by debugging the functionality and resolving the issue?

### Task 3
Koderly would like to track which employees are attending which events. Can you extend the PartyPeople application to add this functionality?

### Task 4
Koderly would like to track which drinks should be ordered for employees. Can you extend the functionality to allow for optionally specifing a ‘Favourite Drink’ for each employee?

### Task 5
Koderly would like to track the five most social employees (employees who’ve attended the most events). Can you add a widget to the Home screen to display this information?

### Task 6
Koderly would like to track upcoming events which have no attendees. Can you add a widget to the Home screen to display this information? 

## Submission
Please commit your work for review by __9AM__ on __3rd of September__. We anticipate that you would spend up to seven hours in completing these tasks, however, you're welcome to spend only as much time as you feel able to. You don't need to complete every task.

1. Fork the PartyPeople project
2. Create a Feature Branch (``` git checkout -b feature/{featureName} ```)
3. Commit your changes (```git commit -m 'My Commit Note'```)
4. Push to your branch (```git push origin feature/{featureName}```)
5. Open a Pull Request

## Contact
If you are struggling with a particular task, or are unable to proceed for any reason, please don’t hesitate to get in touch: [recruitment@koder.ly](mailto:recruitment@koder.ly)