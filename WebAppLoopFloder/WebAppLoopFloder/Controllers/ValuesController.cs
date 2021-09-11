using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAppLoopFloder.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        //public IEnumerable<string> Get()
        public List<string> Get()
        {
            //return new string[] { "value1", "value2" };
            List<string> lst = new List<string>();
            DirectoryInfo di = new DirectoryInfo(@"\\localhost\rsit");
            lst = WalkDirectoryTree(di);
            return lst;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        static List<string> WalkDirectoryTree(System.IO.DirectoryInfo root)
        {
            System.Collections.Specialized.StringCollection log = new System.Collections.Specialized.StringCollection();
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;
            List<string> lst = new List<string>();
            lst = null;

            // First, process all the files directly under this folder
            try
            {
                files = root.GetFiles("*.*");
            }
            // This is thrown if even one of the files requires permissions greater
            // than the application provides.
            catch (UnauthorizedAccessException e)
            {
                // This code just writes out the message and continues to recurse.
                // You may decide to do something different here. For example, you
                // can try to elevate your privileges and access the file again.
                log.Add(e.Message);
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            if (files != null)
            {
                //foreach (System.IO.FileInfo fi in files)
                //{
                //    // In this example, we only access the existing FileInfo object. If we
                //    // want to open, delete or modify the file, then
                //    // a try-catch block is required here to handle the case
                //    // where the file has been deleted since the call to TraverseTree().
                //    Console.WriteLine(fi.FullName);
                //}

                //// Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();
                lst = new List<string>();
                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    // Resursive call for each subdirectory.
                    //Console.WriteLine(dirInfo.FullName);
                    lst.Add(dirInfo.FullName);
                    WalkDirectoryTree(dirInfo);
                }

                
            }
            return lst;
        }
    }
}
