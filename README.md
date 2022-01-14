# forum-onnorokom

The project is made for Onnorokom Software Limited

Configuration:
1. You need to create database first and then change appsettings.json file with connection string and smtp configuration.
2. No need to create table. You have to apply migrations. 
    a. dotnet ef database update --project Onnorokom.Forum.Web --context ApplicationDbContext
    b. dotnet ef database update --project Onnorokom.Forum.Web --context MembershipDbContext
4. Defaul Moderator email: moderator@email.com and password: moderator@email.com

