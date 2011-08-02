using NUnit.Framework;
using TDDBC;

namespace TDDBCTest
{
    [TestFixture]
    public class T16MainTest
    {
        [Test]
        public void Getで指定したkeyのvalueを取得()
        {
            var t16Main = new T16Main();
            t16Main.Put("AA", "999");
            string result = t16Main.Get("AA");

            Assert.That(result, Is.EqualTo("999"));
        }

        [Test]
        public void Dumpで登録されている一覧を表示()
        {
            var t16Main = new T16Main();
            t16Main.Put("AA", "999");
            t16Main.Put("ZZ", "111");
            string result = t16Main.Dump();

            Assert.That(result, Is.EqualTo("999\n111\n"));
        }
        
        [Test]
        [ExpectedException("System.ArgumentNullException")]
        public void keyにnullをわたすと例外が発生()
        {
            var t16Main = new T16Main();
            t16Main.Put(null, "999");
        }

        [Test]
        public void valueのnullは許容する()
        {
            var t16Main = new T16Main();
            t16Main.Put("hoge", null);
        }
    }
}
