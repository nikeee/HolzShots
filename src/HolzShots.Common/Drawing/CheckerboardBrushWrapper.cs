using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolzShots.Drawing
{
    /// <summary>
    /// We need this because the "Large Checkerboard" of the HatchBrush are too small.
    /// Also, we cannot control the size of the tiles.
    /// </summary>
    public sealed class CheckerboardBrushWrapper : IDisposable
    {
        public static Color DefaultCheckerboardFirstColor { get; } = Color.FromArgb(255, 204, 204, 204);
        public static Color DefaultCheckerboardSecondColor { get; } = Color.White;

        public Size TileSize { get; }
        public Color FirstColor { get; }
        public Color SecondColor { get; }

        private readonly Lazy<TextureBrush> _brush;
        public Brush Brush => _brush.Value;

        public CheckerboardBrushWrapper(int tileSize, Color firstColor, Color secondColor)
            : this(new Size(tileSize, tileSize), firstColor, secondColor) { }
        public CheckerboardBrushWrapper(Size tileSize, Color firstColor, Color secondColor)
        {
            TileSize = tileSize;
            FirstColor = firstColor;
            SecondColor = secondColor;
            _brush = new Lazy<TextureBrush>(CreateTextureBrush, false);
        }

        public static CheckerboardBrushWrapper CreateDefault(int tileSize) => new CheckerboardBrushWrapper(tileSize, DefaultCheckerboardFirstColor, DefaultCheckerboardSecondColor);

        private TextureBrush CreateTextureBrush()
        {
            var tile = new Bitmap(TileSize.Width * 2, TileSize.Height * 2);
            using (var firstBrush = new SolidBrush(FirstColor))
            using (var secondBrush = new SolidBrush(SecondColor))
            using (var g = Graphics.FromImage(tile))
            {
                // top-left
                g.FillRectangle(firstBrush, new Rectangle(0, 0, TileSize.Width, TileSize.Height));
                // bottom-right
                g.FillRectangle(firstBrush, new Rectangle(TileSize.Width, TileSize.Height, TileSize.Width, TileSize.Height));
                // bottom-left
                g.FillRectangle(secondBrush, new Rectangle(0, TileSize.Height, TileSize.Width, TileSize.Height));
                // top-right
                g.FillRectangle(secondBrush, new Rectangle(TileSize.Width, 0, TileSize.Width, TileSize.Height));
            }
            return new TextureBrush(tile);
        }

        public void Dispose()
        {
            if (_brush.IsValueCreated)
                _brush.Value.Dispose();
        }
    }
}
