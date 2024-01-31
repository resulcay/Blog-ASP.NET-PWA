using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class BlogDataAccess
{
    public List<Blog> GetAllBlogs()
    {
        List<Blog> blogs = new List<Blog>();

        //            "Data Source=77.245.159.27\\MSSQLSERVER2019;" +
        //                         "User Id=resulcay;" +
        //             "Password=HV8plhmA&z?9d6za;" +
        //             "Initial Catalog=CoreBlogDb;"

        // string connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        string connectionString = @"Data Source=(localdb)\CoreDemo;Initial Catalog=CoreBlogDb;Integrated Security=SSPI;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM Blogs";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
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

    private Blog MapReaderToBlog(SqlDataReader reader)
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
            CategoryID = Convert.ToInt32(reader["CategoryID"]),
            WriterID = Convert.ToInt32(reader["WriterID"])
        };
    }

    public Blog GetBlogById(int blogId)
    {
        Blog blog = null;

        using (SqlConnection connection = new SqlConnection("server=(localdb)\\CoreDemo;database=CoreBlogDb; integrated security=true;"))
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
}