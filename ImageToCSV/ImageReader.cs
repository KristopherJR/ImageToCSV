using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;

/// <summary>
/// Author: Kristopher Randle
/// Version: 0.3, 12-12-21
/// </summary>
namespace ImageToCSV
{
    public class ImageReader
    {
        #region FIELDS
        private string _outputPath;
        private Bitmap _image;
        private string[,] _csv;
        private StreamWriter _streamWriter;
        private byte[] _whitePixel;
        private byte[] _blackPixel;
        private byte[] _customPixel;
        #endregion

        public ImageReader()
        {
            _outputPath = Directory.GetCurrentDirectory() + "/ImageToCSV.csv";
            _image = new Bitmap("1 to 1 - Floor Plan V2.png");
            _csv = new string[_image.Width, _image.Height];
            _whitePixel = new byte[] { 255, 255, 255 };
            _blackPixel = new byte[] { 0, 0, 0 };
            _customPixel = new byte[] { 100, 255, 100 }; // green pixel

            ParseImage();
            OutputTxt();
            PrintCurrentCSV();
        }

        private void ParseImage()
        {
            for (int i = 0; i < _image.Width; i++)
            {
                for (int j = 0; j < _image.Height; j++)
                {
                    Color pixel = _image.GetPixel(i, j);
                    byte[] rgb = new byte[] { pixel.R, pixel.G, pixel.B };

                    if (rgb.SequenceEqual(_whitePixel))
                    {
                        _csv[i, j] = "0,";
                    }
                    if (rgb.SequenceEqual(_blackPixel))
                    {
                        _csv[i, j] = "1,";
                    }
                    if (rgb.SequenceEqual(_customPixel))
                    {
                        _csv[i, j] = "2,";
                    }
                }
            }
        }

        private void OutputTxt()
        {
            using (_streamWriter = new StreamWriter(_outputPath))
            {
                for (int i = 0; i < _csv.GetLength(1); i++) // rows/image width
                {
                    for (int j = 0; j < _csv.GetLength(0); j++) // columns/image height
                    {
                        _streamWriter.Write(_csv[j, i]);
                    }
                    _streamWriter.WriteLine(""); // new line
                }
            }
        }

        private void PrintCurrentCSV()
        {
            for (int i = 0; i < _csv.GetLength(0); i++)
            {
                for (int j = 0; j < _csv.GetLength(1); j++)
                {
                    Debug.Write(_csv[i, j]);
                }
                Debug.WriteLine("");
            }
        }

        /// <summary>
        /// Prints the RGB value of a pixel represented as a byte array.
        /// </summary>
        /// <param name="pPixel">The byte array representing the RGB pixels to be printed to the console.</param>
        private void PrintPixelValues(byte[] pPixel)
        {
            Debug.WriteLine("R: " + pPixel[0] +
                           " G: " + pPixel[1] +
                           " B: " + pPixel[2]);
        }
    }
}