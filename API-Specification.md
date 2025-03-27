# EStore API Specification

## API Information
- **Name**: EStoreAPI
- **Version**: 1
- **Description**: An ASP.NET Core API for managing customers, orders and products.
- **Contact Information**: sofia.k.nordstrom@gmail.com

---


## Customer Endpoints

### Get All Customers
- **HTTP Method:** `GET`
- **Route:** `/api/customers`
- **Query Parameters:**  
  - `searchQuery` (optional): A string to filter customers by email.
- **Responses:**
  - **200 OK**: Returns a list of customers in the form of `CustomerDto`.
  - **404 Not Found**: If no customers are found.
  - **500 Internal Server Error**: If there is a server error.

#### Example Request:

GET /api/customers?searchQuery=johan


#### Example Response:

```
[
  {
    "id": "67e2aa66092c57ebff2e026c",
    "firstName": "Johan",
    "lastName": "Nilsson",
    "email": "johan.nilsson@example.com",
    "mobile": "46702345678",
    "street": "Lilla Vägen 34",
    "city": "Göteborg",
    "postalCode": "456 78",
    "country": "Sverige"
  },
  {
    "id": "67e2aade092c57ebff2e026d",
    "firstName": "Lisa",
    "lastName": "Johansson",
    "email": "lisa.johansson@example.com",
    "mobile": "46703456789",
    "street": "Solgatan 56",
    "city": "Malmö",
    "postalCode": "789 01",
    "country": "Sverige"
  }

```
---

### Get Customer by ID
- **HTTP Method:** `GET`
- **Route:** `/api/customers/{id}`
- **Path Parameters:**
  - `id` (string): The unique ID of the customer.
- **Responses:**
  - **200 OK**: Returns the customer details in the form of `CustomerDto`.
  - **404 Not Found**: If the customer is not found.
  - **500 Internal Server Error**: If there is a server error.

#### Example Request:

GET /api/customers/605c72ef153207001f7a5f1

#### Example Response:
```
{
  "id": "67e2aade092c57ebff2e026d",
  "firstName": "Lisa",
  "lastName": "Johansson",
  "email": "lisa.johansson@example.com",
  "mobile": "46703456789",
  "street": "Solgatan 56",
  "city": "Malmö",
  "postalCode": "789 01",
  "country": "Sverige"
}

```

---

### Create Customer
- **HTTP Method:** `POST`
- **Route:** `/api/customers`
- **Body:**
  - **CustomerForCreationDto**: The object containing customer details to create.
- **Responses:**
  - **201 Created**: If the customer was successfully created.
  - **409 Conflict**: If a customer with the same email already exists.
  - **500 Internal Server Error**: If there is a server error.

#### Example Request:

POST /api/customers

Content-Type: application/json


```
{
"firstName": "Anna",
"lastName": "Eriksson",
"email": "anna.eriksson@example.com",
"mobile": "46701234567",
"street": "Storgatan 12",
"city": "Stockholm",
"postalCode": "123 45",
"country": "Sverige"
}
```

#### Example Response:
```
{
"id": "67e2a1236e471b5a19775b99",
"firstName": "Anna",
"lastName": "Eriksson",
"email": "anna.eriksson@example.com",
"mobile": "46701234567",
"street": "Storgatan 12",
"city": "Stockholm",
"postalCode": "123 45",
"country": "Sverige"
}
```
---

### Delete Customer by ID
- **HTTP Method:** `DELETE`
- **Route:** `/api/customers/{id}`
- **Path Parameters:**
  - `id` (string): The unique ID of the customer to delete.
- **Responses:**
  - **204 No Content**: If the customer was successfully deleted.
  - **400 Bad Request**: If the ID format is invalid.
  - **404 Not Found**: If the customer is not found.
  - **500 Internal Server Error**: If there is a server error.

#### Example Request:

DELETE /api/customers/605c72ef153207001f7a5f1

---

### Update Customer by ID
- **HTTP Method:** `PUT`
- **Route:** `/api/customers/{id}`
- **Path Parameters:**
  - `id` (string): The unique ID of the customer to update.
- **Body:**
  - **CustomerForUpdateDto**: The object containing updated customer details.
- **Responses:**
  - **200 OK**: If the customer was successfully updated.
  - **400 Bad Request**: If the ID format is invalid or the updated data is incorrect.
  - **404 Not Found**: If the customer is not found.
  - **500 Internal Server Error**: If there is a server error.

#### Example Request:

PUT /api/customers/67e2a1236e471b5a19775b99

Content-Type: application/json


