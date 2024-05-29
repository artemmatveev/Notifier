namespace NotifierApi.Domain.Tests
{
    internal sealed class TelegramMessageTests
    {
        [Test, Order(1)]
        public void Create_TelegramMessage()
        {
            // Act
            var msg = Utils.GetTelegramMessageByFaker();

            // Assert
            Assert.That(msg.NotificationId, Is.Not.EqualTo(default));
            Assert.That(msg.ResourceId, Is.Not.EqualTo(default));
            Assert.That(msg.Body, Is.Not.EqualTo(default));
            Assert.That(msg.Priority, Is.Not.EqualTo(Priority.Unspecified));
            Assert.That(msg.ChatId, Is.Not.EqualTo(default));
            Assert.That(msg.CreationTime, Is.Not.EqualTo(default));
            Assert.That(msg.ReceiveTime, Is.Null);
            Assert.That(msg.SentTime, Is.Null);
        }

        [TestCase(0)]
        public void Create_TelegramMessage_ThrowInvalidParameterException(long? chatId)
        {
            // Act
            TestDelegate actual = () => Utils.GetTelegramMessageByFaker(chatId: chatId);

            // Assert
            Assert.Throws<InvalidParameterException>(actual);
        }

        [Test]
        public void Receive_TelegramMessage()
        {
            // Arrange
            var msg = Utils.GetTelegramMessageByFaker();

            // Act
            msg.Receive();

            // Assert
            Assert.That(msg.ReceiveTime, Is.Not.Null);
        }

        [Test]
        public void Send_TelegramMessage()
        {
            // Arrange
            var msg = Utils.GetTelegramMessageByFaker();

            // Act
            msg.Send();

            // Assert
            Assert.That(msg.SentTime, Is.Not.Null);
        }
    }
}
