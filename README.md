dotnet new webapi -n PizzaStore
cd PizzaStore

# Add required packages
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.EntityFrameworkCore.Tools

# Build and run
dotnet build
dotnet run

PizzaStore/
-Controllers/          # API endpoints
-Data/                # Database context
-Models/              # Entity models and DTOs
-Services/            # Business logic
-Program.cs           # App configuration

Swagger UI: https://localhost:7063/swagger
Base URL: https://localhost:7063/api

Toppings
GET /api/toppings - Get all toppings
GET /api/toppings/{id} - Get topping by ID
POST /api/toppings - Create new topping
PUT /api/toppings/{id} - Update topping
DELETE /api/toppings/{id} - Delete topping

Pizzas
GET /api/pizzas - Get all pizzas
GET /api/pizzas/{id} - Get pizza by ID
POST /api/pizzas - Create new pizza
PUT /api/pizzas/{id} - Update pizza
DELETE /api/pizzas/{id} - Delete pizza

**Sample Requests**
#  Create a Topping
POST /api/toppings
{
  "name": "Pineapple",
  "price": 2.25
}

# Create a Pizza
POST /api/pizzas
{
  "name": "Supreme Pizza",
  "description": "Pizza with multiple toppings",
  "basePrice": 18.99,
  "toppingIds": [1, 2, 3]
}

**Sample Response**
Get alltoppings - https://localhost:7063/api/toppings
[
    {
        "id": 1,
        "name": "Pepperoni",
        "price": 2.50
    },
    {
        "id": 2,
        "name": "Mushrooms",
        "price": 1.75
    },
    {
        "id": 3,
        "name": "Cheese",
        "price": 2.00
    },
    {
        "id": 4,
        "name": "Sausage",
        "price": 3.00
    },
    {
        "id": 5,
        "name": "Bell Peppers",
        "price": 1.50
    },
    {
        "id": 6,
        "name": "Pineapple",
        "price": 2.25
    }
]

**Error Responses**
Duplicate Name (409 Conflict)
Not Found (404
Validation Error (400 Bad Request)

**TESTING**
1. Use Swagger UI - Go to /swagger for interactive testing
2. Use curl - Command line testing
3. Use Postman - Import the API endpoints


