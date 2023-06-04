# RESTful API to access generated bills
## How to run the API
### Step 1. Request JWT Token 

In the HTTP client for testing restful API like Postman or SoapUI, input the following HTTPPOST API url, with correct JSON values,
```
http://localhost:5073/api/Account/Token

{
 "UserName": "user1",
 "Password": "pwd1"
}
```
After the API is called, if the credential in the request payload is correct, the access token will be returned in the response. 

### Step 2. Call GenerateBill API with the access token 
In the HTTP clienet, input the following HTTPGET API url, with Authorization header which has the token returned from the step 1 "Bearer <token>", the specific Bill information will be returned in the response if the parameter id has related reccord in the database.
```
http://localhost:5073/Bills/{id}

Header: Authorization, Value: Bearer <token>
```
Without the header or invalid token value, you will get 401 Unauthorized error.

## Outline of Architecture

![Architecture Diagram](https://github.com/thisistrue1/DentalCare/blob/main/ArchitectureDiagram.png)

## Third Party Libraries
- Entity Framework 6.4.4
* Newtonsoft.Json 13.0.3
- Microsoft.AspNetCore.Authentication.JwtBearer 6.0.15
* Microsoft.IdentityModel.JsonWebTokens 6.30.1
+ System.IdentityModel.Tokens.Jwt 6.30.1
- Autofac 7.0.1
* Log4net 2.0.15
+ ibex40 4.8.0.7

## Assumptions

1. Assume the tblService table has been populated properly before the background service is run to generate bills. 

2. When generating the bills, assume the payment is due after 90 days from the service date.

3. Assume the IsPaid flag in tblBill table is marked with true by another process once receiving the patient's payment.



