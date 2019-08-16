using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace dotNET.Core
{
    public interface IFileManager
    {
       void ShowStructure(Action<int, string> render);
        Task<string> ReadAllTextAsync(string path);

    }

    public class FileManager : IFileManager
   {
        public IFileProvider FileProvider { get; private set; }
    
        public FileManager(IFileProvider fileProvider)
        {
            this.FileProvider = fileProvider;
       }
     
       public void ShowStructure(Action<int, string> render)
      {
           int layer = -1;
          Render("", ref layer, render);
       }
    
       private void Render(string subPath, ref int layer, Action<int, string> render)
       {
          layer++;
          foreach (var fileInfo in this.FileProvider.GetDirectoryContents(subPath))
           {
                 render(layer, fileInfo.Name);
                if (fileInfo.IsDirectory)
                     {
                        Render($@"{subPath}\{fileInfo.Name}".TrimStart('\\'), ref layer, render);
                     }
             }
          layer--;
       }

        public async Task<string> ReadAllTextAsync(string path)
     {
         byte[] buffer;
         using (Stream readStream = this.FileProvider.GetFileInfo(path).CreateReadStream())
          {
             buffer = new byte[readStream.Length];
           await readStream.ReadAsync(buffer, 0, buffer.Length);
         }
        return Encoding.UTF8.GetString(buffer);
     }
    }

}
