JwtCraft 🔑

JwtCraft is a .NET library that simplifies the creation and validation of JWT tokens, while also providing Refresh Token generation in a fast and secure way. It was designed to make authentication in applications more practical, keeping control over expired tokens and ensuring the security of operations.

Installation is straightforward and can be done via .NET CLI using the command dotnet add package JwtCraft or via PowerShell with Install-Package JwtCraft.

To use the library, configure the static class JwtOptions by setting the following properties: SecretKey, Audience, Issuer and TokenValidInMinutes. ⚠️ It is essential that this configuration is done before calling builder.Services.AddAuthentication(), which takes an IConfiguration parameter and sets up the authentication operations for the application.

Next, register the service in the dependency injection container with builder.Services.AddScoped<ITokenService, JwtService>. After this, the library is ready to use. Any class that requires authentication can receive the ITokenService interface, which provides methods to create JWT tokens, generate Refresh Tokens, and retrieve the ClaimsPrincipal from expired tokens, ensuring greater security and control over sessions.

JwtCraft provides a simple, secure, and efficient integration for .NET applications, eliminating the complexity of manual token-based authentication. For more details, usage examples, and complete documentation, please refer to the official GitHub repository. 🚀