
using System;
using System.Diagnostics;
using System.Linq;

namespace LabelImageSystem
{
    /// <summary>
    /// 调用外部程序工具
    /// </summary>
    public sealed class InvokeUtils
    {
        /// <summary>
        /// 调用外部程序
        /// </summary>
        /// <param name="path">外部程序路径</param>
        /// <param name="arguments">参数列表</param>
        /// <returns>返回内容</returns>
        public static string Invoke(string path, params string[] arguments)
        {
            try
            {
                var info = new ProcessStartInfo
                {
                    FileName = path,
                    Arguments = arguments.Aggregate((prev, next) => $"\"{prev}\" \"{next}\""),
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (var process = Process.Start(info))
                {
                    return process.StandardOutput.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
