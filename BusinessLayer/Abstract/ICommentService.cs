﻿using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface ICommentService : IGenericService<Comment>
    {
        List<Comment> GetCommentsByBlogId(int id);
        List<Comment> GetCommentsWithBlogAndWriter();
    }
}
