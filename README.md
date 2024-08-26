# Rengomedia

## Objective

Develop specified modules using ASP.NET Core MVC, EF Core, and MSSQL as part of a backend system. Focus on functionality over design, utilizing the default template for UI elements.

## Modules to be Implemented

### Department/Sub-Departments Module

**Description:** Create a module to manage departments and sub-departments.

**Features:**
- Departments can contain multiple sub-departments.
- Sub-departments can further contain sub-departments, creating a multi-level hierarchy.
- Implement functionality to select a department/sub-department and display:
  - A list of all sub-departments within the selected department/sub-department.
  - A list of all parent departments up to the top-level for the selected department/sub-department.

**Fields:**
- Department Name
- Department Logo

### Reminder Module

**Description:** Develop a module to set reminders that trigger email notifications.

**Features:**
- Set reminders with a title and specific date-time for sending an email notification.

**Fields:**
- Title
- Date-Time

## Steps to Run the Project

1. **Add-Migration InitialCreate**: Generate the initial migration for the database schema.
2. **Update-Database**: Apply the migration to the database.
3. **Run SQL Query**:
    ```sql
    INSERT INTO [ringomedia].[dbo].[Departments] ([Name], [LogoPath], [ParentDepartmentId])
    VALUES ('Main Department', '/logos/default.png', NULL);
    ```

## Setup

Ensure you have the following prerequisites installed:
- .NET Core SDK
- SQL Server

Clone the repository and run the commands in the order specified to set up and run the project.

For any issues or contributions, please refer to the issue tracker or open a pull request.

