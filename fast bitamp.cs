using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCViewer_2
{
    unsafe class Bitmapfast
    {
        private Bitmap _bmp = null;
        private System.Drawing.Imaging.BitmapData _img = null;
        public Bitmapfast(Bitmap original)
        {
            _bmp = original;
        }
        public void BeginAccess()
        {
                _img = _bmp.LockBits(new Rectangle(0, 0, _bmp.Width, _bmp.Height),
         System.Drawing.Imaging.ImageLockMode.ReadWrite,
         System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }
        public void EndAccess()
        {
                _bmp.UnlockBits(_img);
                _img = null;
        }
        public Color GetPixel(int x, int y)
        {
            IntPtr adr = _img.Scan0;
            int pos = x * 4 + _img.Stride * y;
            byte b = System.Runtime.InteropServices.Marshal.ReadByte(adr, pos + 0);
            byte g = System.Runtime.InteropServices.Marshal.ReadByte(adr, pos + 1);
            byte r = System.Runtime.InteropServices.Marshal.ReadByte(adr, pos + 2);
            byte a = System.Runtime.InteropServices.Marshal.ReadByte(adr, pos + 3);

            return Color.FromArgb(a, r, g, b);
        }

        public void SetPixel(int x, int y, Color col)
        {
            IntPtr adr = _img.Scan0;
            int pos = x * 4 + _img.Stride * y;
            System.Runtime.InteropServices.Marshal.WriteByte(adr, pos + 0, col.B);
            System.Runtime.InteropServices.Marshal.WriteByte(adr, pos + 1, col.G);
            System.Runtime.InteropServices.Marshal.WriteByte(adr, pos + 2, col.R);
            System.Runtime.InteropServices.Marshal.WriteByte(adr, pos + 3, col.A);
        }

        public byte[] GetBytes(int x, int y)
        {
            IntPtr adr = _img.Scan0;
            int pos = x * 4 + _img.Stride * y;
            byte b = System.Runtime.InteropServices.Marshal.ReadByte(adr, pos + 0);
            byte g = System.Runtime.InteropServices.Marshal.ReadByte(adr, pos + 1);
            byte r = System.Runtime.InteropServices.Marshal.ReadByte(adr, pos + 2);
            byte a = System.Runtime.InteropServices.Marshal.ReadByte(adr, pos + 3);
            return new byte[] { a, r, g, b };
        }
    }
}
