using digital.caliber.model.Models;

namespace digital.caliber.model.ViewModels
{
    public class BonesSpaces
    {
        public decimal PerineoSuperiorArch { get; set; }
        
        public decimal PerineoInferiorArch { get; set; }

        public decimal SuperiorBonesIntercanine { get; set; }
        
        public decimal InferiorBonesIntercanine { get; set; }

        public decimal Bones13To23 { get; set; }
        
        public decimal Bones33To43 { get; set; }

        public static BonesSpaces GetBonesCalculation(MeasuresMouthViewModel mouseMeasure)
        {
            var bonesSpaces = new BonesSpaces
            {
                PerineoSuperiorArch = mouseMeasure.LeftSuperiorCanine + mouseMeasure.LeftSuperiorIncisive +
                                      mouseMeasure.LeftSuperiorPremolar +
                                      mouseMeasure.RightSuperiorCanine +
                                      mouseMeasure.RightSuperiorIncisive +
                                      mouseMeasure.RightSuperiorPremolar,
                PerineoInferiorArch = mouseMeasure.LeftInferiorCanine + mouseMeasure.LeftInferiorIncisive +
                                      mouseMeasure.LeftInferiorPremolar +
                                      mouseMeasure.RightInferiorCanine +
                                      mouseMeasure.RightInferiorIncisive +
                                      mouseMeasure.RightInferiorPremolar,
                SuperiorBonesIntercanine =
                    mouseMeasure.LeftSuperiorIncisive + mouseMeasure.RightSuperiorIncisive,
                InferiorBonesIntercanine =
                    mouseMeasure.LeftInferiorIncisive + mouseMeasure.RightInferiorIncisive,
                Bones13To23 = mouseMeasure.LeftSuperiorIncisive + mouseMeasure.LeftSuperiorCanine +
                              mouseMeasure.RightSuperiorIncisive + mouseMeasure.RightSuperiorCanine,
                Bones33To43 = mouseMeasure.LeftInferiorIncisive + mouseMeasure.LeftInferiorCanine +
                              mouseMeasure.RightInferiorIncisive + mouseMeasure.RightInferiorCanine
            };

            return bonesSpaces;
        }
    }
}
