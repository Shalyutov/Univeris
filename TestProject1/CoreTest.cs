using Microsoft.VisualStudio.TestTools.UnitTesting;
using Univeris;
using Univeris.Global;
using Univeris.Identity;
using Univeris.Identity.Claims;

namespace TestProject1
{
    [TestClass]
    public class CoreTest
    {
        Core core;
        [TestInitialize]
        public void Init()
        {
            core = new Core();
            Assert.IsNotNull(core);
        }
        [TestMethod("Проверка существования экземпляра контекст данных")]
        public void TestInit()
        {
            Assert.IsNotNull(core.Data);
        }
        [TestMethod("Инициализация программного шаблона")]
        public void TestTemplate()
        {
            core.Start(true);
            bool users = core.Data.Context.Users.Count > 0;
            bool faculties = core.Data.Context.Faculties.Count > 0;
            bool cources = core.Data.Context.Courses.Count > 0;

            Assert.IsTrue(users && faculties && cources);
        }
        [TestMethod("Инициализация данных из конфигурационного файла")]
        public void TestData()
        {
            try
            {
                core.Start("data.json");
            }
            catch (InitException)
            {
                Assert.Fail();
            }

            bool users = core.Data.Context.Users.Count > 0;
            bool faculties = core.Data.Context.Faculties.Count > 0;
            bool cources = core.Data.Context.Courses.Count > 0;

            Assert.IsTrue(users && faculties && cources);
        }
        [TestMethod("Инициализация данных из несуществующего файла")]
        public void TestErrorDataInit()
        {
            Assert.ThrowsException<InitException>(() => 
            { 
                core.Start("config.json"); 
            });
        }
        [TestMethod("Проверка аутентификации")]
        public void TestSignIn()
        {
            core.Start(true);
            core.SignIn("андрей", "123");

            var expect = new User(10001, "андрей", "shalyutov.a@ya.ru", "+79000000000", "123");
            Assert.AreEqual(expect, core.User);
        }
        [TestMethod("Проверка аутентификации с некорректным паролем")]
        public void TestIncorrectPassword()
        {
            core.Start(true);

            Assert.IsFalse(core.SignIn("андрей", "759"));
        }
        [TestMethod("Проверка доступа")]
        public void TestAccess()
        {
            core.Start(true);
            Assert.AreEqual(AccessLevel.Student, core.GetAccessLevel(core.Data.Context.Users[0], core.Data.Context.Courses[0]));
        }
        [TestMethod("Проверка отказа в доступе")]
        public void TestErrorAccess()
        {
            core.Start(true);
            Assert.IsNull(core.GetAccessLevel(core.Data.Context.Users[2], core.Data.Context.Courses[0]));
        }
        [TestMethod("Проверка отказа в доступе к неизвестному ресурсу")]
        public void TestInvalidClaim()
        {
            core.Start(true);
            var user = new User(10004, "", "", "", "");
            Assert.IsNull(core.GetAccessLevel(user, core.Data.Context.Courses[0]));
        }
        [TestCleanup]
        public void TestCleanup()
        {
            core.SignOut();
            Assert.IsNull(core.User);
        }
    }
}