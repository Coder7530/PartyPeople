## Tasks Solutions

### Task 1
The PartyPeople project does not currently build. Can you help figure out why, and resolve the build issues?

### Task 1 Solution
I am getting few build errors pointing that Dapper reference is not found. 
Checked the project references Dapper was not there (expected that it might be broken). 
To add it to the project dependencies I used the following command ```dotnet add package Dapper``` in the PMC after navigating to the Website project folder.

Second after publishing the database and created a SQL user to map to it I built and ran the Website project. 
Got another error: `SqlException: Login failed for user 'iliyand'. Reason: The password of the account must be changed.`. To resolve it I had to change/reset the SQL user password.

### Task 2
A bug has been reported that updating events is not working as expected. Can you help by debugging the functionality and resolving the issue?
What I did first is to create/edit/delete an event to see if it works as supposed to.
Noticed time is not presented as PM or 24h format when selected time was in the afternoon.

Event's start and end date string format had to be changed from `hh:mm`(12h) to `HH:mm`(24h) within the event Index and Details views and the Home page. 

Also spotted the event Id was not passed down to the event update procedure. 

### Task 3
Koderly would like to track which employees are attending which events. Can you extend the PartyPeople application to add this functionality?

The approach I will take is to create attendees section per event to hold the employees that reserved a slot. 

I will need to create an SQL table to store the association between events and employees (one to many relationship).

And will need procedures to query, add and remove event employeees. Plus updating the Event Edit view and the Controller relevant methods.

### Task 4

### Task 5

### Task 6

