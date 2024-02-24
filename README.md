# BloggingPlatform

Blogging Platform

Overview:
•	I have developed this API Blogging Platform using ASP.Net Core API, .Net6 and Database is MSSQL.
•	For Authentication and Authorization JWT Bearer tokens mechanism implemented.
•	I have used Postman for testing the API Endpoints.
•	Here, I have created three Entities called User, Post and Comment.
1.	User entity is for maintaining the user details like Username and Password.
2.	Post entity is for maintaining the Blog post details like Title, Content, UserId for which user created the plog and DateCreated for when the post is created
3.	Comment entity is for maintaining the Comments of the blog post. It has property called PostId to maintain the comment which belongs to which post.
Repository: GitHub

Sample request and response:
Below details for the test request and response of the API project using Postman.
API Endpoint: https://localhost:7031/api/user/createuser
Request:
{
    "username": "TestUser",
    "password": "TestUser"
}
Response:
New user successfully created
Screenshot:
 
Endpoint: https://localhost:7031/api/user/login
Request:
{
    "username": "TestUser",
    "password": "TestUser"
}

Response:
{
    "id": 7,
    "username": "TestUser",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjciLCJuYmYiOjE3MDg3NTc0ODAsImV4cCI6MTcwODc1Nzc4MCwiaWF0IjoxNzA4NzU3NDgwfQ.OuM8DhUZRYxpBokJ3n2CLLAttH9tnBdMK4ah8QJZQ8M"
}

Screenshot:
 

Endpoint: https://localhost:7031/api/user/getuserdetails/7
To get the user details, I have tried different positive and negative checks below.
1.	After creating the bearer token, if the bearer token is not valid below response we will get.
 

2.	After creating the bearer token, checking the api with the valid token we will get below response.
 

3.	After valid token, if we are not authorized to get some other User’s details we will get below response.
 


Endpoint: https://localhost:7031/api/post/createpost
Request:
{
    "title": "Sample Post",
    "content": "Sample post content for testing the blog post. Sample post content for testing the blog post. Sample post content for testing the blog post.Sample post content for testing the blog post.Sample post content for testing the blog post.Sample post content for testing the blog post.Sample post content for testing the blog post.Sample post content for testing the blog post.Sample post content for testing the blog post.Sample post content for testing the blog post.Sample post content for testing the blog post.Sample post content for testing the blog post. Sample post content for testing the blog post.Sample post content for testing the blog post.Sample post content for testing the blog post. Sample post content for testing the blog post."
}

Response:
{
    "id": 3,
    "title": "Sample Post",
    "content": "Sample post content for testing the blog post. Sample post content for testing the blog post. Sample post content for testing the blog post.Sample post content for testing the blog post.Sample post content for testing the blog post.Sample post content for testing the blog post.Sample post content for testing the blog post.Sample post content for testing the blog post.Sample post content for testing the blog post.Sample post content for testing the blog post.Sample post content for testing the blog post.Sample post content for testing the blog post. Sample post content for testing the blog post.Sample post content for testing the blog post.Sample post content for testing the blog post. Sample post content for testing the blog post.",
    "userId": 7,
    "dateCreated": "2024-02-24T07:11:39.3260055Z"
}

Screenshot:
 


Endpoint: https://localhost:7031/api/comment/addcomment
Request:
{
    "postid": 3,
    "content": "Sample comment for testing.",
    "userid": 7
}

Response:
{
    "id": 1,
    "postId": 3,
    "content": "Sample comment for testing.",
    "userId": 7,
    "dateCreated": "2024-02-24T07:19:48.5996127Z"
}
Screenshot:
 
