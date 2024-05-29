namespace NotifierApi.Domain.Tests
{
    internal sealed class ChannelTests
    {
        const string NOT_JSON = "d";

        [Test, Order(1)]
        public void Create_Channel()
        {
            // Act
            var channel = Utils.GetChannelByFaker();

            // Assert
            Assert.That(channel.Name, Is.Not.EqualTo(default));
            Assert.That(channel.Data, Is.Not.EqualTo(default));
            Assert.That(channel.Transport, Is.Not.EqualTo(Transport.Unspecified));            
            Assert.That(channel.CreationTime, Is.Not.EqualTo(default));
            Assert.That(channel.CreationTime, Is.EqualTo(channel.ModificationTime));
        }

        [TestCase(null, "{}")]
        [TestCase("", "{}")]
        [TestCase("c", null)]
        [TestCase("c", "")]
        [TestCase("c", NOT_JSON)]
        public void Create_Channel_ThrowInvalidParameterException(string name, string data)
        {
            // Act
            TestDelegate actual = () => Utils.GetChannelByFaker(name, data);

            // Assert
            Assert.Throws<InvalidParameterException>(actual);
        }

        [TestCase(151)]
        public void Create_Channel_NameMaxLenght_ThrowInvalidParameterException(int maxLenght)
        {
            // Arrange
            var faker = new Faker();
            var name = maxLenght.RandomString();

            // Act
            TestDelegate actual = () => Utils.GetChannelByFaker(name);

            // Assert
            Assert.Throws<InvalidParameterException>(actual);
        }

        [TestCase(" c ")]
        public void Create_Channel_TrimingSpacesInName(string name)
        {
            // Arrange
            var expected = "c";

            // Act
            var channel = Utils.GetChannelByFaker(name);
            var actual = channel.Name;

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("c", "{}")]
        public void Update_Channel(string name, string data)
        {
            // Arrange
            var transport = Transport.Telegram;

            var channel = Utils.GetChannelByFaker();
            var prevModTime = channel.ModificationTime;

            // Act
            channel.Update(name, data, transport);

            // Assert
            Assert.That(channel.Name, Is.EqualTo(name));
            Assert.That(channel.Data, Is.EqualTo(data));
            Assert.That(channel.Transport, Is.EqualTo(transport));
            Assert.That(channel.ModificationTime > prevModTime);
        }

        [TestCase(null, "{}")]
        [TestCase("", "{}")]
        [TestCase("c", null)]
        [TestCase("c", "")]
        [TestCase("c", NOT_JSON)]
        public void Update_Channel_ThrowInvalidParameterException(string name, string data)
        {
            // Arrange
            var channel = Utils.GetChannelByFaker();

            // Act
            TestDelegate actual = () => channel.Update(name, data, Transport.Telegram);

            // Assert
            Assert.Throws<InvalidParameterException>(actual);
        }

        [TestCase(151)]
        public void Update_Channel_NameMaxLenght_ThrowInvalidParameterException(int maxLenght)
        {
            // Arrange
            var name = maxLenght.RandomString();
            var channel = Utils.GetChannelByFaker();

            // Act
            TestDelegate actual = () => channel.Update(name, "{}", Transport.Telegram);

            // Assert
            Assert.Throws<InvalidParameterException>(actual);
        }

        [TestCase(" c ", " {} ")]
        public void Update_Channel_TrimingSpacesInName(string name, string data)
        {
            // Arrange
            var expectedName = "c";
            var expectedData = "{}";
            var channel = Utils.GetChannelByFaker();

            // Act            
            channel.Update(name, data, Transport.Push);
            var actualName = channel.Name;
            var actualData = channel.Data;

            // Assert
            Assert.That(actualName, Is.EqualTo(expectedName));
            Assert.That(actualData, Is.EqualTo(expectedData));
        }

        [Test]
        public void Delete_Channel()
        {
            // Arrange
            var channel = Utils.GetChannelByFaker();
            var prevModTime = channel.ModificationTime;

            // Act
            channel.Delete();

            // Assert
            Assert.That(channel.Status, Is.EqualTo(Status.Deleted));
            Assert.That(channel.ModificationTime > prevModTime);
        }

        [Test]
        public void Delete_Channel_ThrowBusinessRuleException()
        {
            // Arrange
            var channel = Utils.GetChannelByFaker();

            // Act
            TestDelegate actual = () =>
            {
                channel.Delete();
                channel.Delete();
            };

            // Assert
            Assert.Throws<BusinessRuleException>(actual);
        }

        [Test]
        public void Enable_Channel()
        {
            // Arrange
            var channel = Utils.GetChannelByFaker();
            var prevModTime = channel.ModificationTime;

            // Act
            //channel.Enable();

            // Assert
            Assert.That(channel.Status, Is.EqualTo(Status.Enabled));
            Assert.That(channel.ModificationTime > prevModTime);
        }

        [Test]
        public void Enable_Channel_ThrowBusinessRuleException()
        {
            // Arrange
            var app = Utils.GetChannelByFaker();

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
        public void Disable_Channel()
        {
            // Arrange
            var channel = Utils.GetChannelByFaker();
            //channel.Enable();
            var prevModTime = channel.ModificationTime;

            // Act
            //channel.Disable();

            // Assert
            Assert.That(channel.Status, Is.EqualTo(Status.Enabled));
            Assert.That(channel.ModificationTime > prevModTime);
        }

        [Test]
        public void Disable_Channel_ThrowBusinessRuleException()
        {
            // Arrange
            var channel = Utils.GetChannelByFaker();

            // Act
            //TestDelegate actual = () => channel.Disable();

            // Assert
            //Assert.Throws<BusinessRuleException>(actual);
        }
    }
}
