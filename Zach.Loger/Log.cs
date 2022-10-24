using log4net;
using System;
namespace Zach.Loger
{
    /// <summary>
    /// 日志
    /// </summary>
    public class Log
    {
        /// <summary>
        /// 日志实体类
        /// </summary>
        private ILog logger;

        private LogFormat format;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="log">日志操作对象</param>
        public Log(ILog log)
        {
            this.logger = log;
        }
        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="message">消息</param>
        public void Debug(object message)
        {
            this.logger.Debug(message);
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="message">消息</param>
        public void Error(object message)
        {
            this.logger.Error(message);
        }
        /// <summary>
        /// 信息日志
        /// </summary>
        /// <param name="message">消息</param>
        public void Info(object message)
        {
            this.logger.Info(message);
        }
        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="message">消息</param>
        public void Warn(object message)
        {
            this.logger.Warn(message);
        }
        /// <summary>
        /// Format日志
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        private void WriteFormatLog(LogLevel logLevel, object message)
        {
            format = new LogFormat();
            string tempStr;
            switch (logLevel)
            {
                case LogLevel.Debug:
                    tempStr = format.EasyFormatDebug(message.ToString());
                    this.logger.Debug(tempStr);
                    break;
                case LogLevel.Info:
                    tempStr = format.EasyFormatInfo(message.ToString());
                    this.logger.Info(tempStr);
                    break;
                case LogLevel.Error:
                    tempStr = format.EasyFormatError(message.ToString());
                    this.logger.Error(tempStr);
                    break;
                case LogLevel.Warning:
                    tempStr = format.EasyFormatWarning(message.ToString());
                    this.logger.Warn(tempStr);
                    break;
                default:
                    break;
            }
        }

        public void WriteFormatDebug(object message) => WriteFormatLog(LogLevel.Debug, message);

        public void WriteFormatInfo(object message) => WriteFormatLog(LogLevel.Info, message);

        public void WriteFormatError(object message) => WriteFormatLog(LogLevel.Error, message);

        public void WriteFormatWarn(object message) => WriteFormatLog(LogLevel.Warning, message);

    }
}
