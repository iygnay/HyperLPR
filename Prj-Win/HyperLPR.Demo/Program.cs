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
            var data = new byte[20000];

            var count = LibHyperLPRCW.PlateRecognize(
                "hyperlpr_model/cascade.xml",
                "hyperlpr_model/HorizonalFinemapping.prototxt", 
                "hyperlpr_model/HorizonalFinemapping.caffemodel",
                "hyperlpr_model/Segmentation.prototxt", 
                "hyperlpr_model/Segmentation.caffemodel",
                "hyperlpr_model/CharacterRecognization.prototxt", 
                "hyperlpr_model/CharacterRecognization.caffemodel",
                "hyperlpr_image/浙C1993A.jpg",
                data,
                data.Length
                );

            var s1 = Encoding.ASCII.GetString(data, 0, Math.Min(count, data.Length));
            var s2 = Encoding.UTF8.GetString(data, 0, Math.Min(count, data.Length));
            var s3 = Encoding.Unicode.GetString(data, 0, Math.Min(count, data.Length));
            var s4 = Encoding.Default.GetString(data, 0, Math.Min(count, data.Length));
            Console.WriteLine($"1program end {count}: {s4}");
            Console.ReadKey();
        }
    }

    public static class LibHyperLPRCW
    {
        [DllImport(
            "libhyperlpr_cw_v2.0.3.dll",
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
            //string segmentationfree_proto,
            //string segmentationfree_caffemodel,
            string imageFileName,
            byte[] buffer,
            int bufferSize
            );
    }
}
