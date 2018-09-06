using System.Globalization;
using System.Threading;
using digital.caliber.model.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace digital.caliber.tests.CalculationsTests
{
    public abstract class BaseTest
    {
        [TestInitialize]
        public void Setup()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            this.OnBaseTestSetup();
        }

        protected virtual void OnBaseTestSetup()
        {
        }

        /// <summary>
        /// Sets the rooth theet default messures.
        /// </summary>
        /// <returns></returns>
        public MeasuresTeethsViewModel SetRoothTheetDefaultMeasures()
        {
            var roothMeasures = new MeasuresTeethsViewModel
            {
                Tooth11 = 8,
                Tooth12 = 6,
                Tooth13 = 8,
                Tooth14 = 8,
                Tooth15 = 7,
                Tooth16 = 11,
                Tooth17 = 0,
                Tooth21 = 8,
                Tooth22 = 6,
                Tooth23 = 8,
                Tooth24 = 8,
                Tooth25 = 8,
                Tooth26 = 11,
                Tooth27 = 0,
                Tooth31 = 6,
                Tooth32 = 6,
                Tooth33 = 7,
                Tooth34 = 7,
                Tooth35 = 7,
                Tooth36 = 11,
                Tooth37 = 11,
                Tooth41 = 6,
                Tooth42 = 6,
                Tooth43 = 7,
                Tooth44 = 7,
                Tooth45 = 7,
                Tooth46 = 11,
                Tooth47 = 11
            };

            return roothMeasures;
        }

        /// <summary>
        /// Sets the mouth default messures.
        /// </summary>
        public MeasuresMouthViewModel SetMouthDefaultMeasures()
        {
            var mouthMeasure = new MeasuresMouthViewModel
            {
                RightSuperiorIncisive = 4,
                RightSuperiorCanine = 8,
                RightSuperiorPremolar = 16,
                LeftSuperiorIncisive = 4,
                LeftSuperiorCanine = 8,
                LeftSuperiorPremolar = 13,
                RightInferiorIncisive = 4,
                RightInferiorCanine = 8,
                RightInferiorPremolar = 11,
                LeftInferiorIncisive = 4,
                LeftInferiorCanine = 8,
                LeftInferiorPremolar = 19
            };

            return mouthMeasure;
        }
    }
}
