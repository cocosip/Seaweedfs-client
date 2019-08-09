using System;

namespace Seaweedfs.Client.Scheduling
{
    /// <summary>定时任务
    /// </summary>
    public interface IScheduleService
    {
        /// <summary>运行定时任务
        /// </summary>
        void StartTask(string name, Action action, int dueTime, int period);

        /// <summary>停止定时任务
        /// </summary>
        void StopTask(string name);
    }
}
