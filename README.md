# PhoneSubscription
PhoneSubscription is an ASP.Net web API to create users and his phone subscriptions. 

## Usage

### Get all users
```http
GET /api/User
```
### Get specific user
```http
GET /api/User/?id=5
```
### Create user
```http
POST /api/User
```
provide user JSON as:
```
{
    "id": 9,
    "firstname": "test5",
    "lastname": "last1",
    "email": "testlast@mail.com",
    "subscriptions": [
        {
            "id": "2712e86a-d519-48af-b50b-194e9a2102dg",
            "name": "50 min deal11",
            "price": 24,
            "priceIncVatAmount  ": 30,
            "CallMinutes": 50
        }
    ],
    "totalPriceIncVatAmount":30, 
    "totalcallminutes":50  
}
```
### Update user and add/delete subscription to user
```http
PUT /api/User/?id=5
```
Also provide updated user JSON

### Delete specific user
```http
DELETE /api/User/?id=5
```
### Get all subscriptions
```http
GET /api/Subscription
```
### Get specific Subscription
```http
GET /api/Subscription/?id=2712e86a-d519-48af-b50b-194e9a2102dg
```
### Create subscription
```http
POST /api/Subscription
```
provide subscription JSON as:
```
{
    "id": "2712e86a-d519-48af-b50b-194e9a2102dg",
    "name": "50 min deal11",
    "price": 24,
    "priceIncVatAmount  ": 30,
    "CallMinutes": 50
}
```
### Update subscription data
```http
PUT /api/Subscription/?id=2712e86a-d519-48af-b50b-194e9a2102dg
```
Also provide updated subscription JSON

### Delete specific subscription
```http
DELETE /api/Subscription/?id=2712e86a-d519-48af-b50b-194e9a2102dg
```
