
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace AyaWo.Unit
{
    /// <summary>
    /// 读取dat文件helper
    /// </summary>
    public class DatHelper<T>
    {
        /// <summary>
        /// 获取配置文件dat
        /// </summary>
        /// <param name="datName"></param>
        /// <returns></returns>
        public static List<T> GetDatList(String datName)
        {
            try
            {
                var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\" + datName);
                if (File.Exists(filepath))
                {
                    using (StreamReader sw = new StreamReader(filepath))
                    {
                        return JsonConvert.DeserializeObject<List<T>>(sw.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        /// <summary>
        /// 保存配置文件dat
        /// </summary>
        /// <param name="datName">文件名称</param>
        /// <param name="content">内容</param>
        public static void SaveDat(String datName,string content)
        {
            try
            {
                var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\" + datName);
                //实例化一个文件流--->与写入文件相关联  
                FileStream fs = new FileStream(filepath, FileMode.Create);
                //实例化一个StreamWriter-->与fs相关联  
                StreamWriter sw = new StreamWriter(fs);
                //开始写入  
                sw.Write(content);
                //清空缓冲区  
                sw.Flush();
                //关闭流  
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
