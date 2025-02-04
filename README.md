# GameStore API

GameStore API is a web API for managing games, including functionalities like user registration, purchasing games, wishlist management, and game publishing by developers and publishers. This project is built with ASP.NET Core and uses MediatR, FluentValidation, Redis caching, and other modern technologies.
## Features

- **Onion Architecture/Clean Architecture**: The application follows Onion Architecture (or Clean Architecture), promoting separation of concerns, maintainability, and testability by dividing the application into distinct layers.
- **CQRS Pattern**: Implements Command Query Responsibility Segregation (CQRS) to separate the write operations (commands) from the read operations (queries), improving scalability and performance.
- **User Management**: Register, log in, and manage users.
- **Game Management**: Publishers can upload, update, and delete games, and users can view the uploaded games
- **User Authentication**: JWT-based registration and login.
- **Purchasing**: Users can buy games and manage their transactions.
- **Wishlist**: Users can add games to their wishlist.
- **System Requirements**: Track and display system requirements for each game.
- **Caching**: Uses Redis for caching frequently accessed data.

## Technologies Used

- **ASP.NET Core**
- **Entity Framework Core**
- **JWT Bearer Authentication**
- **SQL Server**
- **AutoMapper**
- **Swagger**
- **Mediatr**
- **Redis**: For caching using Docker.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation

1. **Clone the repository:**

    ```bash
    git clone https://github.com/GiorgiChekurishvili/GameStore.git
    ```

2. **Navigate to the project directory:**

    ```bash
    cd GameStore
    ```

3. **Restore dependencies:**

    ```bash
    dotnet restore
    ```

4. **Update your `appsettings.json` with the correct connection strings:**

    ```json
   "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=GameStoreDb;TrustServerCertificate=True;Trusted_Connection=True;"
    },
    ```

    - **SQL Server**: Ensure you have a SQL Server instance running locally or remotely.
    - **Redis**: The Redis connection string is typically set to `localhost:6379` if running locally.

5. **Apply database migrations:**

    ```bash
    dotnet ef database update
    ```

6. **Run the application:**

    ```bash
    dotnet run
    ```

## API Endpoints

### Authentication

- **POST /api/authentication/Login**: Logs in a user.
- **POST /api/authentication/Register**: Registers a new user.

### Cart

- **GET /api/cart/GetCartGames**: Retrieves games currently in the user's cart.
- **POST /api/cart/AddGameToCart**: Adds a game to the user's cart.
- **POST /api/cart/CheckoutGames**: Processes the checkout for games in the cart.
- **DELETE /api/cart/RemoveAllGamesFromCart**: Removes all games from the user's cart.
- **DELETE /api/cart/RemoveGameFromCartRequest**: Removes a specific game from the user's cart.


### Category

- **GET /api/category/GetAllCategories**: Retrieves all categories of games.
- **GET /api/category/GetCategoryById/{id}**: Retrieves a specific category by its ID.
- **POST /api/category/AddCategory**: Adds a new game category (Admin only).
- **PUT /api/category/UpdateCategory/{id}**: Updates an existing category (Admin only).
- **DELETE /api/category/DeleteCategory/{id}**: Deletes a category (Admin only).
  
### Game

- **GET /api/game/GetAllGames**: Retrieves all games available on the platform.
- **GET /api/game/GetAllGamesByPublisher**: Retrieves all games by a specific publisher.
- **GET /api/game/GetAllGamesByCategory/{categoryId}**: Retrieves all games by a specific category.
- **GET /api/game/GetGameById/{gameId}**: Retrieves a specific game by its ID.
- **POST /api/game/AddGame**: Adds a new game (Publisher only).
- **PUT /api/game/UpdateGame/{id}**: Updates a specific game (Publisher only).
- **DELETE /api/game/DeleteGame/{id}**: Deletes a game (Publisher only).


### System Requirements

- **GET /api/systemrequirements/GetSystemRequirementsForGame/{id}**: Retrieves the system requirements for a game.
- **POST /api/systemrequirements/AddSystemRequirements**: Adds system requirements for a game (Publisher only).
- **PUT /api/systemrequirements/UpdateSystemRequirements/{id}**: Updates system requirements for a game (Publisher only).

 
 ### Library

- **GET /api/library/GetAllLibraryGames**: Retrieves all games in the user's library.
- **GET /api/library/GetLibraryGameById/{id}**: Retrieves a specific game from the user's library.


### Transactions

- **GET /api/transaction/GetAllTransactionsByUserId**: Retrieves all transactions for the logged-in user.
- **GET /api/transaction/GetUserBalance**: Retrieves the user's current balance.
- **POST /api/transaction/FillBalance**: Adds balance to a user in order to purchase a game.


### Wishlist

- **GET /api/wishlist/GetWishlistGames**: retrieves a list of all games currently in the user's wishlist.
- **GET /api/wishlist/AddGameToWishlist/{gameId}**: adds a specific game to the user's wishlist using the game ID.
- **POST /api/wishlist/RemoveGameFromWishlsit/{gameId}**: removes a specific game from the user's wishlist using the game ID.

## Contributing

Feel free to contribute by opening pull requests or issues. Your contributions are welcome.

## Security
JWT authentication is used for securing the endpoints.
Different user roles like Admin and Member are managed.

