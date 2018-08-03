﻿using digital.caliber.model.Models;
using digital.caliber.services.Calculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace digital.caliber.tests.CalculationsTests.MoyersTests
{
    [TestClass]
    public class MoyersTests : BaseTest
    {
        private MeasuresMouthViewModel mouthMessure;
        private MeasuresTeethsViewModel theethMessure;

        [TestInitialize]
        public void TestInit()
        {
            SetDefaultMessuresInputsValue();
        }

        [TestMethod]
        public void GetMoyersShouldSuccess()
        {
            var moyers = MoyersCalculator.GetResult(mouthMessure, theethMessure);

            Assert.AreEqual(moyers.RightSuperior, (decimal)0.9, "Superior Right Invalid");
            Assert.AreEqual(moyers.LeftSuperior, (decimal)-2.1, "Superior Left Invalid");
            Assert.AreEqual(moyers.RightInferior, (decimal)-3.8, "Inferior Right Invalid");
            Assert.AreEqual(moyers.LeftInferior, (decimal)4.2, "Inferior Left Invalid");
        }

        private void SetDefaultMessuresInputsValue()
        {
            mouthMessure = SetMouthDefaultMessures();
            theethMessure = SetRoothTheetDefaultMessures();
        }
    }
}
