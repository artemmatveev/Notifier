namespace NotifierApi.Domain.Tests
{
    internal sealed class EmailMessageTests
    {
        const string FROM_NAME = "f";
        const string FROM_EMAIL = "f@a.com";
        const string TO_NAME = "t";
        const string TO_EMAIL = "t@a.com";
        const string SUBJECT = "s";
        const string BODY = "b";
        const string FROM_NAME_WITH_SPACES = $" {FROM_NAME} ";
        const string FROM_EMAIL_WITH_SPACES = $" {FROM_EMAIL} ";
        const string TO_NAME_WITH_SPACES = $" {TO_NAME} ";
        const string TO_EMAIL_WITH_SPACES = $" {TO_EMAIL} ";
        const string SUBJECT_WITH_SPACES = $" {SUBJECT} ";
        const string BODY_WITH_SPACES = $" {BODY} ";

        [Test, Order(1)]
        public void Create_EmailMessage()
        {
            // Act
            var msg = Utils.GetEmailMessageByFaker();

            // Assert
            Assert.That(msg.NotificationId, Is.Not.EqualTo(default));
            Assert.That(msg.ResourceId, Is.Not.EqualTo(default));
            Assert.That(msg.FromName, Is.Not.EqualTo(default));
            Assert.That(msg.FromEmail, Is.Not.EqualTo(default));
            Assert.That(msg.ToName, Is.Not.EqualTo(default));
            Assert.That(msg.ToEmail, Is.Not.EqualTo(default));
            Assert.That(msg.Subject, Is.Not.EqualTo(default));
            Assert.That(msg.Body, Is.Not.EqualTo(default));
            Assert.That(msg.Priority, Is.Not.EqualTo(Priority.Unspecified));
            Assert.That(msg.CreationTime, Is.Not.EqualTo(default));
            Assert.That(msg.ReceiveTime, Is.Null);
            Assert.That(msg.SentTime, Is.Null);
        }

        [TestCase("", FROM_EMAIL, TO_NAME, TO_EMAIL, SUBJECT, BODY)]
        [TestCase(null, FROM_EMAIL, TO_NAME, TO_EMAIL, SUBJECT, BODY)]
        [TestCase(FROM_NAME, "p", TO_NAME, TO_EMAIL, SUBJECT, BODY)]
        [TestCase(FROM_NAME, "", TO_NAME, TO_EMAIL, SUBJECT, BODY)]
        [TestCase(FROM_NAME, null, TO_NAME, TO_EMAIL, SUBJECT, BODY)]
        [TestCase(FROM_NAME, FROM_EMAIL, "", TO_EMAIL, SUBJECT, BODY)]
        [TestCase(FROM_NAME, FROM_EMAIL, null, TO_EMAIL, SUBJECT, BODY)]
        [TestCase(FROM_NAME, FROM_EMAIL, TO_NAME, "t", SUBJECT, BODY)]
        [TestCase(FROM_NAME, FROM_EMAIL, TO_NAME, "", SUBJECT, BODY)]
        [TestCase(FROM_NAME, FROM_EMAIL, TO_NAME, null, SUBJECT, BODY)]
        [TestCase(FROM_NAME, FROM_EMAIL, TO_NAME, TO_EMAIL, "", BODY)]
        [TestCase(FROM_NAME, FROM_EMAIL, TO_NAME, TO_EMAIL, null, BODY)]
        [TestCase(FROM_NAME, FROM_EMAIL, TO_NAME, TO_EMAIL, SUBJECT, "")]
        [TestCase(FROM_NAME, FROM_EMAIL, TO_NAME, TO_EMAIL, SUBJECT, null)]
        public void Create_EmailMessage_ThrowInvalidParameterException(string fromName, string fromEmail, string toName,
            string toEmail, string subject, string body)
        {
            // Act
            TestDelegate actual = () => Utils.GetEmailMessageByFaker(
                fromName: fromName,
                fromEmail: fromEmail,
                toName: toName,
                toEmail: toEmail,
                subject: subject,
                body: body);

            // Assert
            Assert.Throws<InvalidParameterException>(actual);
        }


        [TestCase(256, 1, 1, 1, 1)]
        [TestCase(1, 321, 1, 1, 1)]
        [TestCase(1, 1, 256, 1, 1)]
        [TestCase(1, 1, 1, 321, 1)]
        [TestCase(1, 1, 1, 1, 151)]        
        public void Create_EmailMessage_FieldsMaxNameLenght_ThrowInvalidParameterException(int fromNameMaxLenght, int fromEmailMaxLenght, 
            int toNameMaxLenght, int toEmailMaxLenght, int subjectMaxLenght)
        {
            // Arrange            
            var fromName = fromNameMaxLenght.RandomString();
            var fromEmail = fromEmailMaxLenght.RandomString();
            var toName = toNameMaxLenght.RandomString();
            var toEmail = toEmailMaxLenght.RandomString();
            var subject = subjectMaxLenght.RandomString();

            // Act
            TestDelegate actual = () => Utils.GetEmailMessageByFaker(
                fromName: fromName,
                fromEmail: fromEmail,
                toName: toName,
                toEmail: toEmail,
                subject: subject);

            // Assert
            Assert.Throws<InvalidParameterException>(actual);
        }

        [TestCase(FROM_NAME_WITH_SPACES,
                  FROM_EMAIL_WITH_SPACES,
                  TO_NAME_WITH_SPACES,
                  TO_EMAIL_WITH_SPACES,
                  SUBJECT_WITH_SPACES,
                  BODY_WITH_SPACES)]
        public void Create_EmailMessage_TrimingSpacesInFields(string fromName, string fromEmail, string toName,
        string toEmail, string subject, string body)
        {            
            // Act
            var msg = Utils.GetEmailMessageByFaker(
                fromName: fromName,
                fromEmail: fromEmail,
                toName: toName,
                toEmail: toEmail,
                subject: subject,
                body: body);

            var actualFromName = msg.FromName;
            var actualFromEmail = msg.FromEmail;
            var actualToName = msg.ToName;
            var actualToEmail = msg.ToEmail;
            var actualSubject = msg.Subject;
            var actualBody = msg.Body;

            // Assert
            Assert.That(actualFromName, Is.EqualTo(FROM_NAME));
            Assert.That(actualFromEmail, Is.EqualTo(FROM_EMAIL));
            Assert.That(actualToName, Is.EqualTo(TO_NAME));
            Assert.That(actualToEmail, Is.EqualTo(TO_EMAIL));
            Assert.That(actualSubject, Is.EqualTo(SUBJECT));
            Assert.That(actualBody, Is.EqualTo(BODY));
        }

        [Test]
        public void Receive_EmailMessage()
        {
            // Arrange
            var msg = Utils.GetEmailMessageByFaker();

            // Act
            msg.Receive();

            // Assert
            Assert.That(msg.ReceiveTime, Is.Not.Null);
        }

        [Test]
        public void Send_EmailMessage()
        {
            // Arrange
            var msg = Utils.GetEmailMessageByFaker();

            // Act
            msg.Send();

            // Assert
            Assert.That(msg.SentTime, Is.Not.Null);
        }
    }
}
