using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;

namespace LH.Controls
{
    public partial class LoadingAnimation : UserControl
    {
        public const int FrameSize = 64;

        private Bitmap animationStrip;
        private int framesCount;

        public LoadingAnimation()
        {
            InitializeComponent();
            Width = FrameSize;
            Height = FrameSize;

            animationStrip = new ResourceManager(GetType()).GetObject("LoadingAnimationStrip") as Bitmap;
            if (animationStrip == null)
                throw new Exception("The required LoadingAnimationStrip resource is missing or invalid.");

            framesCount = animationStrip.Width / FrameSize;
        }

        private void DrawFrame(int frameIndex, Graphics g)
        {
            g.DrawImage(animationStrip, ClientRectangle, new Rectangle(frameIndex * FrameSize, 0, FrameSize, FrameSize), 
                GraphicsUnit.Pixel);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawFrame(0, e.Graphics);
        }
    }
}
