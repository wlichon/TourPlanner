1. Create PostgreSQL Database with the same user id, password, database name and server, as specified in the appsettings.json file in the TourPlannerAPI project
2. Execute Update-Database command in the package manager console, to create the database using the code-first approach (make sure that the TourPlannerAPI is selected as the default project when doing so).
3. Run the TourPlannerAPI project, a SwaggerUI should show up in your browser and a console window showing that the server is running should appear.
4. Run the TourPlanner project, the TourPlanner UI should appear.
5. You can add Tours, by typing the name of the the tour in the textbox provided in the left pane near the top, and pressing the + button afterwards.
6. You can select a tour and edit its description, starting point and destination.
7. You can add tour logs in the "Route" menu, by clicking the + button, you can also edit them by selecting a log and clicking the "..." button.
8. Clicking the export button will create a pdf with information about the tour. The generated report can be found in the "TourPlanner/bin/Debug/net6.0-windows" folder.
9. Errors are logged in the "TourPlanner/bin/Debug/net6.0-windows/logs" folder.
