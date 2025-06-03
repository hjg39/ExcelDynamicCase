using ExcelUnityPipeline;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelDynamicCase.PipelineToUnity
{
    public static class PipelineToUnity
    {
        private const string PIPE = "BattlePipe";

        public async static Task SendOverworldStateAsync(BattleResult battleResult)
        {
            using (NamedPipeClientStream pipe = new NamedPipeClientStream(".", PIPE, PipeDirection.InOut, PipeOptions.Asynchronous))
            {
                Trace.WriteLine("Connecting to pipe...");
                await pipe.ConnectAsync();
                Trace.WriteLine("Pipe connected.");

                try
                {

                }
                catch (System.Exception)
                {

                    throw;
                }

                await PipeHelper.WriteAsync(pipe, battleResult);   // ➜ Unity
                BattleParameters result = await PipeHelper.ReadAsync<BattleParameters>(pipe); // ⬅ Unity

                Trace.WriteLine(result.ToString());
                Trace.WriteLine(result.QuestionId);
                Trace.WriteLine(string.Join(",", result.AllowedFunctions));
            }

        }
    }
}
