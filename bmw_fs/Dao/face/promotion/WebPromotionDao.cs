﻿using bmw_fs.Models.promotion;
using IBatisNet.DataMapper;
using System.Collections.Generic;
using System;

namespace bmw_fs.Dao.face.promotion
{
    public class WebPromotionDao
    {
        public void insertWebPromotion(Promotion webPromotion)
        {
           Mapper.Instance().Insert("webPromotion.insertWebPromotion", webPromotion);
        }

        public IList<Promotion> findAll(Promotion webPromotion)
        {
            return Mapper.Instance().QueryForList<Promotion>("webPromotion.findAll", webPromotion);
        }

        public int findAllCount(Promotion webPromotion)
        {
            return Mapper.Instance().QueryForObject<int>("webPromotion.findAllCount", webPromotion);
        }

        public Promotion findWebPromotion(Promotion webPromotion)
        {
            return Mapper.Instance().QueryForObject<Promotion>("webPromotion.findWebPromotion", webPromotion);
        }

        public void updateWebPromotion(Promotion webPromotion)
        {
            Mapper.Instance().Update("webPromotion.updateWebPromotion", webPromotion);
        }

        public void deleteWebPromotion(Promotion webPromotion)
        {
            Mapper.Instance().Delete("webPromotion.deleteWebPromotion", webPromotion);
        }

        public void insertWebPromotionUrl(PromotionUrl promotionUrl)
        {
            Mapper.Instance().Insert("webPromotion.insertWebPromotionUrl", promotionUrl);
        }

        public String findWebPromotionImgUrl(Promotion webPromotion)
        {
           return  Mapper.Instance().QueryForObject<String>("webPromotion.findWebPromotionImgUrl", webPromotion);
        }

        public void deleteWebPromotionUrl(PromotionUrl promotionUrl)
        {
            Mapper.Instance().Delete("webPromotion.deleteWebPromotionUrl", promotionUrl);
        }
    }
}