# API Specification - EStoreAPI

Den här specifikationen beskriver API:ets olika endpoints för `Customers`, `Orders`, och `Products` som finns i EStoreAPI.

---

Namn: EstoreAPI

Version: 1

Beskrivning: Ett ASP.NET Core API 

Kontaktinformation: sofia.k.nordstrom@gmail.com

## Endpoints & HTTP-metoder

### Customer Endpoints 

#### 1. **Get All Customers**

- **Metod:** `GET`
- **URL:** `/api/customers`
- **Beskrivning:** Hämtar alla kunder.
- **Query Parameters:**  
    - `searchQuery` (valfri): Textsträng för att filtrera kunder på email.
- **Svar:**
    - **200 OK:** Lista med kunder i form av `CustomerDto`.
    - **404 Not Found:** Om inga kunder hittas.
    - **500 Internal Server Error:** Vid ett serverfel.

#### 2. **Get Customer by ID**

- **Metod:** `GET`
- **URL:** `/api/customers/{id}`
- **Beskrivning:** Hämtar en specifik kund baserat på kundens ID.
- **Path Parameters:**  
    - `id`: Kundens unika ID.
- **Svar:**
    - **200 OK:** Kund i form av `CustomerDto`.
    - **404 Not Found:** Om kunden inte hittas.
    - **500 Internal Server Error:** Vid ett serverfel.

#### 3. **Create Customer**

- **Metod:** `POST`
- **URL:** `/api/customers`
- **Beskrivning:** Skapar en ny kund.
- **Body:**
    - **CustomerForCreationDto:** Objekt som innehåller kundens namn, email och andra nödvändiga uppgifter.
```
[
  {
    "id": "string",
    "firstName": "string",
    "lastName": "string",
    "email": "string",
    "mobile": "string",
    "street": "string",
    "city": "string",
    "postalCode": "string",
    "country": "string"
  }
```
- **Svar:**
    - **201 Created:** Om kunden skapades framgångsrikt.
    - **409 Conflict:** Om en kund med samma email redan existerar.
    - **500 Internal Server Error:** Vid ett serverfel.

#### 4. **Delete Customer by ID**

- **Metod:** `DELETE`
- **URL:** `/api/customers/{id}`
- **Beskrivning:** Tar bort en kund baserat på kundens ID.
- **Path Parameters:**  
    - `id`: Kundens unika ID.
- **Svar:**
    - **204 No Content:** Om kunden raderas framgångsrikt.
    - **400 Bad Request:** Om ID-formatet är ogiltigt.
    - **404 Not Found:** Om kunden inte hittas.
    - **500 Internal Server Error:** Vid ett serverfel.

#### 5. **Update Customer by ID**

- **Metod:** `PUT`
- **URL:** `/api/customers/{id}`
- **Beskrivning:** Uppdaterar en kund baserat på kundens ID.
- **Path Parameters:**  
    - `id`: Kundens unika ID.
- **Body:**  
    - **CustomerForUpdateDto:** Objekt som innehåller den uppdaterade kundinformationen.
    ``` 
    {
      "firstName": "string",
      "lastName": "string",
      "email": "string",
      "mobile": "string",
      "street": "string",
      "city": "string",
      "postalCode": "string",
      "country": "string"
    }
    ```
- **Svar:**
    - **200 OK:** Om kunden uppdaterades framgångsrikt.
    - **400 Bad Request:** Om ID-formatet är ogiltigt eller om de uppdaterade data är felaktiga.
    - **404 Not Found:** Om kunden inte hittas.
    - **500 Internal Server Error:** Vid ett serverfel.

---

### Order Endpoints

#### 1. **Get Order by ID**

- **Metod:** `GET`
- **URL:** `/api/orders/{id}`
- **Beskrivning:** Hämtar en specifik order baserat på order-ID.
- **Path Parameters:**  
    - `id`: Orderns unika ID.
