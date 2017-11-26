using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace SetupMeetings.Infrastructure.Databases.Xml
{
    public class TestObject
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    class XmlTestObjectsRepository : XmlRepository<TestObject, int>
    {
        public XmlTestObjectsRepository(string filePath) : base(filePath, o => o.Id)
        {
        }
    }

    [TestClass]
    public class XmlRepositoryTest
    {
        private string _filePath;

        [TestInitialize]
        public void Setup()
        {
            _filePath = Path.GetTempFileName();
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        [TestCleanup]
        public void Teardown()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        [TestMethod]
        public void オブジェクトを永続化して正常に再インスタンス化できること()
        {
            var sut = new XmlTestObjectsRepository(_filePath);
            sut.Save(new TestObject() { Id = 1, Value = "Value 1" });
            var value = sut.FindById(1);
            Assert.AreEqual(1, value.Id);
            Assert.AreEqual("Value 1", value.Value);
            Assert.IsTrue(File.Exists(_filePath));

            sut = new XmlTestObjectsRepository(_filePath);
            value = sut.FindById(1);
            Assert.AreEqual(1, value.Id);
            Assert.AreEqual("Value 1", value.Value);
        }
    }
}
