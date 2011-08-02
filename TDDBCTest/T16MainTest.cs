using System;
using NUnit.Framework;
using TDDBC;
using System.Collections.Generic;

namespace TDDBCTest
{
    [TestFixture]
    public class T16MainTest
    {
        private T16Main _t16Main;

        [SetUp]
        public void SetUp()
        {
            _t16Main = new T16Main();
        }

        #region T16MAIN-1: putでkeyとvalueを追加し、dumpで一覧表示、getでkeyに対応するvalueを取得できる
        [Test]
        public void Getで指定したkeyのvalueを取得()
        {
            _t16Main.Put("AA", "999");
            string result = _t16Main.Get("AA");

            Assert.That(result, Is.EqualTo("999"));
        }

        [Test]
        public void Dumpで登録されている一覧を表示()
        {
            _t16Main.Put("AA", "999");
            _t16Main.Put("ZZ", "111");
            string result = _t16Main.Dump();

            Assert.That(result, Is.EqualTo("999\n111\n"));
        }

        [Test]
        [ExpectedException("System.ArgumentNullException")]
        public void keyにnullをわたすと例外が発生()
        {
            _t16Main.Put(null, "999");
        }

        [Test]
        public void valueのnullは許容する()
        {
            _t16Main.Put("hoge", null);
        }
        #endregion

        #region T16MAIN-2: deleteで指定のkey-valueを削除
        [Test]
        public void Deleteで指定のkeyvalueを削除()
        {
            _t16Main.Put("AA", "999");
            _t16Main.Delete("AA");
            string result = _t16Main.Get("AA");

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public void 存在しないkeyが渡されたら何もしない()
        {
            _t16Main.Delete("AA");
        }

        [Test]
        [ExpectedException("System.ArgumentNullException")]
        public void nullを渡すと例外が発生する()
        {
            _t16Main.Delete(null);
        }
        #endregion

        #region T16MAIN-3: putの引数に既に存在するkeyが指定された場合、valueのみを更新する
        [Test]
        public void Putで既に存在するkeyの場合はvalueを更新する()
        {
            _t16Main.Put("HOGE", "fuga");
            _t16Main.Put("HOGE", "piyo");
            string result = _t16Main.Get("HOGE");

            Assert.That(result, Is.EqualTo("piyo"));
        }
        #endregion

        #region T16MAIN-4: keyとvalueのセットを一度に複数追加できる
        [Test]
        public void 複数のkeyとvalueをまとめて追加できる()
        {
            var list = new List<string[]>
                           {
                               new[] {"AA", "111"}, 
                               new[] {"BB", "222"}, 
                               new[] {"CC", "333"}
                           };
            _t16Main.Put(list);

            Assert.That(_t16Main.Get("AA"), Is.EqualTo("111"));
            Assert.That(_t16Main.Get("BB"), Is.EqualTo("222"));
            Assert.That(_t16Main.Get("CC"), Is.EqualTo("333"));
        }

        [Test]
        public void 同じキーが複数ある場合は一番最後に指定されたものが使用される()
        {
            var list = new List<string[]>
                           {
                               new[] {"AA", "111"},
                               new[] {"AA", "222"}, 
                               new[] {"CC", "333"}
                           };
            _t16Main.Put(list);

            Assert.That(_t16Main.Get("AA"), Is.EqualTo("222"));
            Assert.That(_t16Main.Get("CC"), Is.EqualTo("333"));
        }

        [Test]
        public void 既に存在するキーがある場合も今回指定したものが優先される()
        {
            _t16Main.Put("AA", "fuga");

            var list = new List<string[]>
                           {
                               new[] {"AA", "222"},
                               new[] {"CC", "333"}
                           };
            _t16Main.Put(list);

            Assert.That(_t16Main.Get("AA"), Is.EqualTo("222"));
            Assert.That(_t16Main.Get("CC"), Is.EqualTo("333"));
        }

        [Test]
        public void 指定した引数の中にnullのキーがある場合は例外を投げて状態を元に戻す()
        {
            _t16Main.Put("AA", "fuga");

            var list = new List<string[]>
                           {
                               new[] {"BB", "222"}, 
                               new[] {null, "333"}
                           };
            try
            {
                _t16Main.Put(list);
            }
            catch (ArgumentNullException)
            {
                Assert.That(_t16Main.Get("AA"), Is.EqualTo("fuga"));
                Assert.That(_t16Main.Get("BB"), Is.EqualTo(null));
            }
        }
        #endregion
    }
}
