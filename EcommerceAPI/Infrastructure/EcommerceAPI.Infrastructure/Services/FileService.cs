using EcommerceAPI.Infrastructure.Operations;

namespace EcommerceAPI.Infrastructure.Services
{
    public class FileService
    {
        private async Task<string> FileRenameAsync(string path, string fileName)
        {
            string extension = Path.GetExtension(fileName);
            string oldName = Path.GetFileNameWithoutExtension(fileName);
            string regulatedFileName = NameOperation.CharacterRegulatory(oldName);

            var files = Directory.GetFiles(path, regulatedFileName + "*"); //finds all files starting with this name

            if (files.Length == 0) return regulatedFileName + "-1" + extension; //So this is the first time the file is uploaded with this name.

            int[] fileNumbers = new int[files.Length];  //We'll get the file numbers here and find the highest one.
            int lastHyphenIndex;
            for (int i = 0; i < files.Length; i++)
            {
                lastHyphenIndex = files[i].LastIndexOf("-");
                fileNumbers[i] = int.Parse(files[i].Substring(lastHyphenIndex + 1, files[i].Length - extension.Length - lastHyphenIndex - 1));
            }
            var biggestNumber = fileNumbers.Max(); //we found the highest number
            biggestNumber++;
            return regulatedFileName + "-" + biggestNumber + extension; //we increment and return
        }
    }
}
