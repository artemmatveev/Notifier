namespace NotifierApi.Domain.Tests
{
    //https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices
    internal sealed class ApplicationTests
    {
        [Test, Order(1)]
        public void Create_Application()
        {
            // Act
            var app = Utils.GetApplicationByFaker();

            // Assert
            Assert.That(app.Name, Is.Not.EqualTo(default));
            Assert.That(app.Comment, Is.Not.EqualTo(default));            
            Assert.That(app.CreationTime, Is.Not.EqualTo(default));
            Assert.That(app.CreationTime, Is.EqualTo(app.ModificationTime));
        }

        [TestCase("")]
        [TestCase(null)]
        public void Create_Application_ThrowInvalidParameterException(string? name)
        {
            // Act
            TestDelegate actual = () => Utils.GetApplicationByFaker(name);

            // Assert
            Assert.Throws<InvalidParameterException>(actual);
        }

        [TestCase(151)]
        public void Create_Application_NameMaxLenght_ThrowInvalidParameterException(int maxLenght)
        {
            // Arrange            
            var name = maxLenght.RandomString();

            // Act
            TestDelegate actual = () => Utils.GetApplicationByFaker(name);

            // Assert
            Assert.Throws<InvalidParameterException>(actual);

        }

        [TestCase(" a ")]
        public void Create_Application_TrimingSpacesInFields(string name)
        {
            // Arrange
            var expected = "a";

            // Act
            var app = Utils.GetApplicationByFaker(name);
            var actual = app.Name;

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("a", "c")]
        [TestCase("a", "")]
        [TestCase("a", null)]
        public void Update_Application(string name, string? comment)
        {
            // Arrange
            var app = Utils.GetApplicationByFaker();
            var prevModTime = app.ModificationTime;

            // Act
            app.Update(name, comment);

            // Assert
            Assert.That(app.Name, Is.EqualTo(name));
            Assert.That(app.Comment, Is.EqualTo(comment));
            Assert.That(app.ModificationTime > prevModTime);
        }

        [TestCase("")]
        [TestCase(null)]
        public void Update_Application_ThrowInvalidParameterException(string name)
        {
            // Arrange
            var app = Utils.GetApplicationByFaker();

            // Act
            TestDelegate actual = () => app.Update(name, string.Empty);

            // Assert
            Assert.Throws<InvalidParameterException>(actual);
        }

        [TestCase(151)]
        public void Update_Application_NameMaxLenght__ThrowInvalidParameterException(int maxLenght)
        {
            // Arrange
            var name = maxLenght.RandomString();
            var app = Utils.GetApplicationByFaker();

            // Act
            TestDelegate actual = () => app.Update(name, string.Empty);

            // Assert
            Assert.Throws<InvalidParameterException>(actual);
        }

        [TestCase(" a ", " b ")]
        public void Update_Application_TrimingSpacesInFields(string name, string? comment)
        {
            // Arrange
            var expectedName = "a";
            var expectedComment = "b";
            var app = Utils.GetApplicationByFaker();

            // Act
            app.Update(name, comment);
            var actualName = app.Name;
            var actualComment = app.Comment;

            // Assert
            Assert.That(actualName, Is.EqualTo(expectedName));
            Assert.That(actualComment, Is.EqualTo(expectedComment));
        }

        [Test]
        public void Delete_Application()
        {
            // Arrange
            var app = Utils.GetApplicationByFaker();
            var prevModTime = app.ModificationTime;

            // Act
            app.Delete();

            // Assert
            Assert.That(app.Status, Is.EqualTo(Status.Deleted));
            Assert.That(app.ModificationTime > prevModTime);
        }

        [Test]
        public void Delete_Application_ThrowBusinessRuleException()
        {
            // Arrange
            var app = Utils.GetApplicationByFaker();

            // Act
            TestDelegate actual = () =>
            {
                app.Delete();
                app.Delete();
            };

            // Assert
            Assert.Throws<BusinessRuleException>(actual);
        }

        [Test]
        public void Enable_Application()
        {
            // Arrange
            var app = Utils.GetApplicationByFaker();
            var prevModTime = app.ModificationTime;

            // Act
            //app.Enable();

            // Assert
            Assert.That(app.Status, Is.EqualTo(Status.Enabled));
            Assert.That(app.ModificationTime > prevModTime);
        }

        [Test]
        public void Enable_Application_ThrowBusinessRuleException()
        {
            // Arrange
            var app = Utils.GetApplicationByFaker();

            // Act
            TestDelegate actual = () =>
            {
                //app.Enable();
                //app.Enable();
            };

            // Assert
            Assert.Throws<BusinessRuleException>(actual);
        }

        [Test]
        public void Disable_Application()
        {
            // Arrange
            var app = Utils.GetApplicationByFaker();
            //app.Enable();
            var prevModTime = app.ModificationTime;

            // Act
            //app.Disable();

            // Assert
            Assert.That(app.Status, Is.EqualTo(Status.Enabled));
            Assert.That(app.ModificationTime > prevModTime);
        }

        [Test]
        public void Disable_Application_ThrowBusinessRuleException()
        {
            // Arrange
            var app = Utils.GetApplicationByFaker();

            // Act
            //TestDelegate actual = () => app.Disable();

            // Assert
            //Assert.Throws<BusinessRuleException>(actual);
        }
    }
}
