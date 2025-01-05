using System;
using System.Runtime.InteropServices;

public unsafe class TFoolStringBuilder : IDisposable
{
    static public readonly int TempBufferSize = 1024;
    int m_Capacity = 0;
    int m_Size = 0;
    private byte[] m_Buffer = null;
    private char* m_Char = null;
    private char* m_Temp = (char*)Marshal.AllocHGlobal(sizeof(char) * TempBufferSize).ToPointer();
    private GCHandle m_BufferHandle;
    private bool _disposed = false;

    void __Resize(int nCapacity)
    {
        if (nCapacity > m_Capacity)
        {
            nCapacity = (nCapacity + 4095) & ~4095;
            var buf = new byte[nCapacity * sizeof(char)];
            if (m_Size > 0)
            {
                m_Buffer.CopyTo(buf, 0);
                m_BufferHandle.Free();
            }
            m_Capacity = nCapacity;
            m_Buffer = buf;
            m_BufferHandle = GCHandle.Alloc(m_Buffer, GCHandleType.Pinned);
            m_Char = (char*)m_BufferHandle.AddrOfPinnedObject().ToPointer();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                if (m_BufferHandle.IsAllocated)
                {
                    m_BufferHandle.Free();
                }
            }

            if (m_Temp != null)
            {
                Marshal.FreeHGlobal((IntPtr)m_Temp);
                m_Temp = null;
            }

            _disposed = true;
        }
    }

    ~TFoolStringBuilder()
    {
        Dispose(false);
    }

    public TFoolStringBuilder Append(int v)
    {
        var n = FormatInt(m_Temp, v);
        AppendFormatted(n);
        return this;
    }

    public TFoolStringBuilder Append(float v)
    {
        var n = FormatFloat(m_Temp, v);
        AppendFormatted(n);
        return this;
    }

    public TFoolStringBuilder Append(bool v)
    {
        var n = FormatBool(m_Temp, v);
        AppendFormatted(n);
        return this;
    }

    public TFoolStringBuilder Append(DateTime v)
    {
        var n = FormatDateTime(m_Temp, v);
        AppendFormatted(n);
        return this;
    }

    public TFoolStringBuilder Append(string v)
    {
        if (v != null && v.Length > 0)
        {
            __Resize(m_Size + v.Length);
            var nSizeCopy = v.Length * sizeof(char);
            fixed (char* p = v)
            {
                Buffer.MemoryCopy(p, m_Char + m_Size, nSizeCopy, nSizeCopy);
            }
            m_Size += v.Length;
        }
        return this;
    }

    private void AppendFormatted(int startIndex)
    {
        var nSize = TempBufferSize - startIndex;
        __Resize(m_Size + nSize);
        var nSizeCopy = nSize * sizeof(char);
        Buffer.MemoryCopy(m_Temp + startIndex, m_Char + m_Size, nSizeCopy, nSizeCopy);
        m_Size += nSize;
    }

    private static int FormatInt(char* buffer, int v)
    {
        var n = TempBufferSize;
        if (v != 0)
        {
            for (int i = Math.Abs(v); i != 0; i /= 10)
            {
                buffer[--n] = (char)('0' + i % 10);
            }

            if (v < 0)
            {
                buffer[--n] = '-';
            }
        }
        else
        {
            buffer[--n] = '0';
        }
        return n;
    }

    private static int FormatFloat(char* buffer, float v)
    {
        var n = TempBufferSize;
        var t = v < 0.0f ? -v : v;
        var ipart = (uint)t;
        var fpart = (uint)((t - ipart) * 10000.0f + 0.5f);
        if (fpart != 0)
        {
            for (uint i = fpart; i != 0; i /= 10)
            {
                buffer[--n] = (char)('0' + i % 10);
            }
            buffer[--n] = '.';
        }

        if (ipart != 0)
        {
            for (uint i = ipart; i != 0; i /= 10)
            {
                buffer[--n] = (char)('0' + i % 10);
            }

            if (v < 0)
            {
                buffer[--n] = '-';
            }
        }
        else
        {
            buffer[--n] = '0';
        }
        return n;
    }

    private static int FormatBool(char* buffer, bool v)
    {
        var n = TempBufferSize;
        buffer[--n] = 'e';
        if (v)
        {
            buffer[--n] = 'r';
            buffer[--n] = 'u';
            buffer[--n] = 't';
        }
        else
        {
            buffer[--n] = 's';
            buffer[--n] = 'l';
            buffer[--n] = 'a';
            buffer[--n] = 'f';
        }
        return n;
    }

    private static int FormatDateTime(char* buffer, DateTime v)
    {
        var n = TempBufferSize;
        var h = v.Hour;
        var m = v.Minute;
        var s = v.Second;
        if (s != 0)
        {
            for (int i = s; i != 0; i /= 10)
            {
                buffer[--n] = (char)('0' + i % 10);
            }

            if (s < 10)
            {
                buffer[--n] = '0';
            }
        }
        else
        {
            buffer[--n] = '0';
            buffer[--n] = '0';
        }
        buffer[--n] = ':';
        if (m != 0)
        {
            for (int i = m; i != 0; i /= 10)
            {
                buffer[--n] = (char)('0' + i % 10);
            }

            if (m < 10)
            {
                buffer[--n] = '0';
            }
        }
        else
        {
            buffer[--n] = '0';
            buffer[--n] = '0';
        }
        buffer[--n] = ':';
        if (h != 0)
        {
            for (int i = h; i != 0; i /= 10)
            {
                buffer[--n] = (char)('0' + i % 10);
            }

            if (h < 10)
            {
                buffer[--n] = '0';
            }
        }
        else
        {
            buffer[--n] = '0';
            buffer[--n] = '0';
        }
        return n;
    }
}