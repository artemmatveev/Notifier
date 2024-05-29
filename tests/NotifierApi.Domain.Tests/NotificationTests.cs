namespace NotifierApi.Domain.Tests
{
    internal sealed class NotificationTests
    {
        [Test, Order(1)]
        public void Create_Notification()
        {
            // Act
            var notification = Utils.GetNotificationByFaker();

            // Assert
            Assert.That(notification.ApplicationId, Is.Not.EqualTo(default));
            Assert.That(notification.Constant, Is.Not.EqualTo(default));
            Assert.That(notification.Priority, Is.Not.EqualTo(Priority.Unspecified));
            Assert.That(notification.Name, Is.Not.EqualTo(default));
            Assert.That(notification.Comment, Is.Not.EqualTo(default));            
            Assert.That(notification.CreationTime, Is.Not.EqualTo(default));
            Assert.That(notification.CreationTime, Is.EqualTo(notification.ModificationTime));
            Assert.That(notification.NotificationChannels.Any(), Is.True);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Create_Notification_ThrowInvalidParameterException(string name)
        {
            // Act
            TestDelegate actual = () => Utils.GetNotificationByFaker(name);

            // Assert
            Assert.Throws<InvalidParameterException>(actual);
        }

        [TestCase(151)]
        public void Create_Notification_NameMaxLenght_ThrowInvalidParameterException(int maxLenght)
        {
            // Arrange            
            var name = maxLenght.RandomString();

            // Act
            TestDelegate actual = () => Utils.GetNotificationByFaker(name);

            // Assert
            Assert.Throws<InvalidParameterException>(actual);
        }

        [TestCase(" n ")]
        public void Create_Notification_TrimingSpacesInName(string name)
        {
            // Arrange
            var expected = "n";

            // Act
            var notification = Utils.GetNotificationByFaker(name);
            var actual = notification.Name;

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        //[TestCase("n", "c")]
        //[TestCase("n", "")]
        //[TestCase("n", null)]
        //public void Update_Notification(string name, string? comment)
        //{
        //    // Arrange
        //    var priority = Priority.High;

        //    var notification = Utils.GetNotificationByFaker();
        //    var prevModTime = notification.ModificationTime;

        //    // Act
        //    notification.Update(priority, name, comment);

        //    // Assert
        //    Assert.That(notification.Name, Is.EqualTo(name));
        //    Assert.That(notification.Comment, Is.EqualTo(comment));
        //    Assert.That(notification.ModificationTime > prevModTime);
        //}

        //[TestCase("")]
        //[TestCase(null)]
        //public void Update_Notification_ThrowInvalidParameterException(string name)
        //{
        //    // Arrange
        //    var notification = Utils.GetNotificationByFaker();

        //    // Act
        //    TestDelegate actual = () => notification.Update(Priority.High, name, string.Empty);

        //    // Assert
        //    Assert.Throws<InvalidParameterException>(actual);
        //}

        //[TestCase(151)]
        //public void Update_Notification_NameMaxLenght_ThrowInvalidParameterException(int maxLenght)
        //{
        //    // Arrange            
        //    var name = maxLenght.RandomString();
        //    var notification = Utils.GetNotificationByFaker();

        //    // Act
        //    TestDelegate actual = () => notification.Update(Priority.High, name, string.Empty);

        //    // Assert
        //    Assert.Throws<InvalidParameterException>(actual);
        //}

        //[TestCase(" n ", " c ")]
        //public void Update_Notification_TrimingSpacesInName(string name, string? comment)
        //{
        //    // Arrange
        //    var expectedName = "n";
        //    var expectedComment = "c";
        //    var notification = Utils.GetNotificationByFaker();

        //    // Act
        //    notification.Update(Priority.Highest, name, comment);
        //    var actualName = notification.Name;
        //    var actualComment = notification.Comment;

        //    // Assert
        //    Assert.That(actualName, Is.EqualTo(expectedName));
        //    Assert.That(actualComment, Is.EqualTo(expectedComment));
        //}

        [Test]
        public void Delete_Notification()
        {
            // Arrange
            var notification = Utils.GetNotificationByFaker();
            var prevModTime = notification.ModificationTime;

            // Act
            notification.Delete();

            // Assert
            Assert.That(notification.Status, Is.EqualTo(Status.Deleted));
            Assert.That(notification.ModificationTime > prevModTime);
        }

        [Test]
        public void Delete_Notification_ThrowBusinessRuleException()
        {
            // Arrange
            var notification = Utils.GetNotificationByFaker();

            // Act
            TestDelegate actual = () =>
            {
                notification.Delete();
                notification.Delete();
            };

            // Assert
            Assert.Throws<BusinessRuleException>(actual);
        }

        [Test]
        public void Enable_Notification()
        {
            // Arrange
            var notification = Utils.GetNotificationByFaker();
            var prevModTime = notification.ModificationTime;

            // Act
            // notification.Enable();

            // Assert
            Assert.That(notification.Status, Is.EqualTo(Status.Enabled));
            Assert.That(notification.ModificationTime > prevModTime);
        }

        [Test]
        public void Enable_Notification_ThrowBusinessRuleException()
        {
            // Arrange
            var notification = Utils.GetNotificationByFaker();

            // Act
            TestDelegate actual = () =>
            {
                //notification.Enable();
                //notification.Enable();
            };

            // Assert
            Assert.Throws<BusinessRuleException>(actual);
        }

        [Test]
        public void Disable_Notification()
        {
            // Arrange
            var notification = Utils.GetNotificationByFaker();
            //notification.Enable();
            var prevModTime = notification.ModificationTime;

            // Act
            //notification.Disable();

            // Assert
            Assert.That(notification.Status, Is.EqualTo(Status.Enabled));
            Assert.That(notification.ModificationTime > prevModTime);
        }

        [Test]
        public void Disable_Notification_ThrowBusinessRuleException()
        {
            // Arrange
            var notification = Utils.GetNotificationByFaker();

            // Act
            //TestDelegate actual = () => notification.Disable();

            // Assert
            //Assert.Throws<BusinessRuleException>(actual);
        }
    }
}
