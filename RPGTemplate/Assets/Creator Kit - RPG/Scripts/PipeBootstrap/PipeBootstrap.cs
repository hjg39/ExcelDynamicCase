// PipeBootstrap.cs
using UnityEngine;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ExcelUnityPipeline;
using System.IO;
using RPGM.UI;
using RPGM.Gameplay;
using RPGM.Core;
using UnityEditor;                 // DTOs
                                   // PipeHelper lives in the same assembly
public class PipeBootstrap : MonoBehaviour
{
    private const string PIPE = "BattlePipe";
    private NamedPipeServerStream _pipe;
    private CancellationTokenSource _cts;

    // -------------- Unity lifecycle --------------
    private async void Start()
    {
        // Persist this object across scene loads
        DontDestroyOnLoad(gameObject);

        _cts = new CancellationTokenSource();
        _pipe = new NamedPipeServerStream(
                    PIPE,
                    PipeDirection.InOut,
                    1,
                    PipeTransmissionMode.Message,
                    PipeOptions.Asynchronous);

        Debug.Log($"[PipeBootstrap] Waiting for Excel on pipe \"{PIPE}\"…");

        await _pipe.WaitForConnectionAsync(_cts.Token)
                   .ConfigureAwait(true);          // resume on Unity thread

        Debug.Log("[PipeBootstrap] Excel connected!");

        GameModel model = Schedule.GetModel<GameModel>();

        // Main message loop
        while (!_cts.Token.IsCancellationRequested && _pipe.IsConnected)
        {
            try
            {
                BattleResult battleResult = await PipeHelper.ReadAsync<BattleResult>(_pipe, _cts.Token);
                model.input.EndBattleState(battleResult);

                // await PipeHelper.WriteAsync(_pipe, battleParameters, _cts.Token);
            }
            catch (EndOfStreamException) { QuitApp(); } // Excel closed the pipe
            catch (IOException ex)
            {
                Debug.LogWarning($"Pipe IO error: {ex.Message}");
                break;
            }
            catch (TaskCanceledException)
            {
                QuitApp();
            }
        }

        Debug.Log("[PipeBootstrap] Pipe closed — exiting bootstrap.");
    }

    void QuitApp()
    {
        Debug.Log("[PipeBootstrap] Task cancelled — quitting application.");
        Application.Quit();

        #if UNITY_EDITOR
                // In the Editor, Application.Quit() does nothing, so stop Play Mode:
                EditorApplication.ExitPlaymode();      // Unity 2021+ alternative
        #endif
    }

    public async Task RunBattle(BattleParameters battleParameters)
    {
        await PipeHelper.WriteAsync(_pipe, battleParameters, _cts.Token);
    }

    private void OnApplicationQuit()
    {
        _cts?.Cancel();
        _pipe?.Dispose();
    }

    // -------------- Your game-specific logic --------------
    /// <summary>Loads the battle scene, runs combat, returns a result.</summary>
    //private async Task<BattleResult> RunBattleAsync(
    //    BattleState state, CancellationToken token)
    //{
    //    // 1. switch to the Battle scene
    //    await UnityEngine.SceneManagement.SceneManager
    //         .LoadSceneAsync("Battle", UnityEngine.SceneManagement.LoadSceneMode.Single);

    //    // 2. TODO: hand 'state' to your BattleManager, wait until combat ends
    //    // Here we fake a quick battle with Task.Delay
    //    await Task.Delay(500, token);

    //    // 3. Build & return the outcome
    //    return new BattleResult
    //    {
    //        WinnerId = 1,
    //        Turns = 3,
    //        // ..fill remaining fields
    //    };
    //}
}
