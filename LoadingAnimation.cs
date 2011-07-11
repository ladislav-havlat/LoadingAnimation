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
    /// <summary>
    /// An Ajax-style loading animation control.
    /// </summary>
    public partial class LoadingAnimation : UserControl
    {
        /// <summary>
        /// Size of the animation frames.
        /// </summary>
        public const int FrameSize = 64;

        #region Private fields
        /// <summary>
        /// The animation strip to be the images taken from.
        /// </summary>
        private Bitmap animationStrip;
        /// <summary>
        /// Index of the displayed frame.
        /// </summary>
        private int currentFrame;
        /// <summary>
        /// Total count of frames in the animation.
        /// </summary>
        private int framesCount;
        #endregion

        #region Public properties
        /// <summary>
        /// If true, the animation is running. If false, the animation is paused.
        /// </summary>
        [Description("If true, the animation is running. If false, the animation is paused.")]
        [DefaultValue(true)]
        public bool Active
        {
            get { return frameTimer.Enabled; }
            set { frameTimer.Enabled = value; }
        }

        /// <summary>
        /// Index of the displayed frame.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CurrentFrame
        {
            get { return currentFrame; }
            set
            {
                currentFrame = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Time interval between frames.
        /// </summary>
        [Description("Time interval between frames.")]
        [DefaultValue(50)]
        public int FrameInterval
        {
            get { return frameTimer.Interval; }
            set { frameTimer.Interval = value; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of LoadingAnimation.
        /// </summary>
        public LoadingAnimation()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Width = FrameSize;
            Height = FrameSize;

            animationStrip = new ResourceManager(GetType()).GetObject("LoadingAnimationStrip") as Bitmap;
            if (animationStrip == null)
                throw new Exception("The required LoadingAnimationStrip resource is missing or invalid.");

            framesCount = animationStrip.Width / FrameSize;
        }

        /// <summary>
        /// Called when the Visible property has changed.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            if (Visible)
                CurrentFrame = 0;
            base.OnVisibleChanged(e);
        }

        /// <summary>
        /// Paints the contents of the control.
        /// </summary>
        /// <param name="e">Data associated with the event.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (currentFrame < 0 || currentFrame >= framesCount)
                return;

            e.Graphics.DrawImage(animationStrip, ClientRectangle, new Rectangle(currentFrame * FrameSize, 0, FrameSize, FrameSize),
                GraphicsUnit.Pixel);
        }

        /// <summary>
        /// Displays the next animation frame.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Data associated with the event.</param>
        private void frameTimer_Tick(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                int frame = CurrentFrame + 1;
                if (frame >= framesCount)
                    frame = 0;
                CurrentFrame = frame;
            }
        }
    }
}
