# Innosphere Application

## Overview

Innosphere is a .NET application designed to streamline the job application process. It provides a comprehensive system for managing employees, job listings, job seekers, job applications, and user roles. The application employs Identity Server for authentication, ensuring secure access to its features.

## Features

1. **Authentication with Identity Server**: Utilizes Identity Server for secure authentication and access control.
2. **Employee Management**: Enables the management of employee profiles within the organization.
3. **Job Management**: Facilitates the creation, editing, and removal of job listings.
4. **Job Seeker Profiles**: Allows job seekers to create and manage their profiles, including resume upload functionality.
5. **Job Application Tracking**: Provides a system for job seekers to apply for listed jobs and tracks their application status.
6. **User Role Management**: Administers user roles to control access and permissions within the application.

## Setup

To set up Innosphere locally, follow these steps:

1. Clone the repository from [GitHub Repo URL].
2. Install dependencies by running `dotnet restore`.
3. Configure the Identity Server for authentication.
4. Configure the MSSQL database connection string in `appsettings.json`.
5. Migrate the database schema using Entity Framework Core migrations (`dotnet ef database update`).
6. Run the application using `dotnet run`.
7. Access the application through the provided URL.

## Technologies Used

- **Backend**: .NET (C#)
- **Authentication**: Identity Server
- **Database**: MSSQL

## Future Plans

In the future, Innosphere will integrate a frontend using React to enhance user experience and interactivity.

## Contributing

Contributions to Innosphere are welcome! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request on the GitHub repository.

## Contact

For any inquiries or support, feel free to contact Vloriat Berisha at vb43315@ubt-uni.net.

Thank you for using Innosphere! We hope it streamlines your job application process effectively.
