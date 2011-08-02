using NUnit.Framework;
using TDDBC;

namespace TDDBCTest
{
    [TestFixture]
    public class T16MainTest
    {
        private T16Main t16Main;

        [SetUp]
        public void SetUp()
        {
            t16Main = new T16Main();
        }

        #region T16MAIN-1: putでkeyとvalueを追加し、dumpで一覧表示、getでkeyに対応するvalueを取得できる
        [Test]
        public void Getで指定したkeyのvalueを取得()
        {
            t16Main.Put("AA", "999");
            string result = t16Main.Get("AA");

            Assert.That(result, Is.EqualTo("999"));
        }

        [Test]
        public void Dumpで登録されている一覧を表示()
        {
            t16Main.Put("AA", "999");
            t16Main.Put("ZZ", "111");
            string result = t16Main.Dump();

            Assert.That(result, Is.EqualTo("999\n111\n"));
        }
        
        [Test]
        [ExpectedException("System.ArgumentNullException")]
        public void keyにnullをわたすと例外が発生()
        {
            t16Main.Put(null, "999");
        }

        [Test]
        public void valueのnullは許容する()
        {
            t16Main.Put("hoge", null);
        }
        #endregion

        #region T16MAIN-2: deleteで指定のkey-valueを削除
        [Test]
        public void Deleteで指定のkeyvalueを削除()
        {
            t16Main.Put("AA", "999");
            t16Main.Delete("AA");
            string result = t16Main.Get("AA");

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public void 存在しないkeyが渡されたら何もしない()
        {
            t16Main.Delete("AA");
        }

        [Test]
        [ExpectedException("System.ArgumentNullException")]
        public void nullを渡すと例外が発生する()
        {
            t16Main.Delete(null);
        }
        #endregion

        #region T16MAIN-3: putの引数に既に存在するkeyが指定された場合、valueのみを更新する
        [Test]
        public void Putで既に存在するkeyの場合はvalueを更新する()
        {
            t16Main.Put("HOGE", "fuga");
            t16Main.Put("HOGE", "piyo");
            string result = t16Main.Get("HOGE");

            Assert.That(result, Is.EqualTo("piyo"));
        }
        #endregion
    }
}
