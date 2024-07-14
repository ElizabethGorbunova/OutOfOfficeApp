# OutOfOfficeApp
The main functionality of the application is: 
1. employees sending leave requests and HR managers or project managers approving or rejecting these requests;
2. assigning to appropriate employees by the project manager.

The web service OutOfOfficeApp was built using the .NET Core framework and represents a REST API for five services: Employee Service, Leave Request Service, Approval Request Service, Project Service, and Account Service. These services handle the main business logic. MSSQL Management Studio was used for configuring the data structure during development and for storing the provided data. The ORM used in the OutOfOfficeApp project is Entity Framework. All database sets and entities corresponding to the tables were created via migrations.

The application has six main entities: Employee, LeaveRequest, ApprovalRequest, Project, User, and Role. The last two relate to the authentication and authorization mechanism, which is based on JWT Token(using Microsoft.AspNetCore.Authentication.JwtBearer library). Each user has it's own authorization rights and in case of forbidden access, receives an appropriate status code. All endpoints were tested in Postman and Swagger.
