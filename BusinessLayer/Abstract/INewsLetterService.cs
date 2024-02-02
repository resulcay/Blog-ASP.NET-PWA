﻿using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface INewsLetterService
    {
        void AddNewsLetter(NewsLetter newsLetter);
        NewsLetter GetById(int id);
    }
}
