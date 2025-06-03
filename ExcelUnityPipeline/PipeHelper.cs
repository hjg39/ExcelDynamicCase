// PipeHelper.cs  (works everywhere)

using Newtonsoft.Json;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public static class PipeHelper
{
    /// write any object as UTF-8 JSON
    public static async Task WriteAsync<T>(
        PipeStream pipe, T payload, CancellationToken token = default)
    {
        string json = JsonConvert.SerializeObject(payload);
        byte[] buf = Encoding.UTF8.GetBytes(json);
        await pipe.WriteAsync(buf, 0, buf.Length, token);
        await pipe.FlushAsync(token);
    }

    /// read a complete message and deserialize it
    public static async Task<T> ReadAsync<T>(
        PipeStream pipe, CancellationToken token = default)
    {
        using (var ms = new MemoryStream())
        {
            var chunk = new byte[8 * 1024];
            do
            {
                int n = await pipe.ReadAsync(chunk, 0, chunk.Length, token);
                if (n == 0) throw new EndOfStreamException();
                ms.Write(chunk, 0, n);
            }
            while (!pipe.IsMessageComplete);

            string json = Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int)ms.Length);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
