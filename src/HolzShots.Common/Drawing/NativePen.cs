using System.Drawing;
using HolzShots.NativeTypes;

namespace HolzShots.Drawing;

public class NativePen : IDisposable
{
    public IntPtr Handle { get; }

    public NativePen(uint brushColor, int size) => Handle = NativeMethods.CreatePen(PenStyle.Solid, size, brushColor);
    public NativePen(Color brushColor, int size)
        : this(unchecked((uint)ColorTranslator.ToWin32(brushColor)), size)
    { }

    #region IDisposable Support

    private bool disposedValue = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            NativeMethods.DeleteObject(Handle);
            disposedValue = true;
        }
    }

    ~NativePen() => Dispose(false);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion
}
