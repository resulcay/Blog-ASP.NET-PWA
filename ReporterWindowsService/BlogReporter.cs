using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace ReporterWindowsService
{
    public partial class BlogReporter : ServiceBase
    {
        readonly Timer timer = new Timer();

        public BlogReporter()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is started at " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 10000;
            timer.Enabled = true;
        }
        protected override void OnStop()
        {
            WriteToFile("Service is stopped at " + DateTime.Now);
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("Service is recall at " + DateTime.Now);
        }
        public void WriteToFile(string Message)
        {
            BlogDataAccess _blogManager = new BlogDataAccess();

            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            string filepath = Path.Combine(path, "ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (StreamWriter sw = File.AppendText(filepath))
            {

                try
                {
                    List<Blog> blogs = _blogManager.GetAllBlogs();
                    foreach (var blog in blogs)
                    {
                        sw.WriteLine(Message + $"BlogID: {blog.BlogID}");
                        sw.WriteLine(Message + $"BlogTitle: {blog.BlogTitle}");
                        sw.WriteLine(Message + $"BlogContent: {blog.BlogContent}");
                        sw.WriteLine(Message + $"BlogThumbnailImage: {blog.BlogThumbnailImage}");
                        sw.WriteLine(Message + $"BlogImage: {blog.BlogImage}");
                        sw.WriteLine(Message + $"BlogCreatedAt: {blog.BlogCreatedAt}");
                        sw.WriteLine(Message + $"BlogStatus: {blog.BlogStatus}");
                        sw.WriteLine(Message + $"CategoryID: {blog.CategoryID}");
                        sw.WriteLine(Message + $"WriterID: {blog.WriterID}");
                        sw.WriteLine(new string('-', 30));
                    }
                }
                catch (Exception ex)
                {
                    sw.WriteLine(Message + "Error: " + ex.Message);
                    throw new Exception();
                }
            }
        }
    }
}