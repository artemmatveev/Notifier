namespace NotifierApi.Domain.Tests
{
    internal sealed class TemplateTests
    {
        const string NAME = "n";
        const string SUBJECT = "s";
        const string BODY = "b";
        const string COMMENT = "c";
        const string NAME_WITH_SPACES = $" {NAME} ";
        const string SUBJECT_WITH_SPACES = $" {SUBJECT} ";
        const string BODY_WITH_SPACES = $" {BODY} ";
        const string COMMENT_WITH_SPACES = $" {COMMENT} ";

        [Test, Order(1)]
        public void Create_Template()
        {
            // Act
            var temp = Utils.GetTemplateByFaker();

            Assert.That(temp.Transport, Is.Not.EqualTo(Transport.Unspecified));
            Assert.That(temp.Lang, Is.Not.EqualTo(Lang.Unspecified));
            Assert.That(temp.Subject, Is.Not.EqualTo(default));
            Assert.That(temp.Body, Is.Not.EqualTo(default));
            Assert.That(temp.Name, Is.Not.EqualTo(default));
            Assert.That(temp.Comment, Is.Not.EqualTo(default));            
            Assert.That(temp.CreationTime, Is.Not.EqualTo(default));
            Assert.That(temp.CreationTime, Is.EqualTo(temp.ModificationTime));
        }

        [TestCase("", SUBJECT, BODY)]
        [TestCase(null, SUBJECT, BODY)]
        [TestCase(NAME, "", BODY)]
        [TestCase(NAME, null, BODY)]
        [TestCase(NAME, SUBJECT, "")]
        [TestCase(NAME, SUBJECT, null)]
        public void Create_Template_ThrowInvalidParameterException(string? name, string? subject, string? body)
        {
            // Act
            TestDelegate actual = () => Utils.GetTemplateByFaker(
                name: name,
                subject: subject,
                body: body);

            // Assert
            Assert.Throws<InvalidParameterException>(actual);
        }

        [TestCase(151, 1)]        
        [TestCase(1, 151)]
        public void Create_Template_MaxLenghtFields_ThrowInvalidParameterException(int nameMaxLenght, int subjectMaxLenght)
        {
            // Arrange            
            var name = nameMaxLenght.RandomString();
            var subject = subjectMaxLenght.RandomString();

            // Act
            TestDelegate actual = () => Utils.GetTemplateByFaker(
                name: name,
                subject: subject,
                body: BODY);

            // Assert
            Assert.Throws<InvalidParameterException>(actual);
        }

        [TestCase(NAME_WITH_SPACES,
                  SUBJECT_WITH_SPACES,
                  BODY_WITH_SPACES,
                  COMMENT_WITH_SPACES)]
        public void Create_Template_TrimingSpacesInFields(string? name, string? subject, string? body, string? comment)
        {            
            // Act
            var temp = Utils.GetTemplateByFaker(
                name: name,
                subject: subject,
                body: body,
                comment: comment);

            var actualName = temp.Name;
            var actualSubject = temp.Subject;
            var actualBody = temp.Body;
            var actualComment = temp.Comment;

            // Assert
            Assert.That(actualName, Is.EqualTo(NAME));
            Assert.That(actualSubject, Is.EqualTo(SUBJECT));
            Assert.That(actualBody, Is.EqualTo(BODY));
            Assert.That(actualComment, Is.EqualTo(COMMENT));
        }

        [TestCase("n", "c")]
        [TestCase("n", "")]
        [TestCase("n", null)]
        public void Update_Template(string name, string? comment)
        {
            // Arrange
            var transport = Transport.Telegram;
            var lang = Lang.It;

            var temp = Utils.GetTemplateByFaker();
            var prevModTime = temp.ModificationTime;

            // Act
            temp.Update(transport, lang, name, comment);

            // Assert
            Assert.That(temp.Transport, Is.EqualTo(transport));
            Assert.That(temp.Lang, Is.EqualTo(lang));            
            Assert.That(temp.Name, Is.EqualTo(name));
            Assert.That(temp.Comment, Is.EqualTo(comment));
            Assert.That(temp.ModificationTime > prevModTime);
        }

        [TestCase(NAME_WITH_SPACES, COMMENT_WITH_SPACES)]
        public void Update_Template_TrimingSpacesInFields(string name, string? comment)
        {            
            var temp = Utils.GetTemplateByFaker();

            // Act
            temp.Update(Transport.Telegram, Lang.It, name, comment);

            var actualName = temp.Name;            
            var actualComment = temp.Comment;

            // Assert
            Assert.That(actualName, Is.EqualTo(NAME));            
            Assert.That(actualComment, Is.EqualTo(COMMENT));
        }

        [TestCase("")]
        [TestCase(null)]        
        public void Update_Template_ThrowInvalidParameterException(string name)
        {
            // Arrange
            var temp = Utils.GetTemplateByFaker();

            // Act
            TestDelegate actual = () => temp.Update(Transport.Telegram, Lang.It, name, null);

            // Assert
            Assert.Throws<InvalidParameterException>(actual);
        }

        [TestCase(151)]                
        public void Update_Template_MaxLenghtFields_ThrowInvalidParameterException(int nameMaxLenght)
        {
            // Arrange
            var faker = new Faker();
            var name = nameMaxLenght.RandomString();            
            var temp = Utils.GetTemplateByFaker();

            // Act
            TestDelegate actual = () => temp.Update(Transport.Telegram, Lang.It, name, null);

            // Assert
            Assert.Throws<InvalidParameterException>(actual);
        }

        [Test]
        public void Delete_Template()
        {
            // Arrange
            var temp = Utils.GetTemplateByFaker();
            var prevModTime = temp.ModificationTime;

            // Act
            temp.Delete();

            // Assert
            Assert.That(temp.Status, Is.EqualTo(Status.Deleted));
            Assert.That(temp.ModificationTime > prevModTime);
        }

        [Test]
        public void Delete_Template_ThrowBusinessRuleException()
        {
            // Arrange
            var temp = Utils.GetTemplateByFaker();

            // Act
            TestDelegate actual = () =>
            {
                temp.Delete();
                temp.Delete();
            };

            // Assert
            Assert.Throws<BusinessRuleException>(actual);
        }

        [Test]
        public void Enable_Template()
        {
            // Arrange
            var temp = Utils.GetTemplateByFaker();
            var prevModTime = temp.ModificationTime;

            // Act
            //temp.Enable();

            // Assert
            Assert.That(temp.Status, Is.EqualTo(Status.Enabled));
            Assert.That(temp.ModificationTime > prevModTime);
        }

        [Test]
        public void Enable_Template_ThrowBusinessRuleException()
        {
            // Arrange
            var temp = Utils.GetTemplateByFaker();

            // Act
            TestDelegate actual = () =>
            {
                //temp.Enable();
                //temp.Enable();
            };

            // Assert
            Assert.Throws<BusinessRuleException>(actual);
        }

        [Test]
        public void Disable_Template()
        {
            // Arrange
            var temp = Utils.GetTemplateByFaker();
            //temp.Enable();
            var prevModTime = temp.ModificationTime;

            // Act
            //temp.Disable();

            // Assert
            Assert.That(temp.Status, Is.EqualTo(Status.Enabled));
            Assert.That(temp.ModificationTime > prevModTime);
        }

        [Test]
        public void Disable_Application_ThrowBusinessRuleException()
        {
            // Arrange
            var temp = Utils.GetTemplateByFaker();

            // Act
            //TestDelegate actual = () => temp.Disable();

            // Assert
            //Assert.Throws<BusinessRuleException>(actual);
        }
    }
}
