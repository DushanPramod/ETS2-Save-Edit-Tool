﻿using System;
using System.Diagnostics;
using System.Threading;

namespace Funbit.Ets.Telemetry.Server.Helpers
{
    public static class TruckBookProcessHelper
    {
        static long _lastCheckTime;
        static bool _cachedRunningFlag;

        /// <summary>
        /// Returns last running game name: "ETS2", "ATS" or null if undefined.
        /// </summary>
        public static string LastRunningGameName { get; set; }

        /// <summary>
        /// Checks whether ETS2 game process is running right now. The maximum check frequency is restricted to 1 second.
        /// </summary>
        /// <returns>True if ETS2 process is run, false otherwise.</returns>
        public static bool IsTruckBookRunning
        {
            get
            {
                if (DateTime.Now - new DateTime(Interlocked.Read(ref _lastCheckTime)) > TimeSpan.FromSeconds(1))
                {
                    Interlocked.Exchange(ref _lastCheckTime, DateTime.Now.Ticks);
                    var processes = Process.GetProcesses();
                    foreach (Process process in processes)
                    {
                        try
                        {
                            bool running = process.ProcessName == "TB Client";

                            if (running)
                            {
                                _cachedRunningFlag = true;
                                LastRunningGameName = "TB Client";
                                return _cachedRunningFlag;
                            }
                        }
                        // ReSharper disable once EmptyGeneralCatchClause
                        catch
                        {
                        }
                    }

                    _cachedRunningFlag = false;
                }

                return _cachedRunningFlag;
            }
        }
    }
}