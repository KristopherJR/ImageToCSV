using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Numerics;

/// <summary>
/// Author: Kristopher Randle
/// Version: 0.1, 12-12-21
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
        #endregion

        public ImageReader()
        {
            _outputPath = Directory.GetCurrentDirectory() + "/ImageToCSV.csv";
            _image = new Bitmap("test2.png");
            _csv = new string[_image.Width, _image.Height];

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
                    Vector3 rgb = new Vector3(pixel.R, pixel.G, pixel.B);

                    if (rgb == new Vector3(255, 255, 255)) // if WHITE pixel
                    {
                        _csv[i, j] = "0,";
                    }
                    if (rgb == new Vector3(0, 0, 0)) // if BLACK pixel
                    {
                        _csv[i, j] = "1,";
                    }
                }
            }
        }

        private void OutputTxt()
        {
            using (_streamWriter = new StreamWriter(_outputPath))
            {
                for (int i = 0; i < _csv.GetLength(0); i++) // rows/image width
                {
                    for (int j = 0; j < _csv.GetLength(1); j++) // columns/image height
                    {
                        _streamWriter.Write(_csv[i, j]);
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
    }
}