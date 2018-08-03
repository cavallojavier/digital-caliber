using digital.caliber.model.Models;
using digital.caliber.services.Calculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace digital.caliber.tests.CalculationsTests.TanakaTests
{
    [TestClass]
    public class TanakaJohnstonTests : BaseTest
    {
        private MeasuresTeethsViewModel theethMessure;

        [TestInitialize]
        public void TestInit()
        {
            theethMessure = SetRoothTheetDefaultMessures();
        }

        [TestMethod]
        public void GetTanakaShouldSuccess()
        {
            var tanakaResult = TanakaCalculator.GetResult(theethMessure);

            Assert.AreEqual(tanakaResult.Inferior, (decimal)22.5, "Inferior Invalid");
            Assert.AreEqual(tanakaResult.Superior, (decimal)23, "Superior Invalid");
        }
    }
}
