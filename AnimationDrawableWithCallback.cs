using System;
using Android.Graphics.Drawables;
using Android.Runtime;

namespace portal.PortalExInteractive.Droid
{
    public class AnimationDrawableWithCompletion : AnimationDrawable
    {
        public event EventHandler Completed;
        private bool _playing;

        public AnimationDrawableWithCompletion()
        {
        }

        protected AnimationDrawableWithCompletion(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void Start()
        {
            base.Start();
            _playing = true;
        }

        public override bool SelectDrawable(int idx)
        {
            var retVal = base.SelectDrawable(idx);

            if (OneShot && _playing && (idx != 0) && (idx == NumberOfFrames - 1))
            {
                _playing = false;
                FireCompleted();
            }
            return retVal;
        }

        private void FireCompleted()
        {
            var handler = Completed;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
    }
}

