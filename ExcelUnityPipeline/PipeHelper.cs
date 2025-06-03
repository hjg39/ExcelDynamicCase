using System;
using System.Buffers;
using System.IO;
using System.IO.Pipes;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

public static class PipeHelper
{
    private static readonly JsonSerializerOptions JsonOpts = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static async Task WriteAsync<T>(
        PipeStream pipe, T payload, CancellationToken token = default)
    {
        // Serialize directly to a fresh byte[] (no spans involved)
        byte[] json = JsonSerializer.SerializeToUtf8Bytes(payload, JsonOpts);
        await pipe.WriteAsync(json, 0, json.Length, token).ConfigureAwait(false);
        await pipe.FlushAsync(token).ConfigureAwait(false);
    }

    public static async Task<T> ReadAsync<T>(
        PipeStream pipe, CancellationToken token = default)
    {
        using (var ms = new MemoryStream())
        {
            var buf = new byte[8 * 1024];
            do
            {
                int n = await pipe.ReadAsync(buf, 0, buf.Length, token)
                                    .ConfigureAwait(false);
                if (n == 0) throw new EndOfStreamException();
                ms.Write(buf, 0, n);
            }
            while (!pipe.IsMessageComplete);

            ms.Position = 0;
            return await JsonSerializer.DeserializeAsync<T>(ms, JsonOpts, token)
                                        .ConfigureAwait(false);
        }
    }
}