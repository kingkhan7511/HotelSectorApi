Description:   
      This API has been developed to manage the booking from Mobile application of Hotel. 

Purpose of ReadMe.md: 

      This document has been prepared to explain the process of start and test the APIs.

How to Start the Project:

      To run the API using Visual Studio, please use the below steps: 

      1) Extract the files from downloaded zip folder
          Option A: User can clone the project from https://github.com/kingkhan7511/HotelSectorApi.git. 
          Option B: Double click on /HotelSectorApi/HotelSectorApi.sln 
      2) Once the you open the project wait for while to resolve the packages dependencies automatically or Restore the NuGet Packages using option in project solution from VS.  
      3) Make HotelSectorApi is Set as StartUp Project by right click on HotelSectorApi.  
      4) Run the project by clicking on IIS Express in VS top bar or just press F5.  
      5) After running the project on IIS Express the broweser will get open and user will be able to view the implemented Swagger UI.
      6) System will seed data for User and Rooms to view the seed records please run the below queries on database:
       select * from Users
       select * from Room 

Testing:

BaseURL: https://localhost:44311  
        Please note the current base url for reference however it can be change  base on configurations. 

How to check response of each API and below steps are common for all APIs: 

        1) First we need to open the API in Swagger. 
        2) User will be able to view the "Try it out" button on top right. 
        3) User should enter parameters in the body. 
        4) After adding the parameters user should click on execute button. 
        5) After clicking on execute button user should be able to view the Response of API in Server response section. 
        6) Those APIs which need Authorization we need to click on         Authorize button on top right of all API and enter the 
        "Bearer $token" if required. 
  
If user want to test the API using any other API testing tools like postman the below are example how to check the APIs. 

  1) LoginToValidateEmail: 

      URL: BaseURL/api/Anonymous/LoginToValidateEmail

      Input: 
        {
          "email": "email2wajid@gmail.com"
        }

      Output : 
        {
          "statusCode": 200,
          "message": "OTP has been sent to the email, please use Login api to get authorized.",
          "isSucess": true,
          "result": "Wajid",
          "error": null
        }
  2) Authenticate API will be used for two purpose: 

      URL: BaseURL/api/Anonymous/Authenticate

      Input:
      
        a) We will get new Token and Refresh token by passing
            email (Required) 
            pin (Required) 
            refreshToken (This should be "") 

        b) To Refresh the token we need to call the same API
            email (This should be "")
            pin (This should be "") 
            refreshToken (This should be pass)

      In both scenario API will return the following DTO: 
      {
        "statusCode": 200,
        "message": "The user has been logged in sucessfully.",
        "isSucess": true,
        "result": {
          "id": 1,
          "token": {
            "token": "",
            "expiryTime": "",
            "refreshToken": "",
            "refreshTokenExpiryTime": ""
          }
        },
        "error": null
      }

  3) GetAllRooms:

      URL: BaseURL/api/Room/GetAllRooms

      Input: In Filter we can pass Room No which are store in database. 
            {
              "skip": 0,
              "maxResultCount": 10,
              "filter": ""
            }

      Output: 
            {
              "statusCode": 200,
              "message": "",
              "isSucess": true,
              "result": {
                "count": 2,
                "result": [
                  {
                    "id": 1,
                    "roomNo": "RN2",
                    "roomFloor": 2,
                    "rentPerHour": 80,
                    "isAvailable": true
                  } 
                ]
              },
              "error": null
            }
  4)  AddUpdateBooking: 

  Note: Id should pass as null to add new record however to update the booking room detail we nee to pass the id which should retun in GetMyBooked API. 

  URL: BaseURL/api/RoomBooking/AddUpdateBooking

  Input:  
      {
        "id": null, 
        "roomId": 2,
        "startDate": "2022-08-20T12:08:15.512Z",
        "endDate": "2022-08-21T12:08:15.512Z"
      }
  Output: 
    Below is sucess response and it can be change if there is any error or validation error: 
      {
        "statusCode": 200,
        "message": "Room booking has been updated.",
        "isSucess": true,
        "result": [],
        "error": null
      }

  5) GetMyBooked: 

    URL: BaseURL/api/RoomBooking/GetMyBooked

    Input: 
      {
        "skip": 0,
        "maxResultCount": 10,
        "startDate": "2022-08-10T12:08:15.512Z",
        "endDate": "2022-08-15T12:59:15.512Z"
      }

    Output: Sucess
      {
        "statusCode": 200,
        "message": "",
        "isSucess": true,
        "result": {
        "count": 2,
        "result": [
          {
            "id": 1,
            "roomDetails": {
              "id": 1,
              "roomNo": "RN2",
              "roomFloor": 2,
              "rentPerHour": 80,
              "isAvailable": false
            },
            "roomId": 1,
            "startDate": "2022-08-10T11:55:21.88",
            "endDate": "2022-08-12T11:55:21.88"
          }  
          }
        ]
      },
      "error": null
    }

  6) DeleteBooking:

    URL: BaseURL/api/RoomBooking/DeleteBooking

    Input: 
         {
           "id": 3
         }
    Output: 
          {
            "statusCode": 200,
            "message": "Room Booking has been deleted sucessfully",
            "isSucess": true,
            "result": [],
            "error": null
          }

Below are the technologies stack which has been used in above excersie: 

  Language: C#

  Framework: Asp.Net Core, EntityFramework  
  
  Version: .Net 5 
  
  Design Pattern: Repository Pattern

  Database Approach: Code First

  Database: MsSQL

  API Open Documentation: Swagger
 
 