```
{
"firstName": "Anna",
"lastName": "Eriksson",
"email": "anna.eriksson@example.com",
"mobile": "46701234567",
"street": "Storgatan 12",
"city": "Stockholm",
"postalCode": "123 45",
"country": "Sverige"
}
```

#### Example Response:
```
{
"firstName": "Anna",
"lastName": "Eriksson",
"email": "anna.eriksson@example.com",
"mobile": "46701234567",
"street": "Storgatan 12",
"city": "Stockholm",
"postalCode": "123 45",
"country": "Sverige"
}
```
---

## Order Endpoints

### Get All Orders
- **HTTP Method:** `GET`
- **Route:** `/api/orders`
- **Responses:**
  - **200 OK**: Returns a list of orders. If no orders are found, returns an empty list..
  - **500 Internal Server Error**: If there is a server error.

### Get Order by ID
- **HTTP Method:** `GET`
- **Route:** `/api/orders/{id}`
- **Path Parameters:**
  - `id` (string): The unique ID of the order.
- **Responses:**
  - **200 OK**: Returns the order details in the form of `OrderDto`.
  - **404 Not Found**: If the order is not found.
  - **500 Internal Server Error**: If there is a server error.

#### Example Request:

GET /api/orders/67e3fb76c413f2a898382a73

#### Example Response:

```
{
  "id": "67e3fb76c413f2a898382a73",
  "customerId": "67e2b9159a2c1e8c0052693a",
  "items": [
    {
      "productId": "67e2cce1784ff72328012892",
      "quantity": 1,
      "price": 8499
    },
    {
      "productId": "67e2cce1784ff72328012892",
      "quantity": 2,
      "price": 8499
    }
  ],
  "orderDate": "2025-03-26T13:04:19.676Z"
}
```
---

### Create Order
- **HTTP Method:** `POST`
- **Route:** `/api/orders`
- **Body:**
  - **OrderForCreationDto**: The object containing order details.
- **Responses:**
  - **201 Created**: If the order was successfully created.
  - **500 Internal Server Error**: If there is a server error.

#### Example Request:

POST /api/orders

Content-Type: application/json

```
{
    "customerId": "67e2aa66092c57ebff2e026c",
    "items": [
      {
        "productId": "67e2cce1784ff72328012892",
        "quantity": 1,
        "price": 8499
      }
    ],
    "orderDate": "2025-03-25T18:56:15.84Z"
}
```

#### Example Response:

```
{
    "id": "67e2fc5983375ce5b457e155",
    "customerId": "67e2aa66092c57ebff2e026c",
    "items": [
      {
        "productId": "67e2cce1784ff72328012892",
        "quantity": 1,
        "price": 8499
      }
    ],
    "orderDate": "2025-03-25T18:56:15.84Z"
  }
```

---

## Product Endpoints

### Get All Products
- **HTTP Method:** `GET`
- **Route:** `/api/products`
- **Query Parameters:**  
  - `name` (optional): A string to filter by product name.
  - `searchQuery` (optional): A string to search by product name or description.
  - `productNumber` (optional): To filter by product number.
- **Responses:**
  - **200 OK**: Returns a list of products in the form of `ProductDto`.
  - **404 Not Found**: If no products are found.
  - **500 Internal Server Error**: If there is a server error.

#### Example Request:

GET /api/customers?searchQuery=sup


#### Example Response:

```
[
  {
    "id": "67e2cf59b64dd8e366890b6b",
    "productNumber": "SUP003",
    "name": "Breeze Glide 11’0”",
    "description": "En mångsidig och lätt SUP som är perfekt för både floder, sjöar och kustnära paddling. Kommer med en praktisk bärväska och paddel för att göra ditt äventyr ännu enklare.",
    "price": 4799,
    "category": "SUP",
    "isDiscontinued": false
  },
  {
    "id": "67e2cf28b64dd8e366890b6a",
    "productNumber": "SUP002",
    "name": "Glacier Explorer 12’0”",
    "description": "En högpresterande SUP för långdistanspaddling och äventyr på öppet vatten. Den styva designen och stora volymen gör det lätt att stå stabilt även under utmanande förhållanden.",
    "price": 7199,
    "category": "SUP",
    "isDiscontinued": false
  },
  {
    "id": "67e2ceecb64dd8e366890b69",
    "productNumber": "SUP001",
    "name": "Wave Rider 10’6”",
    "description": "Den perfekta suven för både nybörjare och erfarna paddlare. Med en längd på 10’6” och en stabil konstruktion ger denna SUP en jämn och säker upplevelse, oavsett om du vill paddla på lugnt eller något rörigare vatten.",
    "price": 5299,
    "category": "SUP",
    "isDiscontinued": false
  }
]

```
---

