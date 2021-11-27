using System;
using System.IO;
using System.Threading;
using System.Reflection;


class Sort
{
    public static string path { set; get; }
    private static string[] SortBy(string path, out string[] n)//Get array of files by path
    {
        string[] a = Directory.GetFiles(path);//Get
        n = Directory.GetDirectories(path);
        return a;
    }

    public static void SortFile()
    {
        {
            
            string[] n;


            string[] ob = Sort.SortBy(path, out n);//Get files
            foreach (string file in ob)
            {
                try
                {
                    if (Directory.Exists(path + @"\" + Path.GetExtension(file).ToUpper().Substring(1)))//If directory that is named as a file extension exists
                    {
                        File.Move(file, path + @"\" + Path.GetExtension(file).ToUpper().Substring(1) + @"\"+Path.GetFileName(file));// Move file to the directory
                    }
                    else//If it does not exist
                    {
                        DirectoryInfo path1 = Directory.CreateDirectory(path + @"\" + Path.GetExtension(file).ToUpper().Substring(1));//Create directory that is named as a file extension
                        File.Move(file, path1.FullName + @"\" + Path.GetFileName(file));//Move file to it
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

    }
    /*In the first version, I thought
     * I would use events. But it turned out
     * that it was too resource-intensive. 
     * I don't remove the code to show this 
     * implementation option as well.*/
    public void Watch()
    {
        string path = path + @"\";
        FileSystemWatcher watch = new FileSystemWatcher(path);

        watch.NotifyFilter = NotifyFilters.FileName;

        watch.Changed += Handler;
        watch.Created += Handler;
        watch.EnableRaisingEvents = true;


    }

    private static void Handler(object sender, FileSystemEventArgs e)//Handler of event.
    {

        Sort.SortFile();

    }
}

class Demo
{
    static void Main()
    {
        Console.WriteLine("......Enter the folder where the sorting will take place......\n......Example: F:\\Dirname\\Dirname......");
        Sort.path = Console.ReadLine();
        while (true)//I know it's not right, but it's the best I've come up with. 
        {
            Thread.Sleep(300000);//Code works once every five minutes.
            Sort.SortFile();
        }

       
    }

}
