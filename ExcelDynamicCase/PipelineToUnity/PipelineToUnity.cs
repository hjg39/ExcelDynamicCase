using ExcelUnityPipeline;
using System.IO.Pipes;
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
                await pipe.ConnectAsync();

                await PipeHelper.WriteAsync(pipe, battleResult);   // ➜ Unity
                BattleParameters result = await PipeHelper.ReadAsync<BattleParameters>(pipe); // ⬅ Unity
            }

        }
    }
}
