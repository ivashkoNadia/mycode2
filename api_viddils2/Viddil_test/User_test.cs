using api_viddils2.Controllers;
using api_viddils2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Net.Http.Json;
using System.Text.Json;
using System.Net;
using Moq;
using System.Data.SQLite;
using Viddil_test;
using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine.Adapter;

namespace Viddil_Test
{

    public class Log_out
    {
        // Arrange
        private static readonly DbContextOptions<Viddil_context> _options = new DbContextOptionsBuilder<Viddil_context>()
                .UseInMemoryDatabase(databaseName: "Viddils_test")
                .Options;
        private static readonly Viddil_context _context = new Viddil_context(_options);
        public static readonly UserController _controller = new UserController(_context);



        [Fact]
        public async Task LogOut_WhenUserNotLoggedIn()
        {
            Function.Del_user();
            var result = await _controller.Log_outUsers();

            var objectResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var message = Assert.IsType<string>(objectResult.Value.GetType().GetProperty("message").GetValue
                (objectResult.Value));
            Assert.Equal("you can not log out because you did not log in", message);
        }

        [Fact]
        public async Task LogOut_WhenUserLoggedIn()
        {
            User_items user_admin = new User_items(2, "Ira", "fox", "1234qwer", "fo@gmail.com", "Admin");
            Function.Set_user(user_admin);
            var result = await _controller.Log_outUsers();

            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var message = Assert.IsType<string>(objectResult.Value.GetType().GetProperty("message"
                ).GetValue(objectResult.Value));
            Assert.Equal("log out was successfully", message);
        }
    }

    public class Log_in
    {
        // Arrange
        private static readonly DbContextOptions<Viddil_context> _options = new DbContextOptionsBuilder<Viddil_context>()
                .UseInMemoryDatabase(databaseName: "Viddils_test")
                .Options;
        private static readonly Viddil_context _context = new Viddil_context(_options);
        public static readonly UserController _controller = new UserController(_context);


        [Fact]
        public async Task LogIn_userNotExist()
        {
            User_registr user = new User_registr("2334cfg", "gygyg@gmail.com");
            var result = await _controller.Log_inUsers(user);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task LogIn_userExist()
        {
            User_registr user = new User_registr("1234qwer", "m@gmail.com");

            //var jsonRequestBody = JsonConvert.SerializeObject(requestBody);


            var result = await _controller.Log_inUsers(user);

            var objectResult = result.Result as ObjectResult;

            Assert.Equal(200, objectResult.StatusCode);
            Assert.Equal("log in was successfully", objectResult.Value);

            //var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            //var message = Assert.IsType<string>(okObjectResult.Value.GetType().GetProperty("message")?.GetValue(okObjectResult.Value));
            //Assert.Equal("log in was successfully", message);

            //Assert.Equal(Function.return_user().email, "oleg@gmail.com");
            //Assert.Equal(Function.return_user().password, "12qwerqwer");
        }
    }




        public class Registration
        {
            private readonly DbContextOptions<Viddil_context> _options;
            private readonly Viddil_context _context;
            private readonly UserController _controller;

            public Registration()
            {
                // Arrange
                _options = new DbContextOptionsBuilder<Viddil_context>()
                    .UseInMemoryDatabase(databaseName: "Viddils_test")
                    .Options;
                _context = new Viddil_context(_options);
                _controller = new UserController(_context);
            }


            [Fact]
            public async Task Successful_registration()
            {
                //var options = new DbContextOptionsBuilder<Viddil_context>()
                //    .UseInMemoryDatabase(databaseName: "test_database")
                //    .Options;
                //var mockContext = new Mock<Viddil_context>(options);
                //var controller = new UserController(mockContext.Object);

                User_items user_admin = new User_items(2, "Ira", "fox", "1234qwer", "fo@gmail.com", "Admin");
                var result = await _controller.Registr_inUsers(user_admin);
                Assert.IsType<OkObjectResult>(result.Result);
            }
        }


    }




