﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Home.DomainModel.Aggregates.GalleryAgg;
using HomeApplication.Dtos;
using Home.DomainModel.Repositories;

namespace HomeApplication.Services
{
    [System.ServiceModel.ServiceContract]
    public interface IGalleryService
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [System.ServiceModel.OperationContract]
        IList<GalleryType> GetAlbums();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [System.ServiceModel.OperationContract]
        int GetAllPhotoTotal();

        /// <summary>
        ///
        /// </summary>
        /// <param name="format"></param>
        /// <param name="filtertime"></param>
        /// <returns></returns>
        [System.ServiceModel.OperationContract]
        IList<GalleryType> GetTimeLineByformat(TimeFormat format, string filtertime = null);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [System.ServiceModel.OperationContract]
        IList<GalleryType> GetEquipmentMake(string filtermake = null);

        [System.ServiceModel.OperationContract]
        IList<GalleryType> GetEquipmentModel();

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="year"></param>
        ///// <returns></returns>
        //[System.ServiceModel.OperationContract]
        //IList<GalleryType> GetTimeLineMonthByYear(string year);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [System.ServiceModel.OperationContract]
        IList<GalleryType> GetRawFormat();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [System.ServiceModel.OperationContract]
        FileProfile GetRandomPhoto();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [System.ServiceModel.OperationContract]
        IDictionary<string, string> GetExifBySerialNumber(string serialNumber);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.ServiceModel.OperationContract]
        FileProfile GetPhoto(Guid id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        [System.ServiceModel.OperationContract]
        FileProfile GetPhotoBySerialNumber(string serialNumber);
    }
}