### Get Product by ID
- **HTTP Method:** `GET`
- **Route:** `/api/products/{id}`
- **Path Parameters:**
  - `id` (string): The unique ID of the product.
- **Responses:**
  - **200 OK**: Returns the product details in the form of `ProductDto`.
  - **404 Not Found**: If the product is not found.
  - **500 Internal Server Error**: If there is a server error.

#### Example Request:

GET /api/products/67e2c66810089f600c922c99

#### Example Response:
```
{
  "id": "67e2c66810089f600c922c99",
  "productNumber": "KAY001",
  "name": "Ocean Explorer 2000",
  "description": "Den perfekta havskajaken för äventyrslystna! Designad för stabilitet och hastighet på både lugnt och lite mer utmanande vatten. Med en ergonomisk sits och vattentåliga förvaringsutrymmen är den både bekväm och praktisk för längre turer.",
  "price": 12499,
  "category": "Havskajaker",
  "isDiscontinued": false
}
```

---

### Create Product
- **HTTP Method:** `POST`
- **Route:** `/api/products`
- **Body:**
  - **ProductForCreationDto**: The object containing product details to create.
- **Responses:**
  - **201 Created**: If the product was successfully created.
  - **409 Conflict**: If a product with the same product number already exists.
  - **500 Internal Server Error**: If there is a server error.

#### Example Request:

POST /api/products

Content-Type: application/json


```
{
  "productNumber": "KAY001",
  "name": "Ocean Explorer 2000",
  "description": "Den perfekta havskajaken för äventyrslystna! Designad för stabilitet och hastighet på både lugnt och lite mer utmanande vatten. Med en ergonomisk sits och vattentåliga förvaringsutrymmen är den både bekväm och praktisk för längre turer.",
  "price": 12499,
  "category": "Havskajaker",
  "isDiscontinued": false
}
```

#### Example Response:
```
{
  "id": "67e2c66810089f600c922c99",
  "productNumber": "KAY001",
  "name": "Ocean Explorer 2000",
  "description": "Den perfekta havskajaken för äventyrslystna! Designad för stabilitet och hastighet på både lugnt och lite mer utmanande vatten. Med en ergonomisk sits och vattentåliga förvaringsutrymmen är den både bekväm och praktisk för längre turer.",
  "price": 12499,
  "category": "Havskajaker",
  "isDiscontinued": false
}
```
---

### Delete Product by ID
- **HTTP Method:** `DELETE`
- **Route:** `/api/products/{id}`
- **Path Parameters:**
  - `id` (string): The unique ID of the product to delete.
- **Responses:**
  - **204 No Content**: If the product was successfully deleted.
  - **400 Bad Request**: If the ID format is invalid.
  - **404 Not Found**: If the product is not found.
  - **500 Internal Server Error**: If there is a server error.

#### Example Request:

DELETE /api/products/67e2c66810089f600c922c99

---

### Update Product by ID
- **HTTP Method:** `PUT`
- **Route:** `/api/products/{id}`
- **Path Parameters:**
  - `id` (string): The unique ID of the product to update.
- **Body:**
  - **ProductForUpdateDto**: The object containing updated product details.
- **Responses:**
  - **200 OK**: If the product was successfully updated.
  - **400 Bad Request**: If the ID format is invalid or the updated data is incorrect.
  - **404 Not Found**: If the product is not found.
  - **409 Conflict**: If a product with the same product number already exists
  - **500 Internal Server Error**: If there is a server error.

#### Example Request:

PUT /api/products/67e2c66810089f600c922c99

Content-Type: application/json


```
{
  "productNumber": "KAY001",
  "name": "Ocean Explorer 2000",
  "description": "Den perfekta havskajaken för äventyrslystna! Designad för stabilitet och hastighet på både lugnt och lite mer utmanande vatten. Med en ergonomisk sits och vattentåliga förvaringsutrymmen är den både bekväm och praktisk för längre turer.",
  "price": 12499,
  "category": "Havskajaker",
  "isDiscontinued": false
}
```

#### Example Response:
```
{
  "productNumber": "KAY001",
  "name": "Ocean Explorer 2000",
  "description": "Den perfekta havskajaken för äventyrslystna! Designad för stabilitet och hastighet på både lugnt och lite mer utmanande vatten. Med en ergonomisk sits och vattentåliga förvaringsutrymmen är den både bekväm och praktisk för längre turer.",
  "price": 12499,
  "category": "Havskajaker",
  "isDiscontinued": false
}
```
---
