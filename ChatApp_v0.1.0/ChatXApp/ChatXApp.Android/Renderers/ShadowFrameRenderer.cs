using Android.Content;
using Android.Graphics;
using ChatXApp.Droid.Renderers;
using ChatXApp.View.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using FrameRenderer = Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer;

[assembly: ExportRenderer(typeof(ShadowFrame), typeof(ShadowFrameRenderer))] 
namespace ChatXApp.Droid.Renderers
{
    public class ShadowFrameRenderer : FrameRenderer
    {
        public ShadowFrameRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                Element.Margin = new Thickness(-5);
                Element.Padding = new Thickness(15);
                Control.CardElevation = 0;
                Control.PreventCornerOverlap = false;
            }
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            // draw border
            var strokePaint = new Paint();
            strokePaint.SetARGB(255, 255, 255, 255);
            strokePaint.SetStyle(Paint.Style.Stroke);
            strokePaint.StrokeWidth = 2;
            var rect = canvas.ClipBounds;
            var outline = new Rect(15, 15, rect.Right - 15, rect.Bottom - 15);

            var bottomPaint = new Paint();
            bottomPaint.SetStyle(Paint.Style.Fill);
            var shadowShader = new LinearGradient(0, 0, 0, 30, new Android.Graphics.Color(0, 0, 0, 100), new Android.Graphics.Color(0, 0, 0, 0), Shader.TileMode.Mirror);
            bottomPaint.SetShader(shadowShader);

            var rightPaint = new Paint();
            rightPaint.SetStyle(Paint.Style.Fill);
            var rightShader = new LinearGradient(0, 0, 20, 0, new Android.Graphics.Color(0, 0, 0, 100), new Android.Graphics.Color(0, 0, 0, 0), Shader.TileMode.Mirror);
            rightPaint.SetShader(rightShader);

            // draw the bottom and right shadows
            canvas.DrawRect(12, rect.Bottom - 12, rect.Right - 12, rect.Bottom + 3, bottomPaint);
            canvas.DrawRect(rect.Right - 12, 14, rect.Right + 3, rect.Bottom, rightPaint);
            canvas.DrawRect(outline, strokePaint);
        }
    }
}