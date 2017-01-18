using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeoBase.Components.CustomControls
{
    public class WidthAnimate:BackgroundWorker
    {
        private Control _control;

        private static bool _stop = false;

        private AnimationInfo _animationInfo;

        public event Action OnAnimationComplete;
        
        public WidthAnimate(Control control, int from, int to, int speed = 100, int step = 10)
        {
            _animationInfo = new AnimationInfo
            {
                From = from,
                To = to,
                Speed = speed,
                Step = step
            };
            
            _control = control;

            this.DoWork += Animate;
            this.RunWorkerCompleted += WidthAnimate_RunWorkerCompleted;
            this.ProgressChanged += AnimationStep;
        }

        private void AnimationStep(object sender, ProgressChangedEventArgs e)
        {
            MethodInvoker methodInvokerDelegate = delegate ()
            { _control.Width = e.ProgressPercentage; };

            if (_control.InvokeRequired)
                _control.Invoke(methodInvokerDelegate);
            else
                methodInvokerDelegate();
        }

        public void Start()
        {
            this.RunWorkerAsync(_animationInfo);
        }

        public void AnimationStop()
        {
            if (this.IsBusy)
            {
                _stop = true;
            }
        }

        private void WidthAnimate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (OnAnimationComplete != null) OnAnimationComplete();
        }

        private void Animate(object sender, DoWorkEventArgs e)
        {
            if (!(e.Argument is AnimationInfo)) return;

            var info = (AnimationInfo)e.Argument;

            int from = info.From;
            int to = info.To;
            int width = info.From;
            int step = info.Step;
            int speed = info.Speed;

            while (width != to)
            {
                if(to > from)
                {
                    if (width + step > to) width = to;
                    else width += step;
                }else if(to < from)
                {
                    if (width - step < to) width = to;
                    else width -= step;
                }
                this.OnProgressChanged(new ProgressChangedEventArgs(width, 1));
                Thread.Sleep(speed);
            }
        }
    }

    public class AnimationInfo
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Speed { get; set; }
        public int Step { get; set; }
    }
}
