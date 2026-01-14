
# GestionStock (Blazor WebAssembly)

Overview
- Single-page Blazor WebAssembly front-end (client) for a simple stock/product management UI.
- Connects to a remote Web API (Swagger: https://demogestionstockapi-cgdrb9bpa5d3dfht.westeurope-01.azurewebsites.net/swagger/index.html).
- Key client features: product gallery, product details, add product (with image + categories), client-side state service.

Technology stack
- .NET 8
- Blazor WebAssembly (client-side)
- C#
- HttpClient for API calls
- Bootstrap (UI and icons)
- Visual Studio 2026 (recommended for development)
- Internal MultiSelect component for category selection

Repository-level features (global)
- Product gallery (listing with images, price badges and categories)
- Product details view (reads selected product from client state)
- Add product form:
  - Fields: Name, Description, Price, Stock, Categories, Image upload (preview)
  - Sends multipart/form-data to API combined with query string parameters
- Category retrieval for selection lists
- Simple client-side state service to pass selected product between pages

Important dependencies
- .NET 8 SDK
- Visual Studio 2026 (or `dotnet` CLI)
- NuGet packages typically used in this workspace:
  - `Microsoft.AspNetCore.Components.WebAssembly`
  - `Microsoft.AspNetCore.Components.Web`
  - (Bootstrap is used for UI; icons via Bootstrap Icons)
- Browser must allow large file reads if testing image upload in Blazor (InputFile limits apply)

API explanation (based on client usage and Swagger)
Base URL (used in `Program.cs`):
https://demogestionstockapi-cgdrb9bpa5d3dfht.westeurope-01.azurewebsites.net/

Swagger UI:
https://demogestionstockapi-cgdrb9bpa5d3dfht.westeurope-01.azurewebsites.net/swagger/index.html

Endpoints the client calls
- GET `api/Category`
  - Response: `List<CategorieDto>` where `CategorieDto` has `{ Id, Name }`
  - Used to populate category multi-select.

- GET `api/Product`
  - Response: `List<ProductDto>` where `ProductDto` includes at least:
    `{ Id, Name, Description, ImageUrl, Price, Categories: [{ Id, Name }] }`
  - Used to build product gallery and details.

- POST `api/Product` (used by client)
  - Current client behavior: builds query string with `Name`, `Description`, `Price`, `Stock` and one or more `Categories={id}` entries, then posts `multipart/form-data` with the uploaded file in the `Image` field.
  - Example (client-side behavior):
    - Request URI: `POST /api/Product?Name=Beer&Description=...&Price=4.5&Stock=10&Categories=1&Categories=2`
    - Body: `multipart/form-data` with field `Image` containing the image file.
  - Expected result (client checks success flag): API should return a success indicator (e.g., 200 OK and JSON or string).

Future features to develop (client + server)
- Authentication & authorization (JWT + role-based UI)
- Edit / Delete product UI + API support
- Categories CRUD (create/edit/delete) + UI
- Stock movements/history (inbound/outbound) and audit logs
- Pagination, sorting, filters (server-side) for product lists
- Search with filters (by category, price range, text)
- Image storage improvements (use blob/storage service for images + CDN)
- Better error handling & consistent API error payloads
- Internationalization / localization
- Offline support / synchronization (PWA)
- Unit/integration tests and end-to-end tests + CI/CD pipeline

API adaptations recommended to make a fully functional, robust app
1. Use canonical, RESTful payloads for creation/editing
   - Accept a JSON body for product creation/update containing:
     ```
     {
       "name": "...",
       "description": "...",
       "price": 4.5,
       "stock": 10,
       "categories": [1,2]
     }
     ```
   - Accept the image as multipart/form-data (or allow separate file upload endpoint). Preferably:
     - `POST /api/Product` with JSON body OR
     - `POST /api/Product?multipart` using multipart where JSON part name is e.g. `product` and file part `image`.
2. Return the created/updated resource (201 Created with Location header + response body containing the new resource) instead of a simple string/success flag.
3. Support structured categories and arrays in request bodies (not just repeated query params).
4. Add pagination, filtering and sorting parameters on `GET /api/Product` (e.g., `page`, `pageSize`, `search`, `categoryId`).
5. Provide consistent error responses (standard error DTO with code/message/validation errors).
6. Implement CORS policy allowing the WASM client origin (for local dev).
7. Add authentication endpoints (login, refresh tokens) and protect write endpoints.
8. Store images in blob storage and return durable `ImageUrl` values.
9. Implement model validation and return detailed validation messages on 400 responses.
10. Add API documentation in Swagger for all endpoints and examples for multipart uploads.

How to run locally (quick)
1. Ensure .NET 8 SDK and Visual Studio 2026 installed.
2. In `GestionStock.Wasm/Program.cs` the base address is currently set to:
   `https://demogestionstockapi-cgdrb9bpa5d3dfht.westeurope-01.azurewebsites.net/`
   - If you run a local API, update the BaseAddress to your local API URL.
3. Start the API (if local) and ensure CORS allows the Blazor client origin.
4. Run the Blazor WebAssembly project (set as startup) and press F5 or `dotnet run` from the client project.
5. Navigate to `/products` to view the product gallery and `/products/add` to create a product.

Notes and quick diagnostics
- If products fail to load, check console/network for the GET `api/Product` response and CORS errors.
- If image upload fails, confirm API accepts `multipart/form-data` and the field name matches (`Image`) and that request size limits are configured on server.
- The current client expects `api/Category` and `api/Product` endpoints and the DTO shapes described above.

 

 
