# Restaurant Menu Dishes
 Web REST API porject to make restaurant menu containing dishes of different kinds.

## Built With

- MongoDb 
- ASP.NET Core 5.0
- Visual Studio 2019 Community
- MongoDb Compass Community

## Getting Started

1. clone this repo
```
https://github.com/marwanamr3/MenuTask.git
```
2. Install MongoDb locally using this link:
https://www.mongodb.com/download-center/community

3. Install NuGet Packages:

- MongoDB.Driver
- MongoDB.Bson

4. Run the solution.

## MongoDB Configuration
**Connection String:** 
"mongodb://localhost:27017" 
**Database:** 
MenuDb

**Collections:**
1) dishes
```json
{
"_id":"5fbbe34d6901906d1069d2eb",
"name":"Beef Burger",
"description":"Grilled Beef with bell peppers",
"price":50,
"category_id":"5fbae955ea55aa66287ba4f5",
"availability_time":
["lunch","dinner","weekends","weekdays","breakfast"],
"available":false,
"preparation_time":45,
"macros":"530",
"created_at":"2020-11-10T22:00:00.000Z",
"updated_at":"2020-11-10T22:00:00.000Z"
}
```
2) categories
```json
{
"_id":"5fbae936ea55aa66287ba4f3",
"name":"desserts"
}
```
3) availabilities
```json
{
"_id":"5fbaf688ea55aa66287ba504",
"name":"breakfast"
}
```

## Dishes REST APIs:

- **GET** `/api/dish`&nbsp;&nbsp;&nbsp;&nbsp;Get all dishes.
- **GET** `/api/dish/{id}`&nbsp;&nbsp;&nbsp;&nbsp;Get dish by id.
- **POST** `/api/dish`&nbsp;&nbsp;&nbsp;&nbsp;Add a new dish.
- **PUT** `/api/dish/{id}`&nbsp;&nbsp;&nbsp;&nbsp;Edit a dish.
- **DELETE** `/api/dish/{id}`&nbsp;&nbsp;&nbsp;&nbsp;Delete a dish.
- **GET** `/api/dish/available`&nbsp;&nbsp;&nbsp;&nbsp;Get only available dishes.
- **GET** `/api/dish/category`&nbsp;&nbsp;&nbsp;&nbsp;Get all dishes in a specific category.
- **GET** `/api/dish/stringfield`&nbsp;&nbsp;&nbsp;&nbsp;Get dish by field of type `string`.
- **GET** `/api/dish/period`&nbsp;&nbsp;&nbsp;&nbsp;Get dish by a specific availability time.

## Developed with :heart: By

- Marwan Amr

