using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Pruebas_de_selenium
{
    public class AccionActUITests : SeleniumTestBase
    {

        private void Login()
        {
            driver.Navigate().GoToUrl("http://localhost:4200/"); // tu URL local o de pruebas

            // Si la API devuelve HTML:
            var bodyText = driver.FindElement(By.TagName("body")).Text;

            driver.FindElement(By.Id("username")).SendKeys("drcecontre@gmail.com");
            driver.FindElement(By.Id("password")).SendKeys("user1234");
            driver.FindElement(By.Id("Ingresar")).Click();

        }
        [Fact]
        public void GetAll_DisplaysDataCorrectly()
        {
            driver.Navigate().GoToUrl("http://localhost:4200/"); // tu URL local o de pruebas

            // Si la API devuelve HTML:;

            driver.FindElement(By.Id("username")).SendKeys("drcecontre@gmail.com");
            driver.FindElement(By.Id("password")).SendKeys("user1234");
            driver.FindElement(By.Id("Ingresar")).Click();


            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(x => x.FindElement(By.TagName("body")).Text.Contains("Ha iniciado sesión correctamente, bienvenido"));



            var messagesText = driver.FindElement(By.TagName("body")).Text;

            Assert.Contains("Ha iniciado sesión correctamente, bienvenido", messagesText);

        }
    }

}