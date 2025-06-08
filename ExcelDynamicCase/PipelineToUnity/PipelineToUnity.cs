using ExcelDynamicCase.Domain;
using ExcelUnityPipeline;
using System;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExcelDynamicCase.PipelineToUnity
{
    public static class PipelineToUnity
    {
        private const string PIPE = "BattlePipe";
        private static readonly SemaphoreSlim _sync = new SemaphoreSlim(1, 1);
        private static NamedPipeClientStream _pipe;

        public static async Task InitPipeAsync()
        {
            _pipe = new NamedPipeClientStream(
                        ".", PIPE, PipeDirection.InOut, PipeOptions.Asynchronous);

            await _pipe.ConnectAsync();
            _pipe.ReadMode = PipeTransmissionMode.Message;   // keep Message mode
        }

        public static async Task SendOverworldStateAsync(BattleResult battleResult)
        {
            if (_pipe is null || !_pipe.IsConnected)
                throw new InvalidOperationException("Pipe not initialised");

            await _sync.WaitAsync();         // serialise callers
            try
            {
                if (!(battleResult is null))
                {
                    await PipeHelper.WriteAsync(_pipe, battleResult);
                } 

                BattleParameters battleParameters = await PipeHelper.ReadAsync<BattleParameters>(_pipe);

                LevelManagement.CaseQuestionCode = (CaseQuestionEnum)battleParameters.QuestionId;
                LevelManagement.Challenger = battleParameters.Challenger;
                //Storage.AllowedFunctions = battleParameters.AllowedFunctions;

                ThisWorkbook.ExcelCtx.Post(_ => StartBattleMode(), null);

                // …use parms …
            }
            finally
            {
                _sync.Release();
            }
        }

        /// somewhere in your shutdown/cleanup path
        public static void ClosePipe()
        {
            _pipe?.Dispose();
        }

        public static void StartBattleMode()
        {
            LevelManagement.StartCaseQuestion();
        }
    }
}
