using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Seaweedfs.Client.Extensions
{
    /// <summary>Task扩展
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>获取Task结果的扩展
        /// </summary>
        public static TResult WaitResult<TResult>(this Task<TResult> task, int timeoutMillis)
        {
            if (task.Wait(timeoutMillis))
            {
                return task.Result;
            }
            return default(TResult);
        }
    }
}
