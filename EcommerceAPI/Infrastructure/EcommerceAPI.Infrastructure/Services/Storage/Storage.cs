using EcommerceAPI.Infrastructure.Operations;

namespace EcommerceAPI.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string pathOrContainerName, string fileName);

        protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFileMethod, bool first = true)
        {
            #region FileRename method for Local Storage

            //string extension = Path.GetExtension(fileName);
            //string oldName = Path.GetFileNameWithoutExtension(fileName);
            //string regulatedFileName = NameOperation.CharacterRegulatory(oldName);

            //var files = Directory.GetFiles(path, regulatedFileName + "*"); //finds all files starting with this name

            //if (files.Length == 0) return regulatedFileName + "-1" + extension; //So this is the first time the file is uploaded with this name.

            //int[] fileNumbers = new int[files.Length];  //We'll get the file numbers here and find the highest one.
            //int lastHyphenIndex;
            //for (int i = 0; i < files.Length; i++)
            //{
            //    lastHyphenIndex = files[i].LastIndexOf("-");
            //    fileNumbers[i] = int.Parse(files[i].Substring(lastHyphenIndex + 1, files[i].Length - extension.Length - lastHyphenIndex - 1));
            //}
            //var biggestNumber = fileNumbers.Max(); //we found the highest number
            //biggestNumber++;
            //return regulatedFileName + "-" + biggestNumber + extension; //we increment and return

            #endregion

            string newFileName = await Task.Run<string>(async () =>
            {
                string extension = Path.GetExtension(fileName);
                string newFileName = string.Empty;
                if (first)
                {
                    string oldName = Path.GetFileNameWithoutExtension(fileName);
                    newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
                }
                else
                {
                    newFileName = fileName;
                    int indexNo1 = newFileName.IndexOf("-");
                    if (indexNo1 == -1)
                        newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                    else
                    {
                        int lastIndex = 0;
                        while (true)
                        {
                            lastIndex = indexNo1;
                            indexNo1 = newFileName.IndexOf("-", indexNo1 + 1);
                            if (indexNo1 == -1)
                            {
                                indexNo1 = lastIndex;
                                break;
                            }
                        }

                        int indexNo2 = newFileName.IndexOf(".");
                        string fileNo = newFileName.Substring(indexNo1 + 1, indexNo2 - indexNo1 - 1);

                        if (int.TryParse(fileNo, out int _fileNo))
                        {
                            _fileNo++;
                            newFileName = newFileName.Remove(indexNo1 + 1, indexNo2 - indexNo1 - 1).Insert(indexNo1 + 1, _fileNo.ToString());
                        }
                        else
                            newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                    }
                }

                if (hasFileMethod(pathOrContainerName, newFileName))
                    return await FileRenameAsync(pathOrContainerName, newFileName, hasFileMethod, false);
                else
                    return newFileName;
            });

            return newFileName;
        }
    }
}
