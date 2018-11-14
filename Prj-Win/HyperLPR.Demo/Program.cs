using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HyperLPR.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new byte[2000];

            var count = LibHyperLPRCW.PlateRecognize(
                "hyperlpr_model_中文/cascade.xml",
                "hyperlpr_model_中文/HorizonalFinemapping.prototxt", 
                "hyperlpr_model_中文/HorizonalFinemapping.caffemodel",
                "hyperlpr_model_中文/Segmentation.prototxt", 
                "hyperlpr_model_中文/Segmentation.caffemodel",
                "hyperlpr_model_中文/CharacterRecognization.prototxt",
                "hyperlpr_model_中文/CharacterRecognization.caffemodel",
                "hyperlpr_image/浙CD73L8.jpg",
                data,
                data.Length
                );

            var s1 = Encoding.ASCII.GetString(data, 0, Math.Min(count, data.Length));
            var s2 = Encoding.UTF8.GetString(data, 0, Math.Min(count, data.Length));
            var s3 = Encoding.Unicode.GetString(data, 0, Math.Min(count, data.Length));
            var s4 = Encoding.Default.GetString(data, 0, Math.Min(count, data.Length));
            Console.WriteLine("program end");
            Console.ReadKey();
        }
    }

    public static class LibHyperLPRCW
    {
        [DllImport(
            "libhyperlpr_cw.dll",
            CallingConvention = CallingConvention.StdCall,
            EntryPoint = "plate_recognize")]
        public static extern int PlateRecognize(
            string detector_filename,
            string finemapping_prototxt,
            string finemapping_caffemodel,
            string segmentation_prototxt,
            string segmentation_caffemodel,
            string charRecognization_proto,
            string charRecognization_caffemodel,
            string imageFileName,
            byte[] buffer,
            int bufferSize
            );
    }
}
