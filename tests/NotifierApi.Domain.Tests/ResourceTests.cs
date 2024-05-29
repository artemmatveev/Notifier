namespace NotifierApi.Domain.Tests
{
    internal sealed class ResourceTests
    {
        const string NAME = "f";
        const string EMAIL = "f@a.com";
        const string USERNAME = "username";
        const string NAME_WITH_SPACES = $" {NAME} ";
        const string EMAIL_WITH_SPACES = $" {EMAIL} ";
        const string USERNAME_WITH_SPACES = $" {USERNAME} ";
        const long CHATID = 568743128;

        [Test, Order(1)]
        public void Create_Resource()
        {
            // Act
            var resource = Utils.GetResourceByFaker();

            // Assert
            Assert.That(resource.StaffId, Is.Not.EqualTo(default));
            Assert.That(resource.Name, Is.Not.EqualTo(default));
            Assert.That(resource.Email, Is.Not.EqualTo(default));
            Assert.That(resource.Username, Is.Not.EqualTo(default));
            Assert.That(resource.ChatId, Is.Not.EqualTo(default));
            Assert.That(resource.CreationTime, Is.Not.EqualTo(default));
            Assert.That(resource.CreationTime, Is.EqualTo(resource.ModificationTime));
        }

        [TestCase(null, EMAIL, USERNAME, CHATID)]
        [TestCase("", EMAIL, USERNAME, CHATID)]
        [TestCase(NAME, "p", USERNAME, CHATID)]
        [TestCase(NAME, "", USERNAME, CHATID)]
        [TestCase(NAME, EMAIL, "", CHATID)]
        [TestCase(NAME, EMAIL, USERNAME, 0)]
        public void Create_Resource_ThrowInvalidParameterException(string? name, string? email, string? username, long? chatId)
        {
            // Act
            TestDelegate actual = () => Utils.GetResourceByFaker(name, email, username, chatId);

            // Assert
            Assert.Throws<InvalidParameterException>(actual);
        }

        [TestCase(256, 1, 1)]
        [TestCase(1, 321, 1)]
        [TestCase(1, 1, 51)]
        public void Create_Resource_FieldMaxLenght_ThrowInvalidParameterException(int nameMaxLenght, int emailMaxLenght, int usernameMaxLenght)
        {
            // Arrange            
            var name = nameMaxLenght.RandomString();
            var email = emailMaxLenght.RandomString();
            var username = usernameMaxLenght.RandomString();

            // Act
            TestDelegate actual = () => Utils.GetResourceByFaker(name, email, username, CHATID);

            // Assert
            Assert.Throws<InvalidParameterException>(actual);
        }

        [TestCase(NAME_WITH_SPACES,
                  EMAIL_WITH_SPACES,
                  USERNAME_WITH_SPACES)]
        public void Create_Resource_TrimingSpacesInFields(string? name, string? email, string? username)
        {            
            // Act
            var msg = Utils.GetResourceByFaker(
                name: name,
                email: email,
                username: username);

            var actualName = msg.Name;
            var actualEmail = msg.Email;
            var actualUserName = msg.Username;

            // Assert
            Assert.That(actualName, Is.EqualTo(NAME));
            Assert.That(actualEmail, Is.EqualTo(EMAIL));
            Assert.That(actualUserName, Is.EqualTo(USERNAME));
        }

        [TestCase(null)]
        [TestCase(CHATID)]
        public void Update_Resource(long? chatId)
        {
            // Arrange            
            var resource = Utils.GetResourceByFaker();
            var prevModTime = resource.ModificationTime;

            // Act
            resource.Update(chatId);

            // Assert
            Assert.That(resource.ChatId, Is.EqualTo(chatId));
            Assert.That(resource.ModificationTime > prevModTime);
        }

        [TestCase(0)]
        public void Update_Resource_ThrowInvalidParameterException(long? chatId)
        {
            // Arrange
            var resource = Utils.GetResourceByFaker();

            // Act
            TestDelegate actual = () => resource.Update(chatId);

            // Assert
            Assert.Throws<InvalidParameterException>(actual);
        }
    }
}
