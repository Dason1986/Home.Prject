﻿using DomainModel.Aggregates.FileAgg;
using DomainModel.Aggregates.GalleryAgg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace HomeApplication.Test
{
    class SubRepository
    {
        public static readonly Guid photoid = Guid.Parse("00f73871-afe7-431a-a9ec-df44b1dcb736");
        public static readonly Guid fileid = Guid.Parse("00f73871-afe7-431a-a9ec-df44b1dcb736");
        public SubRepository()
        {
            _photos = new List<Photo>();
            _albums = new List<Album>();
            _files = new List<FileInfo>();
        }
        readonly List<Photo> _photos;
        readonly List<Album> _albums;
        readonly List<FileInfo> _files;

        public IQueryable<Photo> GetALLPhtots()
        {
            return _photos.AsQueryable();
        }
        public IQueryable<FileInfo> GetALLFiles()
        {
            return _files.AsQueryable();
        }
        public IQueryable<Album> GetALLAlbums()
        {
            return _albums.AsQueryable();
        }

        public void InitPhoto()
        {
            Bitmap image = new Bitmap(100, 100);
            var file = new FileInfo() { ID = fileid };
            var photo = new Photo() { ID = photoid, File = file };
            file.Photo = photo;
            _files.Add(file);
            _photos.Add(photo);
        }
    }
}