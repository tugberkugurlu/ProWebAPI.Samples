using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Hosting;
using Moq;
using PizzaApi.Api.Controllers;
using PizzaApi.Domain;
using Xunit;

namespace PizzaApi.Api.Tests
{

    public class OrderControllerTests
    {

        [Fact]
        public void GetAll_should_return_all_from_OrderService()
        {
            // arrange
            var orders = new Order[0];
            var mockOrderService = new Mock<IOrderService>();
            mockOrderService.Setup(x => x.GetAll())
                        .Returns(orders);
            var orderController = new OrderController(mockOrderService.Object);
            
            // act
            var result = orderController.Get();

            // assert
            Assert.Equal(orders, result);
        }

        [Fact]
        public void Get_should_return_OK_if_order_exists()
        {
            // arrange
            const int OrderId = 123;
            var order = new Order()
                            {
                                Id = OrderId
                            };

            var mockOrderService = new Mock<IOrderService>();
            mockOrderService.Setup(x => x.Exists(It.IsAny<int>()))
                        .Returns(true);

            mockOrderService.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(order);
            var orderController = new OrderController(mockOrderService.Object);
            var request = new HttpRequestMessage();
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = new HttpConfiguration();

            // act
            var result = orderController.Get(request,  OrderId);

            // assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void Get_should_return_NotFound_if_order_DoesNotExistS()
        {
            // arrange
            const int OrderId = 123;
            var mockOrderService = new Mock<IOrderService>();
            mockOrderService.Setup(x => x.Exists(It.IsAny<int>()))
                        .Returns(false);

            var orderController = new OrderController(mockOrderService.Object);
            var request = new HttpRequestMessage();
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = new HttpConfiguration();

            // act
            var result = orderController.Get(request, OrderId);

            // assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }


        [Fact]
        public void Delete_should_return_OK_if_order_exists()
        {
            // arrange
            const int OrderId = 123;
            var mockOrderService = new Mock<IOrderService>();
            mockOrderService.Setup(x => x.Exists(It.IsAny<int>()))
                        .Returns(true);

            var orderController = new OrderController(mockOrderService.Object);
            var request = new HttpRequestMessage();
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = new HttpConfiguration();

            // act
            var result = orderController.Delete(request, OrderId);

            // assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void Delete_should_return_NotFound_if_order_doesNotExist()
        {
            // arrange
            const int OrderId = 123;
            var mockOrderService = new Mock<IOrderService>();
            mockOrderService.Setup(x => x.Exists(It.IsAny<int>()))
                        .Returns(false);

            var orderController = new OrderController(mockOrderService.Object);
            var request = new HttpRequestMessage();
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = new HttpConfiguration();

            // act
            var result = orderController.Delete(request, OrderId);

            // assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
       [Fact]
        public void Put_should_return_OK_if_order_exists()
        {
            // arrange
            const int OrderId = 123;
            var order = new Order()
            {
                Id = OrderId
            };
            var mockOrderService = new Mock<IOrderService>();
            mockOrderService.Setup(x => x.Exists(It.IsAny<int>()))
                        .Returns(true);

            var orderController = new OrderController(mockOrderService.Object);
            var request = new HttpRequestMessage();
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = new HttpConfiguration();

            // act
            var result = orderController.Put(request, order);

            // assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public void Put_should_return_NotFound_if_order_doesNotExist()
        {
            // arrange
            const int OrderId = 123;
            var order = new Order()
            {
                Id = OrderId
            };
            var mockOrderService = new Mock<IOrderService>();
            mockOrderService.Setup(x => x.Exists(It.IsAny<int>()))
                        .Returns(false);

            var orderController = new OrderController(mockOrderService.Object);
            var request = new HttpRequestMessage();
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = new HttpConfiguration();

            // act
            var result = orderController.Put(request, order);

            // assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public void Post_should_return_BadRequest_if_order_Exists()
        {
            // arrange
            const int OrderId = 123;
            var order = new Order(new OrderItem[]
                                      {
                                          new OrderItem()
                                              {
                                                  Name = "Name",
                                                  Quantity = 1
                                              }
                                      })
            {
                Id = OrderId
            };
            
            var mockOrderService = new Mock<IOrderService>();
            mockOrderService.Setup(x => x.Exists(It.IsAny<int>()))
                        .Returns(true);

            var orderController = new OrderController(mockOrderService.Object);
            var request = new HttpRequestMessage();
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = new HttpConfiguration();

            // act
            var result = orderController.Post(request, order);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public void Post_should_return_BadRequest_if_order_hasNoOrderItems()
        {
            // arrange
            const int OrderId = 123;
            var order = new Order()
            {
                Id = OrderId,
            };
            var mockOrderService = new Mock<IOrderService>();
            mockOrderService.Setup(x => x.Exists(It.IsAny<int>()))
                        .Returns(false);

            var orderController = new OrderController(mockOrderService.Object);
            var request = new HttpRequestMessage();
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = new HttpConfiguration();

            // act
            var result = orderController.Post(request, order);

            // assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }


        [Fact]
        public void Post_should_return_Created_if_order_good()
        {
            // arrange
            const int OrderId = 123;
            var order = new Order(new OrderItem[]
                                      {
                                          new OrderItem()
                                              {
                                                  Name = "Name",
                                                  Quantity = 1
                                              }
                                      })
            {
                Id = OrderId
            };
            var mockOrderService = new Mock<IOrderService>();
            mockOrderService.Setup(x => x.Exists(It.IsAny<int>()))
                        .Returns(false);

            var orderController = new OrderController(mockOrderService.Object);
            var request = new HttpRequestMessage();
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = new HttpConfiguration();

            // act
            var result = orderController.Post(request, order);

            // assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }
    }
}
