using System;

public class Blog
{
    public int BlogID { get; set; }
    public string BlogTitle { get; set; }
    public string BlogContent { get; set; }
    public string BlogThumbnailImage { get; set; }
    public string BlogImage { get; set; }
    public DateTime BlogCreatedAt { get; set; }
    public bool BlogStatus { get; set; }
    public int CategoryID { get; set; }
    public int WriterID { get; set; }
}
