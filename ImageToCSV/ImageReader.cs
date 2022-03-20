using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

/// <summary>
/// Author: Kristopher Randle
/// Version: 0.4, 12-12-21
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
        private byte[] _redPixel;
        private byte[] _bluePixel;
        private byte[] _pinkPixel;
        private byte[] _yellowPixel;
        private byte[] _customPixel;

        private string _whitePixelTextureId;
        private string _blackPixelTextureId;
        private string _redPixelTextureId;
        private string _bluePixelTextureId;
        private string _pinkPixelTextureId;
        private string _yellowPixelTextureId;
        private string _customPixel1TextureId;
        #endregion

        public ImageReader()
        {
            _outputPath = Directory.GetCurrentDirectory() + "/ImageToCSV.csv";
            _image = new Bitmap("1to3-Floor-FloorPlan-Quadrants.png");
            _csv = new string[_image.Width, _image.Height];
            _whitePixel = new byte[] { 255, 255, 255 };
            _blackPixel = new byte[] { 0, 0, 0 };
            _redPixel = new byte[] { 255, 0, 0 }; // red, quadrant 1, tile id = 35
            _bluePixel = new byte[] { 0, 0, 255 }; // blue, quadrant 2, tile id = 0
            _pinkPixel = new byte[] { 255, 0, 255 }; // pink, quadrant 3, tile id = 85
            _yellowPixel = new byte[] { 255, 255, 0 }; // yellow, quadrant 4, tile id = 110
            _customPixel = new byte[] { 100, 255, 100 }; // green pixel, windows

            // Adjust these with the Ids of your desired textures from your tilesheets:
            _whitePixelTextureId = "-1";
            _blackPixelTextureId = "50";
            _redPixelTextureId = "35";
            _bluePixelTextureId = "0";
            _pinkPixelTextureId = "85";
            _yellowPixelTextureId = "110";
            _customPixel1TextureId = "30";

        ParseImage();
            OutputTxt();
            PrintCurrentCSV();
        }

        private void ParseImage()
        {
            for (int y = 0; y < _image.Height; y++)
            {
                for (int x = 0; x < _image.Width; x++)
                {
                    Color pixel = _image.GetPixel(x,y);
                    byte[] rgb = new byte[] { pixel.R, pixel.G, pixel.B };

                    if(x == _image.Width - 1)
                    {
                        if (rgb.SequenceEqual(_whitePixel))
                        {
                            _csv[x,y] = _whitePixelTextureId;
                        }
                        if (rgb.SequenceEqual(_blackPixel))
                        {
                            _csv[x,y] = _blackPixelTextureId;
                        }
                        if (rgb.SequenceEqual(_redPixel))
                        {
                            _csv[x, y] = _redPixelTextureId;
                        }
                        if (rgb.SequenceEqual(_bluePixel))
                        {
                            _csv[x, y] = _bluePixelTextureId;
                        }
                        if (rgb.SequenceEqual(_pinkPixel))
                        {
                            _csv[x, y] = _pinkPixelTextureId;
                        }
                        if (rgb.SequenceEqual(_yellowPixel))
                        {
                            _csv[x, y] = _yellowPixelTextureId;
                        }
                        if (rgb.SequenceEqual(_customPixel))
                        {
                            _csv[x,y] = _customPixel1TextureId;
                        }
                    }
                    else
                    {
                        if (rgb.SequenceEqual(_whitePixel))
                        {
                            _csv[x,y] = _whitePixelTextureId + ",";
                        }
                        if (rgb.SequenceEqual(_blackPixel))
                        {
                            _csv[x,y] = _blackPixelTextureId + ",";
                        }
                        if (rgb.SequenceEqual(_redPixel))
                        {
                            _csv[x, y] = _redPixelTextureId + ",";
                        }
                        if (rgb.SequenceEqual(_bluePixel))
                        {
                            _csv[x, y] = _bluePixelTextureId + ",";
                        }
                        if (rgb.SequenceEqual(_pinkPixel))
                        {
                            _csv[x, y] = _pinkPixelTextureId + ",";
                        }
                        if (rgb.SequenceEqual(_yellowPixel))
                        {
                            _csv[x, y] = _yellowPixelTextureId + ",";
                        }
                        if (rgb.SequenceEqual(_customPixel))
                        {
                            _csv[x,y] = _customPixel1TextureId + ",";
                        }
                    }  
                }
            }
        }

        private void OutputTxt()
        {
            using (_streamWriter = new StreamWriter(_outputPath))
            {
                for (int y = 0; y < _csv.GetLength(1); y++)
                {
                    for (int x = 0; x < _csv.GetLength(0); x++)
                    {
                        _streamWriter.Write(_csv[x,y]);
                    }
                    _streamWriter.WriteLine(""); // new line
                }
            }
        }

        private void PrintCurrentCSV()
        {
            for (int y = 0; y < _csv.GetLength(1); y++)
            {
                for (int x = 0; x < _csv.GetLength(0); x++)
                {
                    Debug.Write(_csv[x,y]);
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