- **Svar:**
    - **200 OK:** Order i form av `OrderDto`.
    - **404 Not Found:** Om ordern inte hittas.
    - **500 Internal Server Error:** Vid ett serverfel.

#### 2. **Create Order**

- **Metod:** `POST`
- **URL:** `/api/orders`
- **Beskrivning:** Skapar en ny order.
- **Body:**
    - **OrderForCreationDto:** Objekt som innehåller orderinformation.
    ```
    {
      "customerId": "string",
      "items": [
        {
          "productId": "string",
          "quantity": 0,
          "price": 0
        }
      ]
    }
    ```
- **Svar:**
    - **201 Created:** Om ordern skapades framgångsrikt.
    - **500 Internal Server Error:** Vid ett serverfel.

---

### Product Endpoints

#### 1. **Get All Products**

- **Metod:** `GET`
- **URL:** `/api/products`
- **Beskrivning:** Hämtar alla produkter.
- **Query Parameters:**  
    - `name` (valfri): Textsträng för att filtrera på produktens namn.
    - `searchQuery` (valfri): Textsträng för att söka på namn eller beskrivning.
    - `productNumber` (valfri): För att filtrera på produktens nummer.
- **Svar:**
    - **200 OK:** Lista med produkter i form av `ProductDto`.
    - **404 Not Found:** Om inga produkter hittas.
    - **500 Internal Server Error:** Vid ett serverfel.

#### 2. **Get Product by ID**

- **Metod:** `GET`
- **URL:** `/api/products/{id}`
- **Beskrivning:** Hämtar en specifik produkt baserat på produktens ID.
- **Path Parameters:**  
    - `id`: Produktens unika ID.
- **Svar:**
    - **200 OK:** Produkt i form av `ProductDto`.
    - **404 Not Found:** Om produkten inte hittas.
    - **500 Internal Server Error:** Vid ett serverfel.

#### 3. **Create Product**

- **Metod:** `POST`
- **URL:** `/api/products`
- **Beskrivning:** Skapar en ny produkt.
- **Body:**
    - **ProductForCreationDto:** Objekt som innehåller produktinformation.
    ```
    {
      "productNumber": "string",
      "name": "string",
      "description": "string",
      "price": 0,
      "category": "string",
      "isDiscontinued": true
    }
    ```
- **Svar:**
    - **201 Created:** Om produkten skapades framgångsrikt.
    - **409 Conflict:** Om produkten med samma produktnummer redan existerar.
    - **500 Internal Server Error:** Vid ett serverfel.

#### 4. **Delete Product by ID**

- **Metod:** `DELETE`
- **URL:** `/api/products/{id}`
- **Beskrivning:** Tar bort en produkt baserat på produktens ID.
- **Path Parameters:**  
    - `id`: Produktens unika ID.
- **Svar:**
    - **204 No Content:** Om produkten raderas framgångsrikt.
    - **400 Bad Request:** Om ID-formatet är ogiltigt.
    - **404 Not Found:** Om produkten inte hittas.
    - **500 Internal Server Error:** Vid ett serverfel.

#### 5. **Update Product by ID**

- **Metod:** `PUT`
- **URL:** `/api/products/{id}`
- **Beskrivning:** Uppdaterar en produkt baserat på produktens ID.
- **Path Parameters:**  
    - `id`: Produktens unika ID.
- **Body:**  
    - **ProductForUpdateDto:** Objekt som innehåller den uppdaterade produktinformationen.
    ```
    {
      "productNumber": "string",
      "name": "string",
      "description": "string",
      "price": 0,
      "category": "string",
      "isDiscontinued": true
    }
    ```
- **Svar:**
    - **200 OK:** Om produkten uppdaterades framgångsrikt.
    - **400 Bad Request:** Om ID-formatet är ogiltigt eller om de uppdaterade data är felaktiga.
    - **404 Not Found:** Om produkten inte hittas.
    - **409 Conflict:** Om en produkt med samma produktnummer redan finns.
    - **500 Internal Server Error:** Vid ett serverfel.
