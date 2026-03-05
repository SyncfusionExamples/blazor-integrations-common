
# Syncfusion Blazor Grid + JWT Starter 

This is a **minimal, working** example showing:

- Blazor WebAssembly **Client** (port 5003)
- ASP.NET Core **Server API** protected by **JWT** (port 5005)
- **Syncfusion Blazor Grid** fetching data from the protected API using **Authorization: Bearer <token>**

## How to run (requires .NET 8+ SDK)

Open two terminals in this folder.

### 1) Run the Server (API)
```bash
cd Server
# first time restore packages
dotnet restore
# run on http://localhost:5005
dotnet run --urls http://localhost:5005
```

### 2) Run the Client (Blazor WASM)
```bash
cd Client
# restore
dotnet restore
# run on http://localhost:5003
# Dev server will open a browser tab
dotnet run --urls http://localhost:5003
```

## Demo steps
1. In the client site, open **Login** (left sidebar).
2. Use **username**: `admin` and **password**: `admin123`.
3. Navigate to **Orders**. You should see the Syncfusion Grid with data.

## Notes
- The Syncfusion theme is included via `<link>` in `Client/wwwroot/index.html`.
- If you have a Syncfusion license key, register it in `Client/Program.cs` where indicated.
- For production, store the JWT secret and issuer in safe configuration (User Secrets / Key Vault) and enable HTTPS.

## Tech choices (short)
- Grid data access uses **SfDataManager** with `HttpClientInstance` so JWT is attached automatically by `AuthMessageHandler`.
- The API endpoint returns `{ result, count }` to match **UrlAdaptor** requirements.
