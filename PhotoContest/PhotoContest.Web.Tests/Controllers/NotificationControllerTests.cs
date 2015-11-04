using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContest.Data.UnitsOfWork;
using PhotoContest.Models;
using PhotoContest.Models.Enumerations;
using PhotoContest.Web.Controllers;
using PhotoContest.Web.Models.BindingModel;
using PhotoContest.Web.Models.ViewModels;

namespace PhotoContest.Web.Tests.Controllers
{
    [TestClass]
    public class NotificationControllerTests
    {
        private Mock<IPhotoContestData> photoContestMock;
        NotificationsController notificationController;
        IEnumerable<Notification> notifications;

        [TestInitialize]
        public void Initialize()
        {
            photoContestMock = new Mock<IPhotoContestData>();
            notificationController = new NotificationsController(photoContestMock.Object);
            notifications = new List<Notification>() {
                new Notification()
                {
                    Id = 1,
                    NotificationContent = "Congratulations! You won the big prize.",
                    Type = NotificationType.Winning,
                    User = new User() {UserName = "stamat", Email = "stamat@gmail.com", Id = new Guid().ToString()}
                },
                new Notification()
                {
                    Id = 2,
                    NotificationContent = "Congratulations! You won second place.",
                    Type = NotificationType.Winning,
                    User = new User() {UserName = "pesho", Email = "pesho@gmail.com", Id = new Guid().ToString()}
                },
                new Notification()
                {
                    Id = 3,
                    NotificationContent = "You have been invited to the \"Summer Vacation\" photo contest.",
                    Type = NotificationType.InvitedToContest,
                    User = new User() {UserName = "gosho", Email = "gosho@gmail.com", Id = new Guid().ToString()}
                }
          };
        }

        [TestMethod]
        public void Assert_Action_Index_Returns_DefaultView()
        {
            //Arrange
            //photoContestMock.Setup(x => x.Notifications.All()).Returns(notifications.AsQueryable());

            //Act
            var page = 1;
            var result = (notificationController.Index(page) as ViewResult);


            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName));
        }

        [TestMethod]
        public void Get_All_Notifications_Should_Return_All_Notifications_Ordered_By_Date()
        {
            //Arrange
            photoContestMock.Setup(x => x.Notifications.All()).Returns(notifications.AsQueryable());

            //Act
            var page = 1;
            var result = (notificationController.Index(page) as ViewResult);
            var viewResult = (result.Model) as List<NotificationViewModel>;


            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName));
            Assert.AreEqual(viewResult.Count, 3);
            Assert.AreEqual("Congratulations! You won the big prize.", viewResult[0].NotificationContent);
            Assert.AreEqual("Congratulations! You won second place.", viewResult[1].NotificationContent);
            Assert.AreEqual("You have been invited to the \"Summer Vacation\" photo contest.", viewResult[2].NotificationContent);

        }

        [TestMethod]
        public void Assert_Action_CreateNotification_Returns_DefaultView()
        {
            //Arrange
            var newNotification = new NotificationBindingModel()
            {
                NotificationContent = "You have been invited for ...",
                Type = NotificationType.InvitedToContest,
                UserId = new Guid().ToString()
            };


            //Act
            var result = (notificationController.CreateNotification(newNotification) as ViewResult);


            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName));
        }

        [TestMethod]
        public void Create_Notification_Should_Increase_Number_Of_Notifications()
        {
            //Arrange
            var newNotification = new NotificationBindingModel()
            {
                NotificationContent = "You have been invited for ...",
                Type = NotificationType.InvitedToContest,
                UserId = new Guid().ToString()
            };

            //photoContestMock.Setup(n => n.Notifications.Add(newNotification));

            //Act
            var result = (notificationController.CreateNotification(newNotification) as ViewResult);
            var allNotifications = notifications;


            //Assert
            Assert.AreEqual(allNotifications.Count(), 4);
            //Assert.IsNotNull(result);
            //Assert.IsTrue(string.IsNullOrEmpty(result.ViewName));
        }
    }
}
