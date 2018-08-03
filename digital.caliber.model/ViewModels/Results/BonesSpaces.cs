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

        public static BonesSpaces GetBonesCalculation(MeasuresMouthViewModel mouseMessure)
        {
            var bonesSpaces = new BonesSpaces
            {
                PerineoSuperiorArch = mouseMessure.LeftSuperiorCanine + mouseMessure.LeftSuperiorIncisive +
                                      mouseMessure.LeftSuperiorPremolar +
                                      mouseMessure.RightSuperiorCanine +
                                      mouseMessure.RightSuperiorIncisive +
                                      mouseMessure.RightSuperiorPremolar,
                PerineoInferiorArch = mouseMessure.LeftInferiorCanine + mouseMessure.LeftInferiorIncisive +
                                      mouseMessure.LeftInferiorPremolar +
                                      mouseMessure.RightInferiorCanine +
                                      mouseMessure.RightInferiorIncisive +
                                      mouseMessure.RightInferiorPremolar,
                SuperiorBonesIntercanine =
                    mouseMessure.LeftSuperiorIncisive + mouseMessure.RightSuperiorIncisive,
                InferiorBonesIntercanine =
                    mouseMessure.LeftInferiorIncisive + mouseMessure.RightInferiorIncisive,
                Bones13To23 = mouseMessure.LeftSuperiorIncisive + mouseMessure.LeftSuperiorCanine +
                              mouseMessure.RightSuperiorIncisive + mouseMessure.RightSuperiorCanine,
                Bones33To43 = mouseMessure.LeftInferiorIncisive + mouseMessure.LeftInferiorCanine +
                              mouseMessure.RightInferiorIncisive + mouseMessure.RightInferiorCanine
            };

            return bonesSpaces;
        }
    }
}
