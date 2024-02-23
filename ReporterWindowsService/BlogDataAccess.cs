using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class BlogDataAccess
{
    const string connectionString = "provide your connection string here";

    public List<Blog> GetAllBlogs()
    {
        List<Blog> blogs = new List<Blog>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = @"SELECT Blogs.*, Categories.CategoryName, Writers.WriterName FROM Blogs INNER JOIN Categories ON Blogs.CategoryID = Categories.CategoryID
INNER JOIN Writers ON Blogs.WriterID = Writers.WriterID WHERE BlogCreatedAt BETWEEN @InitialDate AND @EndingDate";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                DateTime initialDate = DateTime.Now.AddHours(24);
                DateTime endingDate = DateTime.Now;

                command.Parameters.AddWithValue("@InitialDate", initialDate);
                command.Parameters.AddWithValue("@EndingDate", endingDate);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Blog blog = MapReaderToBlog(reader);
                        blogs.Add(blog);
                    }
                }
            }
        }

        return blogs;
    }

    public Blog GetBlogById(int blogId)
    {
        Blog blog = null;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM Blogs WHERE BlogID = @BlogID";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@BlogID", blogId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        blog = MapReaderToBlog(reader);
                    }
                }
            }
        }

        return blog;
    }

    private static Blog MapReaderToBlog(SqlDataReader reader)
    {
        return new Blog
        {
            BlogID = Convert.ToInt32(reader["BlogID"]),
            BlogTitle = reader["BlogTitle"].ToString(),
            BlogContent = reader["BlogContent"].ToString(),
            BlogThumbnailImage = reader["BlogThumbnailImage"].ToString(),
            BlogImage = reader["BlogImage"].ToString(),
            BlogCreatedAt = Convert.ToDateTime(reader["BlogCreatedAt"]),
            BlogStatus = Convert.ToBoolean(reader["BlogStatus"]),
            Category = reader["CategoryName"].ToString(),
            Writer = reader["WriterName"].ToString()
        };
    }

}