# aspnetcore-jwt
ASP.Net Core JWT to assist with JWT authentication/authorization for Web API when using Entity Framework Core Identity

## Dependencies
- ASP.Net Core 2.1
  - Microsoft.AspNetCore.Authentication.JwtBearer
  - System.IdentityModel.Tokens.Jwt

## Quickstart
- Install NuGet package
- Initialize JWT in `Startup.ConfigureServices`.  For example:
```
services.AddJwtBearerAuthentication(
    Configuration["Authentication:TokenSigningKey"],
    Configuration["Authentication:Issuer"],
    Configuration["Authentication:Audience"],
    60,
    1440);
```
- Inject `TokenBuilder` class into appropriate controller or class (`AddJwtBearerAuthentication` creates transient dependency rule for this class)
-- The `GenerateToken` method creates a JWT signing token