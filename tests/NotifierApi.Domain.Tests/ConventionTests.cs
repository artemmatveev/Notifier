namespace NotifierApi.Domain.Tests
{
    internal sealed class ConventionTests
    {
        [Test, Order(1)]
        public void Create_Convention()
        {
            // Act
            var conv = Utils.GetConventionByFaker();

            // Assert
            Assert.That(conv.NotificationId, Is.Not.EqualTo(default));
            Assert.That(conv.ResourceId, Is.Not.Not.EqualTo(default));
            Assert.That(conv.Enabled, Is.True);
            Assert.That(conv.CreationTime, Is.Not.EqualTo(default));
            Assert.That(conv.CreationTime, Is.EqualTo(conv.ModificationTime));
        }

        [Test]
        public void Enable_Convention()
        {
            // Arrange
            var conv = Utils.GetConventionByFaker();
            conv.Disable();
            var prevModTime = conv.ModificationTime;

            // Act
            conv.Enable();

            // Assert
            Assert.That(conv.Enabled, Is.True);
            Assert.That(conv.ModificationTime > prevModTime);
        }

        [Test]
        public void Enable_Convention_ThrowBusinessRuleException()
        {
            // Arrange
            var conv = Utils.GetConventionByFaker();

            // Act
            TestDelegate actual = () =>
            {
                conv.Enable();
            };

            // Assert
            Assert.Throws<BusinessRuleException>(actual);
        }

        [Test]
        public void Disable_Convention()
        {
            // Arrange
            var conv = Utils.GetConventionByFaker();
            var prevModTime = conv.ModificationTime;

            // Act
            conv.Disable();

            // Assert
            Assert.That(conv.Enabled, Is.False);
            Assert.That(conv.ModificationTime > prevModTime);
        }

        [Test]
        public void Disable_Channel_ThrowBusinessRuleException()
        {
            // Arrange
            var conv = Utils.GetConventionByFaker();

            // Act
            TestDelegate actual = () =>
            {
                conv.Disable();
                conv.Disable();
            };

            // Assert
            Assert.Throws<BusinessRuleException>(actual);
        }
    }
}
