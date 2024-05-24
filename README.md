# Simple Banking API

This is a simple banking API for managing accounts and transactions.

## Installation

To install and run the API locally, follow these steps:

1. Clone this repository to your local machine.
2. Navigate to the project directory.
3. Run `dotnet build` to build the project.
4. Run `dotnet run` to start the API server.

## Endpoints

### GET /balance

Retrieves the balance of a specified account.

**Parameters:**
- `account_id`: The ID of the account to retrieve the balance for.

### POST /event

Creates a new transaction event, such as deposit, withdrawal, or transfer.

**Body:**
- `type`: Type of the event (`deposit`, `withdraw`, or `transfer`).
- `origin`: ID of the origin account (required for `withdraw` and `transfer`).
- `destination`: ID of the destination account (required for `deposit` and `transfer`).
- `amount`: Amount of the transaction.

## Swagger Documentation

![image](https://github.com/gabrielsvpinheiro/SimpleBanking.API/assets/81546353/3dfa14c3-ea05-4e96-b677-aba8866a1faf)

## Testing

To run the automated tests, execute `dotnet test` in the test project directory.
