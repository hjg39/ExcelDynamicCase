using ExcelDynamicCase.Domain;
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
                pipe.ReadMode = PipeTransmissionMode.Message;
                Trace.WriteLine("Assigned read mode.");

                try
                {

                    await PipeHelper.WriteAsync(pipe, battleResult);   // ➜ Unity
                    BattleParameters battleParameters = await PipeHelper.ReadAsync<BattleParameters>(pipe); // ⬅ Unity

                    CaseQuestionEnum questionCode = (CaseQuestionEnum)battleParameters.QuestionId;
                    LevelManagement.Challenger = battleParameters.Challenger;
                    LevelManagement.CaseQuestionCode = questionCode;

                }
                catch (System.Exception ex)
                {

                    throw;
                }

            }
        }

        public static void StartBattleMode()
        {
            LevelManagement.StartCaseQuestion();
        }
    }
}
