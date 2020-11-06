# Kanban Board
This is a very simple Kanban Board build on top of Blazor (wasm), gRPC and RavenDB.

It should show how Drag&Drop, Blazor Component Unit Testing work and how all the technologies come together.

# Setup
To make it run just start the `LinkDotNet.KanbanBoard.Web` project and the `LinkDotNet.KanbanBoard.UI` project.
Furthermore you need a running RavenDb-Instance. There should be a database already created. The name of the database is defined in the appsettings.json.

# Current Features
The following features are fully working:
 * Adding Goals via dialog
 * Displaying Goals, which were created before
 * Moving Goals in between lanes
 * Deleting Goals
 * Adding Subtasks to Goals

 The following technologies are working:
  * RavenDB as Database
  * ASP.NET Core Backend with gRPC Endpoints
  * Blazor WASM with gRPC Client (gRPC Web)
  * Blazor State-Handling
  * Component Unit tests for Blazor
  * .NET 5 driven